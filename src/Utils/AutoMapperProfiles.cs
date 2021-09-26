using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;
using VacunaAPI.DTOs;
using VacunaAPI.Entities;

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

            //Working with Latitude
            //CreateMap<DomainObjectCreationDTO, DomainObject>()
            //          .ForMember(x => x.Location, x => x.MapFrom(dto =>
            //           geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
            //CreateMap<DomainObject, DomainObjectDTO>()
            //    .ForMember(x => x.Latitude, dto => dto.MapFrom(field => field.Location.Y))
            //    .ForMember(x => x.Longitude, dto => dto.MapFrom(field => field.Location.X))
            //    ;

            //Map Details from file of another part of class
            //CreateMap<DomainObjectCreationDTO, DomainObject>()
            // .ForMember(x => x.Photo,
            //                 options => options.Ignore())
            // .ForMember(x => x.DomainObjectDetails1DTO, options => options.MapFrom(MapDomianObjectDetails1))
            // .ForMember(x => x.DomainObjectDetails2DTO, options => options.MapFrom(MapDomianObjectDetails2))

            //the reserse part of the one before 
            //CreateMap<DomainObject, DomainObjectDTO>()
            //            .ForMember(x => x.Details1, options => options.MapFrom(MapDomainObjectDetails1))
            //            .ForMember(x => x.Details2, options => options.MapFrom(MapDomainObjectDetails2)) 

        }




    }
}
