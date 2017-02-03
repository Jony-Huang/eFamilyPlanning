
using eFamilyPlanning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPoco;

namespace eFamilyPlanning.Repositories
{
    public class UnitRepository : IDisposable 
    {
        private Database context;
        private Transaction tran;
        private UserRepository<User> userRepository;
        private LoginRepository<User> loginRepository;

        public UnitRepository()
        {
            context = new Database("NPocoConStr");
            tran = (Transaction)context.GetTransaction();
        }

        public UserRepository<User> UserRepository
        {
            get 
            { 
                if(userRepository==null)
                {
                    userRepository = new UserRepository<User>(context);
                }
                return userRepository;
            }
        }

        public LoginRepository<User> LoginRepository
        {
            get
            {
                if (loginRepository == null)
                {
                    loginRepository = new LoginRepository<User>(context);
                }
                return loginRepository;
            }
        }


        public void Save()
        {
            tran.Complete();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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