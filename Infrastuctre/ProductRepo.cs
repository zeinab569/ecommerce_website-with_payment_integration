using Core.Entities;
using Core.Interfaces;
using Infrastuctre.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastuctre
{
    public class ProductRepo : IProductRepo
    {
        private readonly StoreContext _context;

        public ProductRepo(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(a=>a.ProductBrand)
                .Include(a=>a.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetByIDAsync(int id)
        {
            return await _context.Products
                .Include(a => a.ProductBrand)
                .Include(a => a.ProductType)
                .FirstOrDefaultAsync(a=>a.Id==id);
        }
    }
}
