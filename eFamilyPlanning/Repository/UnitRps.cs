using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitRps : IDisposable 
    {
        private Database db;
        //private UserRepository<User> userRepository;
        //private LoginRepository<User> loginRepository;

        public UnitRps()
        {
            db = new Database("NPocoConStr");
            db.BeginTransaction();
        }

        //public UserRepository<User> UserRepository
        //{
        //    get 
        //    { 
        //        if(userRepository==null)
        //        {
        //            userRepository = new UserRepository<User>(context);
        //        }
        //        return userRepository;
        //    }
        //}

        //public LoginRepository<User> LoginRepository
        //{
        //    get
        //    {
        //        if (loginRepository == null)
        //        {
        //            loginRepository = new LoginRepository<User>(context);
        //        }
        //        return loginRepository;
        //    }
        //}


        public void Save()
        {
            db.CompleteTransaction();
            Dispose();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
