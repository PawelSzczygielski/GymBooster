using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GymBooster.Api.DTO;
using GymBooster.DatabaseAccess;
using GymBooster.DatabaseAccess.DbModel;
using Microsoft.AspNetCore.Mvc;

namespace GymBooster.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TrainingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITrainingRepository _repo;
        public TrainingController(ITrainingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingDTO>>> Get()
        {
            IEnumerable<TrainingDbModel> allTrainings = await _repo.GetAllTrainings();
            List<TrainingDTO> allTrainingsMapped = _mapper.Map<List<TrainingDTO>>(allTrainings);
            return new ObjectResult(allTrainingsMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingDTO>> Get(long id)
        {
            TrainingDbModel training = await _repo.GetTraining(id);
            if (training == null)
                return new NotFoundResult();

            return new ObjectResult(_mapper.Map<TrainingDTO>(training));
        }

        [HttpPost]
        public async Task<ActionResult<TrainingDTO>> Post([FromBody] CreateTrainingDTO training)
        {
            CreateTrainingDbModel createTrainingDbData = _mapper.Map<CreateTrainingDbModel>(training);
            await _repo.Create(createTrainingDbData);
            return new OkObjectResult(training);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TrainingDTO>> Put([FromBody] TrainingDTO incomingTraining)
        {
            bool trainingExists = await _repo.Exists(incomingTraining.Id);
            if (!trainingExists)
                return new NotFoundResult();

            await _repo.Update(_mapper.Map<TrainingDbModel>(incomingTraining));
            return new OkObjectResult(incomingTraining);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            TrainingDbModel post = await _repo.GetTraining(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }

    
}