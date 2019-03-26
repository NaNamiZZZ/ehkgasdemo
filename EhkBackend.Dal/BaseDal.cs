using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EhkBackend.Dal
{
    public class BaseDal<T> where T : class, new()

    {
        //通过工厂DbContextFactory 取得当前上下文
        public DbContext db
        {
            get { return DbContextFactory.GetCurrentDbContext(); }

        }
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return db.Set<T>().Where(whereLambda).AsQueryable();
        }

        public T Add(T Entity)
        {
            db.Set<T>().Add(Entity);
            return Entity;
        }
        public bool update(T Entity)
        {
            db.Entry(Entity).State = EntityState.Modified;
            return true;
        }
        public bool delete(int id)
        {
            var entity = db.Set<T>().Find(id);
            // db.Entry(entity).Property("delflag").IsModified = true;
            db.Set<T>().Remove(entity);
            return true;
        }
        public bool delete(T Entity)
        {
            db.Entry(Entity).State = EntityState.Deleted;
            return true;
        }
        public int DeleteListLogical(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = db.Set<T>().Find(id);
                db.Entry(entity).Property("delflag").CurrentValue = false;
            }
            return ids.Count;
        }
        public int DeleteListLogical(List<string> ids)
        {
            foreach (var id in ids)
            {
                var entity = db.Set<T>().Find(id);
                db.Entry(entity).Property("delflag").CurrentValue = false;
            }
            return ids.Count;

        }
        public int DeleteUserListLogical(List<int> ids)
        {
            foreach (var id in ids)
            {

                var entity = db.Set<T>().Find(id);
                db.Entry(entity).Property("delflag").CurrentValue = false;
            }
            return ids.Count;

        }

        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSzie, out int total, Func<T, bool> whereLambda, bool isAsc, Func<T, S> orderByLambda)
        {

            var temp = db.Set<T>().Where<T>(whereLambda);
            total = temp.Count();//得到總的條數
                                 //排序，獲取當前頁的數據

            if (isAsc)
            {
                temp = temp.OrderBy<T, S>(orderByLambda)
                    .Skip<T>(pageSzie * (pageIndex - 1))//越過多少條
                   .Take<T>(pageSzie).AsQueryable();//取出多少條
            }
            else
            {
                temp = temp.OrderByDescending<T, S>(orderByLambda)
                    .Skip<T>(pageSzie * (pageIndex - 1))//越過多少條
                    .Take<T>(pageSzie).AsQueryable();//取出多少條
            }
            return temp.AsQueryable();
        }

        public IQueryable<T> LoadPageEntities2<S>(int pageIndex, int pageSize, out int total, Func<T, bool> whereLambda,

         bool isAsc, Func<T, S> orderByLambda)
        {

            //EF4.0和上面的查询一样

            //EF5.0

            var temp = db.Set<T>().Where<T>(whereLambda);

            total = temp.Count(); //得到总的条数

            //排序,获取当前页的数据

            if (isAsc)
            {

                temp = temp.OrderBy<T, S>(orderByLambda)

                     .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条

                     .Take<T>(pageSize).AsQueryable(); //取出多少条

            }

            else
            {

                temp = temp.OrderByDescending<T, S>(orderByLambda)

                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条

                    .Take<T>(pageSize).AsQueryable(); //取出多少条

            }

            return temp.AsQueryable();

        }
    }
}
