﻿using NPoco.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRps<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryProvider<T>, IQueryProvider<T>> orderBy = null, string includeProperties = "");
        void Insert(T t);
        void Delete(object ID);
        void Update(T t);
    }
}
