﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        public int _pagesize = 6;
        public int PageSize 
        {
            get => _pagesize; 
            set => _pagesize = (value > MaxPageSize) ? MaxPageSize : value; 
        }
        public int? BrandID { get; set; }
        public int? TypeID { get; set; }
        public string Sort { get; set;}

        private string _search;
        public string Search
        {
            get=> _search;
            set => _search = value.ToLower();
        }
    }
}
