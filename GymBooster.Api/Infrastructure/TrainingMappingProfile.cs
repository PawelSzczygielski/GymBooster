using AutoMapper;
using GymBooster.Api.DTO;
using GymBooster.DatabaseAccess.DbModel;

namespace GymBooster.Api.Infrastructure
{
    public class TrainingMappingProfile : Profile
    {
        public TrainingMappingProfile()
        {
            CreateMap<CreateTrainingDTO, CreateTrainingDbModel>()
                .ConstructUsing(dto => new CreateTrainingDbModel(dto.Title, dto.Content));

            CreateMap<TrainingDbModel, TrainingDTO>()
                .ConstructUsing(model => new TrainingDTO(model.Id, model.Title, model.Content));
        }
    }
}