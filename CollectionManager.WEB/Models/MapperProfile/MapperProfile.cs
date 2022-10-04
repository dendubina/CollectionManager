using System;
using AutoMapper;
using CollectionManager.WEB.Models.Collections;
using CollectionManager.WEB.Models.CustomFieldValues;
using CollectionManager.WEB.Models.Items;
using Entities.DTO.Collections;
using Entities.DTO.CustomFields;
using Entities.DTO.Items;
using Entities.EF.Models;

namespace CollectionManager.WEB.Models.MapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CollectionToManipulateDto, Collection>().ReverseMap();

            CreateMap<Collection, UsersCollectionToShow>()
                .ForMember(x => x.ItemsCount, opt => opt.MapFrom(x => x.Items.Count));

            CreateMap<Collection, CollectionDetailsToShow>()
                .ForMember(x => x.ItemInCollectionDetails, opt => opt.MapFrom(x => x.Items));

            CreateMap<Collection, ItemToCreate>()
                .ForMember(x => x.Name, opt => opt.Ignore())
                .ForMember(x => x.CollectionId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.CustomFieldValuesToCreate, opt => opt.MapFrom(x => x.CustomFields));


            CreateMap<CustomFieldToManipulateDto, CustomField>()
                .ForMember(x => x.FieldType, opt => opt.MapFrom(x => x.Type))
                .ReverseMap();

            CreateMap<CustomField, CustomFieldValueToCreate>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.FieldType))
                .ForMember(x => x.CustomFieldId, opt => opt.MapFrom(x => x.Id));

            CreateMap<CustomFieldValueToCreate, CustomFieldValue>();

            CreateMap<CustomFieldValue, CustomFieldValueToShow>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.CustomField.Name));

            CreateMap<Item, ItemInCollectionDetails>()
                .ForMember(x => x.CustomFieldValues, opt => opt.MapFrom(x => x.CustomFieldsValues));

            CreateMap<ItemToCreate, Item>()
                .ForMember(x => x.CustomFieldsValues, opt => opt.MapFrom(x => x.CustomFieldValuesToCreate));
        }
    }
}
