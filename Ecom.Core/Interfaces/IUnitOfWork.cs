

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public ICustomerCartRepository CustomerCartRepository { get; }
        public IAuth AuthRepository { get; }
        public IWishListRepository wishListRepository { get; }
    }
}
