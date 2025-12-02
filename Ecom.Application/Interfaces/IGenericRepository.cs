

using System.Linq.Expressions;

namespace Ecom.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id ,params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync(); 
    }
}
/*
 EcommerceSolution
│
└── src
    ├── Ecom.API
    │   ├── Controllers
    │   │   ├── ProductsController.cs
    │   │   ├── BrandsController.cs
    │   │   └── ...
    │   ├── Middleware
    │   ├── Mapping
    │   │   └── MappingProfile.cs
    │   ├── appsettings.json
    │   └── Program.cs
    │
    ├── Ecom.Application
    │   ├── Common
    │   │   ├── Behaviors
    │   │   │   └── ValidationBehavior.cs
    │   │   └── Interfaces
    │   │       └── IProductRepository.cs
    │   │
    │   ├── Products
    │   │   ├── Commands
    │   │   │   ├── CreateProduct
    │   │   │   │   ├── CreateProductCommand.cs
    │   │   │   │   ├── CreateProductCommandHandler.cs
    │   │   │   │   └── CreateProductCommandValidator.cs
    │   │   │   ├── UpdateProduct
    │   │   │   └── DeleteProduct
    │   │   ├── Queries
    │   │   │   ├── GetAllProducts
    │   │   │   │   ├── GetAllProductsQuery.cs
    │   │   │   │   └── GetAllProductsQueryHandler.cs
    │   │   │   └── GetProductById
    │   │   └── Dtos
    │   │       └── ProductDto.cs
    │   │
    │   ├── Brands
    │   │   ├── Commands
    │   │   └── Queries
    │   └── DependencyInjection.cs
    │
    ├── Ecom.Domain
    │   ├── Entities
    │   │   ├── Product.cs
    │   │   ├── Brand.cs
    │   │   └── Category.cs
    │   ├── ValueObjects
    │   ├── Events
    │   └── Common
    │       └── BaseEntity.cs
    │
    └── Ecom.Infrastructure
        ├── Data
        │   └── EcomDbContext.cs
        ├── Repositories
        │   ├── ProductRepository.cs
        │   └── BrandRepository.cs
        ├── Migrations
        ├── DependencyInjection.cs
        └── Services
 
 
 
 */