using AutoMapper;
using NextChallenge.Database;
using NextChallenge.Models;

namespace NextChallenge.App_Start {
    public class MappingProfile : Profile {
        public MappingProfile()
        {
            CreateMap<User, RegisterInput>();


            CreateMap<RegisterInput, User>();

        }
    }
}