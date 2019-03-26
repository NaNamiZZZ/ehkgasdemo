using EhkBackend.IDal;
using EhkBackend.staticDalFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.BLL
{
   public abstract class BaseService<T> where T:class,new()
    {
        public IBaseDAL<T> CurrentDal { get; set; }

        public IDbsession dbsession
        {
            get { return DbSessionFactory.GetCurrentDbSession(); }
        }
        public BaseService()
        {
            SetCurrentDal();
        }
        public abstract void SetCurrentDal();
        public T Add(T Entity)
        {
            CurrentDal.Add(Entity);
            dbsession.SaveChanges();
            return Entity;

        }
        public bool update(T Entity)
        {
            CurrentDal.update(Entity);
            return dbsession.SaveChanges() > 0;

        }

        public bool delete(T Entity)
        {
            CurrentDal.delete(Entity);
            return dbsession.SaveChanges() > 0;

        }
        public bool delete(int id)
        {
            CurrentDal.delete(id);
            return dbsession.SaveChanges() > 0;

        }
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.delete(id);
            }
            return dbsession.SaveChanges();
        }
        public int DeleteUserListLogical(List<int> ids)
        {
            CurrentDal.DeleteUserListLogical(ids);
            return dbsession.SaveChanges();
        }
        public int DeleteListLogical(List<int> ids)
        {
            CurrentDal.DeleteListLogical(ids);
            return dbsession.SaveChanges();
        }
        public int DeleteListLogical(List<string> ids)
        {
            CurrentDal.DeleteListLogical(ids);
            return dbsession.SaveChanges();
        }

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);

        }
        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {
            return CurrentDal.LoadPageEntities2(pageIndex, pageSize, out total, whereLambda, isAsc, orderByLambda);

        }
        public IQueryable<T> LoadPageEntities2<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda,

        bool isAsc, Func<T, S> orderByLambda)
        {
            return CurrentDal.LoadPageEntities2(pageIndex, pageSize, out total, whereLambda, isAsc, orderByLambda);
        }
    }
}
