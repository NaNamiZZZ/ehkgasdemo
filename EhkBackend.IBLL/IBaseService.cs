using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        T Add(T Entity);
        bool update(T Entity);
        bool delete(T Entity);
        bool delete(int id);
        int DeleteListLogical(List<int> ids);
        int DeleteListLogical(List<string> ids);
        int DeleteUserListLogical(List<int> ids);
        IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
        IQueryable<T> LoadPageEntities2<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda);
    }
}
