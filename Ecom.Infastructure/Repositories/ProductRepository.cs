

using AutoMapper;
using Ecom.Application.DTOs;
using Ecom.Application.Common.Filters;
using Ecom.Application.Interfaces;
using Ecom.Application.Sharing;
using Ecom.Core.Services;
using Ecom.Domain.Entities.Products;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Ecom.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext Context, IMapper mapper, IImageManagementService imageManagementService) : base(Context)
        {
            context = Context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include(x => x.Photos)
                .AsNoTracking()
                .ToListAsync();
        }

        //public async Task<ReturnProductDTO> GetAllAsync(ProductParams productParams)
        //{
        //    var Query =  context.Products
        //        .Include(x=>x.Category)
        //        .Include(x=>x.Brand)
        //        .Include(x=>x.Photos)
        //        .AsNoTracking();
        //    ////Filter By Word
        //    //if (!string.IsNullOrEmpty(productParams.Search))
        //    //{
        //    //    //Query = Query
        //    //    //    .Where(x=>x.Name.ToLower().Contains(productParams.Search.ToLower())
        //    //    //     || x.Description.ToLower().Contains(productParams.Search.ToLower()));

        //    //    var searchWords = productParams.Search.Split(' ');
        //    //    Query = Query.Where(m => searchWords.All(word =>

        //    //        m.Name.ToLower().Contains(word) ||
        //    //        m.Description.ToLower().Contains(word)
        //    //    ));
        //    //}

        //    ////Filter By CategoryId
        //    //if (productParams.CategoryId.HasValue)
        //    //{
        //    //    Query = Query.Where(x => x.CategoryId == productParams.CategoryId);
        //    //}
        //    ////Filter By BrandId
        //    //if (productParams.BrandId.HasValue)
        //    //{
        //    //    Query = Query.Where(x => x.BrandId == productParams.BrandId);
        //    //}
        //    ////Filter By BrandId
        //    //if (productParams.Rating.HasValue)
        //    //{
        //    //    Query = Query.Where(x => x.rating == productParams.Rating);
        //    //}
        //    //if (!string.IsNullOrEmpty(productParams.Sort))
        //    //{
        //    //    Query = productParams.Sort switch
        //    //    {
        //    //        "PriceAsn" => Query.OrderBy(x => x.NewPrice),
        //    //        "PriceDec" => Query.OrderByDescending(x => x.NewPrice),
        //    //        _ => Query.OrderBy(x => x.Name),
        //    //    };
        //    //}

        //    var rules = new List<IProductFilter>
        //    {
        //        new SearchFilter(),
        //        new CategoryFilter(),
        //        new BrandFilter(),
        //        new RatingFilter(),
        //        new SortingFilter()
        //     };


        //    foreach (var rule in rules)
        //    {
        //        Query =  rule.Apply(Query, productParams);
        //    }
        //    var returnProductDTO =  new ReturnProductDTO();
        //    returnProductDTO.TotalCount =  Query.Count();
        //    Query = Query.Skip((productParams.pageSize) *(productParams.PageNumber - 1))
        //        .Take(productParams.pageSize);

        //    returnProductDTO.products = _mapper.Map<List<ProductDTO>>(Query);
        //    return returnProductDTO; 
        //}




        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

        }
        public async Task UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();

        }

        //public async Task<bool> AddAsync(AddProductDTO productDTO)
        //{
        //    if (productDTO is null) return false;

        //    var product = _mapper.Map<Product>(productDTO);
        //   await context.Products.AddAsync(product);

        //    await context.SaveChangesAsync();

        //    var ImagePath = await _imageManagementService.AddImageAsync(productDTO.Photo, productDTO.Name);

        //    var photo = ImagePath.Select(path=> new Photo
        //    {
        //        ImageName = path,
        //        ProductId = product.Id,

        //    }).ToList();
        //    await context.Photos.AddRangeAsync(photo);
        //    await context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        //{
        //    if (updateProductDTO is null)
        //    {
        //        return false;
        //    }

        //    var FindProduct = await context.Products
        //        .Include(m => m.Category)
        //        .Include(m => m.Photos).FirstOrDefaultAsync(m=>m.Id == updateProductDTO.Id);

        //    if (FindProduct is null)
        //    {
        //        return false;
        //    }

        //    _mapper.Map(updateProductDTO, FindProduct);

        //    var FindPhoto = await context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();
        //    foreach (var item in FindPhoto)
        //    {
        //        _imageManagementService.DeleteImageAsync(item.ImageName);
        //    }
        //    context.Photos.RemoveRange(FindPhoto);

        //    var ImagePath = await _imageManagementService.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
        //    var photo = ImagePath.Select(path => new Photo
        //    {
        //        ImageName = path,
        //        ProductId = updateProductDTO.Id
        //    }).ToList();

        //    await context.Photos.AddRangeAsync(photo);
        //    await context.SaveChangesAsync();

        //    return true;
        //}

        //public async Task DeleteAsync(Product product)
        //{
        //    var Photo = await context.Photos.Where(m => m.ProductId == product.Id).ToListAsync();
        //    foreach (var item in Photo)
        //    {
        //        _imageManagementService.DeleteImageAsync(item.ImageName);
        //    }

        //    context.Products.Remove(product);
        //    await context.SaveChangesAsync();

        //}
        public async Task DeleteAsync(Product product)
{
    context.Products.Remove(product);
    await context.SaveChangesAsync();
}
        public async Task RemoveProductImagesAsync(int productId)
        {
            var photos = await context.Photos
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            foreach (var img in photos)
                _imageManagementService.DeleteImageAsync(img.ImageName);

            context.Photos.RemoveRange(photos);
            await context.SaveChangesAsync();
        }
    }
}
