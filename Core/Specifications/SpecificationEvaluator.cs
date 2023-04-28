using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class SpecificationEvaluator<IEntity> where IEntity : BaseEntity
    {
        public static IQueryable<IEntity> Getquery(IQueryable<IEntity> inputquery,ISpecification<IEntity> spec)
        {
            var query = inputquery;
            //has data
            if (spec.Criteria != null)
            {
                query= query.Where(spec.Criteria);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
