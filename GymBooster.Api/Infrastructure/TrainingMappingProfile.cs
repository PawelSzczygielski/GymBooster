using AutoMapper;
using GymBooster.Api.DTO;
using GymBooster.DatabaseAccess.DbModel;
using MongoDB.Bson;

namespace GymBooster.Api.Infrastructure
{
    public class TrainingMappingProfile : Profile
    {
        public TrainingMappingProfile()
        {
            CreateMap<CreateTrainingDTO, CreateTrainingDbModel>()
                .ConstructUsing(dto => new CreateTrainingDbModel(dto.Title))
                .ForAllMembers(expression => expression.Ignore());

            CreateMap<TrainingDbModel, TrainingDTO>()
                .ConstructUsing(model => new TrainingDTO(model.Id.ToString(), model.Title))
                .ForAllMembers(expression => expression.Ignore());

            CreateMap<TrainingDTO, TrainingDbModel>()
                .ConstructUsing(dto => new TrainingDbModel(new ObjectId(dto.Id), dto.Title))
                .ForAllMembers(expression => expression.Ignore());
        }
    }
}