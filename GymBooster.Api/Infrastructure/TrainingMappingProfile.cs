using System.Collections.Generic;
using AutoMapper;
using GymBooster.Api.DTO;
using GymBooster.DatabaseAccess.DbModel;

namespace GymBooster.Api.Infrastructure
{
    public class TrainingMappingProfile : Profile
    {
        public TrainingMappingProfile(IMapper mapper)
        {
            CreateMap<TrainingDbModel, TrainingDTO>()
                .ConstructUsing(model => new TrainingDTO(model.Id.ToString(), model.Title))
                .ForAllMembers(expression => expression.Ignore());

            CreateMap<TrainingDTO, TrainingDbModel>()
                .ConstructUsing(dto => new TrainingDbModel(dto.Id, dto.Title)
                {
                    Excercises = mapper.Map<List<ExcerciseDbModel>>(dto.Excercises)
                })
                .ForAllMembers(expression => expression.Ignore());
        }
    }
}