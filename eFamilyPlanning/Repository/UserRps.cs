using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class UserRps<T> : Rps<T> where T : class
    {
        private Database db;
        public UserRps(Database db): base(db)
        {
            this.db = db;
        }

        //public IEnumerable<User> GetHa()
        //{
        //    return context.Query<User>().ToList();
        //}
    
    }
}
