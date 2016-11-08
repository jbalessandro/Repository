using System.Linq;

namespace Domain.Abstract
{
    public interface IBaseService<T> where T: class
    {
        IQueryable<T> Listar();
        T Gravar(T item);
        T Excluir(int id);
        T Find(int id);
    }
}
