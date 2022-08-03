using AutoMapper;
using EO = InventorySystem.Training.BusinessObjects;
using BO = InventorySystem.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Training.Profiles
{
    public class TrainingProfile : Profile
    {
        public TrainingProfile()
        {
            CreateMap<EO.Product, BO.Product>().ReverseMap();

        }
    }
}
