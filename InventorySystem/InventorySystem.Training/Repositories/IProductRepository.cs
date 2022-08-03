﻿using InventorySystem.Data;
using InventorySystem.Training.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem.Training.Repositories
{
    public interface IProductRepository : IRepository<Product, int>
    {
    }
}
