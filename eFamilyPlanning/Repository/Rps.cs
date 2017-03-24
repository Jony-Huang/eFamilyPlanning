using NPoco;
using NPoco.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Rps<T> : IRps<T> where T : class
    {
        private readonly Database db;
        public Rps(Database context)
        {
            this.db = context;
        }

        //public Database Context { get { return this.context; } }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryProvider<T>, IQueryProvider<T>> orderBy = null, string includeProperties = "")
        {
            var query = db.Query<T>();
            if (filter != null)
            {
                query.Where(filter);
            }
            
            //foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include()
            //}

            if (orderBy != null)
            {
                return orderBy(query).ToList();

            }
            //else
            //{
            //    return query.ToList();
            //}
            return query.ToList();
        }

        public void Insert(T t)
        {
            this.db.Insert<T>(t);
        }

        public void Delete(object ID)
        {
            this.db.Delete<T>(ID);
        }

        public virtual void Delete(T t)
        {
            db.Delete<T>(t);
        }

        public void Update(T t)
        {
            this.db.Update(t);
        }
    }
}
