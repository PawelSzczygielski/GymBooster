using System.Collections.Generic;
using System.Threading.Tasks;
using GymBooster.DatabaseAccess;
using GymBooster.DatabaseAccess.DbModel;
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
        public async Task<ActionResult<IEnumerable<TrainingDbModel>>> Get()
        {
            return new ObjectResult(await _repo.GetAllTrainings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDbModel>> Get(long id)
        {
            var todo = await _repo.GetTraining(id);
            if (todo == null)
                return new NotFoundResult();

            return new ObjectResult(todo);
        }

        [HttpPost]
        public async Task<ActionResult<TrainingDbModel>> Post([FromBody] TrainingDbModel training)
        {
            training.Id = await _repo.GetNextId();
            await _repo.Create(training);
            return new OkObjectResult(training);
        }
        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingDbModel>> Put(long id, [FromBody] TrainingDbModel training)
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