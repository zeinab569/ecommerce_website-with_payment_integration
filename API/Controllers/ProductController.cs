
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastuctre.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       // private readonly IProductRepo _productrepository;
        private readonly IGenericRepo<ProductBrand> _brandRepository;
        private readonly IGenericRepo<ProductType> _typeRepository;
        private readonly IGenericRepo<Product> _productRepository;
        private IMapper _mapper;

        public ProductController( IGenericRepo<ProductBrand> brandRepository, IGenericRepo<ProductType> typeRepository, IGenericRepo<Product> productRepository, IMapper mapper)
        {
           // _productrepository = productrepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrand();
            var products = await _productRepository.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));

            //return products.Select(Product => new ProductToReturnDto()
            //{
            //    Id = Product.Id,
            //    Description = Product.Description,
            //    Name = Product.Name,
            //    PictureUrl = Product.PictureUrl,
            //    Price = Product.Price,
            //    ProductBrand = Product.ProductBrand.Name,
            //    ProductType = Product.ProductType.Name
            //}).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrand(id);
            var product = await _productRepository.GetWithSpec(spec);
            return _mapper.Map<Product,ProductToReturnDto>(product);

            //return new ProductToReturnDto()
            //{
            //    Id=product.Id,
            //    Description = product.Description,
            //    Name = product.Name,
            //    PictureUrl = product.PictureUrl,
            //    Price = product.Price,
            //    ProductBrand= product.ProductBrand.Name,
            //    ProductType = product.ProductType.Name

            //};
        }

        [HttpGet ("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _typeRepository.GetListAsync());
        }
        [HttpGet ("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {
            return Ok(await _brandRepository.GetListAsync());
        }


    }
}
