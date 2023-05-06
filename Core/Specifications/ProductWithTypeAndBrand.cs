using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrand:BaseSpecification<Product>
    {

        public ProductWithTypeAndBrand(ProductSpecParams productprams):
            base(x=>
            (string.IsNullOrEmpty(productprams.Search) || x.Name.ToLower().Contains(productprams.Search))&&
            (!productprams.BrandID.HasValue || x.ProductBrandId == productprams.BrandID) &&
            (!productprams.TypeID.HasValue || x.ProductTypeId == productprams.TypeID)
            )
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productprams.PageSize * (productprams.PageIndex - 1), productprams.PageSize);

            if (!string.IsNullOrEmpty(productprams.Sort))
            {
                switch(productprams.Sort) 
                {
                    case "priceAsc":AddOrderBy(x => x.Price); break;
                    case "priceDes":AddOrderByDesExprsssion(x=>x.Price); break;
                    default:AddOrderBy(x=>x.Name); break;
                }
            }
        }

        public ProductWithTypeAndBrand(int id):base(x=>x.Id==id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }
    }
    
}
