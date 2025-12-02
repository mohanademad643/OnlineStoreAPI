using Ecom.Application.DTOs;
using Ecom.Application.Features.Products.Queries.GetAllProducts;
using Ecom.Application.ValidationBehavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Ecom.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddScoped<IRequestHandler<GetAllProductsQuery, ReturnProductDTO>, GetAllProductsHandler>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
