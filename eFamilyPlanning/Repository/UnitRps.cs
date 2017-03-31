using Model;
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
        private readonly Database db;
        private UserRps<User> userRps;
        private LoginRps<User> loginRps;

        public UnitRps()
        {
            db = new Database("NPocoConStr");
            db.BeginTransaction();
        }

        public UserRps<User> UserRps
        {
            get
            {
                if (userRps == null)
                {
                    userRps = new UserRps<User>(db);
                }
                return userRps;
            }
        }

        public LoginRps<User> LoginRps
        {
            get
            {
                if (loginRps == null)
                {
                    loginRps = new LoginRps<User>(db);
                }
                return loginRps;
            }
        }
        public void ExecuteAndDispose()
        {
            if (db.Transaction != null)
            {
                db.CompleteTransaction();
            }
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
