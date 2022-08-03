using AutoMapper;
using InventorySystem.Api.Models;
using InventorySystem.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Api.Profiles
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<CreateProductModel, Product>().ReverseMap();
            CreateMap<UpdateProductModel, Product>().ReverseMap();
        }
    }
}
