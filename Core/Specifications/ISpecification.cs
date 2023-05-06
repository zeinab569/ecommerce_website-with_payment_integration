using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
      
        // add function for filter
        Expression<Func<T, bool>> Criteria{ get; }
        //add function to include
        List<Expression<Func<T, object>>> Includes { get;}
        //Sorting
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesending { get; }
        int Take { get; }
        int Skip { get; }
        bool ispagingEnable { get; }
    }
}
