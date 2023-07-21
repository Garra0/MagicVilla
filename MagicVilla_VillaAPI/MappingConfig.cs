using System;
using AutoMapper;
using MagicVilla_VillaAPI.models;
using MagicVilla_VillaAPI.models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //The MappingConfig class is responsible for configuring
            //the mappings between different types of objects using
            //AutoMapper. AutoMapper is a library that simplifies the
            //mapping process between different classes.

            //CreateMap<Source, Destination>();
            // i can write one or 2 lines do seem work:
            //1-
            //CreateMap<Villa, VillaDTO>().ReverseMap();
            //2-
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();


            // Villa Number
            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();


            CreateMap<ApplicationUser,UserDTO>().ReverseMap();
        }
    }
}

