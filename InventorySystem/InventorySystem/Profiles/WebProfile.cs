using AutoMapper;
using InventorySystem.Areas.Admin.Models;
using InventorySystem.Training.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventorySystem.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<AddProductModel, Product>().ReverseMap();
        }
    }
}
