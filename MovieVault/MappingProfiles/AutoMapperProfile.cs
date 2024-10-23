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
            CreateMap<Movie, MovieDescriptionVM>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ReverseMap();
            CreateMap<MovieApiResponse, MovieDescriptionVM>();
        }
    }
}
