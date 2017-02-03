using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eFamilyPlanning.Models;
using System.Text;
using System.Data.SqlClient;

namespace eFamilyPlanning.Repositories
{
    public class LoginRepository<T> : Repository<T> where T : class
    {
        private Database db;
        public LoginRepository(Database context) : base(context)
        {
            this.db = context;
        }

        public IEnumerable<Menu> GetMenu(string name)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT m.*,r.[Description] FROM [User] AS u INNER JOIN UserRole AS ur ON u.ID=ur.UID ");
            stb.Append("INNER JOIN [Role] AS r ON ur.RID=r.ID INNER JOIN RoleMenu AS rm ");
            stb.Append("ON rm.RID=r.ID INNER JOIN Menu AS m ON m.ID=rm.MID WHERE u.Name=@0");

            return  db.Query<Menu>(stb.ToString(), new SqlParameter("@0", name)).ToList();
        }
    }
}