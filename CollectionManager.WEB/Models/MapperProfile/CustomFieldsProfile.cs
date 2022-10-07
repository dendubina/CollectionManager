﻿using AutoMapper;
using Entities.DTO.CustomFields;
using Entities.DTO.CustomFieldValues;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class CustomFieldsProfile : Profile
    {
        public CustomFieldsProfile()
        {
            CreateMap<CustomFieldToManipulateDto, CustomField>()
                .ForMember(x => x.FieldType, opt => opt.MapFrom(x => x.Type))
                .ReverseMap();

            CreateMap<CustomField, CustomFieldValueToManipulateDto>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.FieldType))
                .ForMember(x => x.CustomFieldId, opt => opt.MapFrom(x => x.Id));

            CreateMap<CustomFieldValue, CustomFieldValueToManipulateDto>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Field.FieldType))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Field.Name))
                .ForMember(x => x.CustomFieldId, opt => opt.MapFrom(x => x.Field.Id));

            CreateMap<CustomFieldValueToManipulateDto, CustomFieldValue>();

            CreateMap<CustomFieldValue, CustomFieldValueToReturnDto>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Field.Name));
        }
    }
}
