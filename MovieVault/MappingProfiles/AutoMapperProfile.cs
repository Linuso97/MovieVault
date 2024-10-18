using AutoMapper;
using MovieVault.Data;
using MovieVault.Models.Movies;

namespace MovieVault.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Movie, MoviesReadOnlyVM>();
            CreateMap<Movie, MovieDescriptionVM>().ReverseMap();
        }
    }
}
