using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductFilterWithCountSpec:BaseSpecification<Product>
    {
        public ProductFilterWithCountSpec(ProductSpecParams producparams):
            base(x=>
                (string.IsNullOrEmpty(producparams.Search) || x.Name.ToLower().Contains(producparams.Search)) &&
                (!producparams.BrandID.HasValue || x.ProductBrandId== producparams.BrandID ) &&
                (!producparams.TypeID.HasValue || x.ProductTypeId== producparams.TypeID )
                )
        { }
    }
}
