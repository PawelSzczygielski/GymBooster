using System.Collections.Generic;
using System.Threading.Tasks;
using GymBooster.Api.Infrastructure;
using GymBooster.Common.Objects.DTO;
using GymBooster.DatabaseAccess;
using GymBooster.DatabaseAccess.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace GymBooster.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TrainingsController : Controller
    {
        private readonly ITrainingRepository _repo;
        public TrainingsController(ITrainingRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> Get()
        {
            IEnumerable<TrainingDbModel> allTrainings = await _repo.GetAllTrainings();
            List<TrainingDTO> allTrainingsMapped = Mapper.Map(allTrainings);
            return new ObjectResult(allTrainingsMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> Get(string id)
        {
            TrainingDbModel training = await _repo.GetTraining(id);
            if (training == null)
                return new NotFoundResult();

            TrainingDTO trainingDTO = Mapper.Map(training);
            return new ObjectResult(trainingDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TrainingDTO>> Post([FromBody] TrainingDTO training)
        {
            TrainingDbModel trainingDbData = Mapper.Map(training);
            TrainingDbModel addedTraining = await _repo.Create(trainingDbData);
            TrainingDTO trainingDTO = Mapper.Map(addedTraining);
            return new OkObjectResult(trainingDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingDTO>> Put(string id, [FromBody] TrainingDTO incomingTraining)
        {
            if(id != incomingTraining.Id)
                return new BadRequestResult();

            bool trainingExists = await _repo.Exists(id);
            if (!trainingExists)
                return await Post(incomingTraining);

            TrainingDbModel trainingDbModel = Mapper.Map(incomingTraining);
            await _repo.Update(trainingDbModel);
            return new OkObjectResult(incomingTraining);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            TrainingDbModel post = await _repo.GetTraining(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}