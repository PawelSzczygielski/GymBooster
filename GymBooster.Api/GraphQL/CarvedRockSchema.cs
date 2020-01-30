using System;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace GymBooster.Api.GraphQL
{
    public class CarvedRockSchema : Schema
    {
        public CarvedRockSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<CarvedRockQuery>();
        }
    }
}
