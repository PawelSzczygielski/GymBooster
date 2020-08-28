using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace GymBooster.Api.IntegrationTests.Infrastructure
{
    public class TestConfigurator<TStartup> : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient HttpClient { get; }

        protected TestConfigurator(string relativeTargetProjectParentDirectory)
        {
            Assembly startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            string contentRoot = GetProjectPath(relativeTargetProjectParentDirectory, startupAssembly);

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.json");

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(TStartup));

            _testServer = new TestServer(webHostBuilder);

            HttpClient = _testServer.CreateClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5001");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            Assembly startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            ApplicationPartManager manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider(),
                }
            };

            services.AddSingleton(manager);
        }

        private static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            string projectName = startupAssembly.GetName().Name;
            string applicationBasePath = AppContext.BaseDirectory;
            DirectoryInfo currentDirectoryInfo = new DirectoryInfo(applicationBasePath);

            do
            {
                currentDirectoryInfo = currentDirectoryInfo.Parent;
                string projectPath = Path.Combine(currentDirectoryInfo.FullName, projectRelativePath);

                if (Directory.Exists(projectPath))
                {
                    DirectoryInfo projectDirectoryInfo = new DirectoryInfo(projectPath);
                    string fullProjectFilePath = Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj");

                    if (File.Exists(fullProjectFilePath))
                    {
                        string fullProjectFolderPath = Path.Combine(projectDirectoryInfo.FullName, projectName);
                        return fullProjectFolderPath;
                    }
                }
            } while (currentDirectoryInfo?.Parent != null);

            throw new DirectoryNotFoundException($"Project root could not be located using the application root {applicationBasePath}.");
        }

        public void Dispose()
        {
            HttpClient.Dispose();
            _testServer.Dispose();
        }
    }
}