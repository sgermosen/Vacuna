using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;
using NetTopologySuite.Geometries; 

namespace VacunaAPI.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
               CreateMap<Inmunization, InmunizationDTO>().ReverseMap();
            CreateMap<InmunizationCreationDTO, Inmunization>()
                .ForMember(x => x.CardPicture,
                                options => options.Ignore());//  we ignore one or various properties than we want to treat as a diferent way  
          
            CreateMap<IdentityUser, UserDTO>();
        }
         



    }
}
