using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymBooster.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _repo;
        public TrainingController(ITrainingRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Training>>> Get()
        {
            return new ObjectResult(await _repo.GetAllTrainings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Training>> Get(long id)
        {
            var todo = await _repo.GetTraining(id);
            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        [HttpPost]
        public async Task<ActionResult<Training>> Post([FromBody] Training training)
        {
            training.Id = await _repo.GetNextId();
            await _repo.Create(training);
            return new OkObjectResult(training);
        }
        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Training>> Put(long id, [FromBody] Training training)
        {
            var todoFromDb = await _repo.GetTraining(id);
            if (todoFromDb == null)
                return new NotFoundResult();
            training.Id = todoFromDb.Id;
            training.InternalId = todoFromDb.InternalId;
            await _repo.Update(training);
            return new OkObjectResult(training);
        }
        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetTraining(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}