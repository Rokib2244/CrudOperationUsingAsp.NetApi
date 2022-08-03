using InventorySystem.Data;
using InventorySystem.Training.Repositories;
using InventorySystem.Training.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Training.UnitOfWorks
{
    public class TrainingUnitOfWork : UnitOfWork, ITrainingUnitOfWork
    {
        public IProductRepository Products { get; private set; }
        public TrainingUnitOfWork( TrainingContext context,
            IProductRepository products) : base((DbContext)context)
        {
            Products = products;
        }
    }
}
