using WebStore_2021.ViewModels;

namespace WebStore_2021.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void Add(int id);

        void Decrement(int id);

        void Remove(int id);

        void Clear(int id);

        CartViewModel GetViewModel();
    }
}
