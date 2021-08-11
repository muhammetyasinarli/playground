using Products.Data;
using Products.Entities;
using Products.Repositories.Base;
using Products.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ProductContext dbContext) : base(dbContext)
        {

        }
    }
}
