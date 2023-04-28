using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> GetByIDAsync(int id);
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetAllTypesAsync();
    }
}
