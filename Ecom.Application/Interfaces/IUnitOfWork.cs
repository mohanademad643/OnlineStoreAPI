

namespace Ecom.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IBrandRepository BrandRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPhotoRepository PhotoRepository { get; }
        public ICustomerCartRepository CustomerCartRepository { get; }
        public IAuth AuthRepository { get; }
        public IUsersRepository usersRepository { get; }
        public IWishListRepository wishListRepository { get; }
    }
}
