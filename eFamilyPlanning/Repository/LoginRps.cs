using Model;
using NPoco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class LoginRps<T> : Rps<T> where T : class
    {
        //HttpSessionState Session = HttpContext.Current.Session;
        private Database db;
        public LoginRps(Database context) : base(context)
        {
            this.db = context;
        }

        public IEnumerable<Menu> GetMenu(string name)
        {
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT m.*,r.[Description] FROM [User] AS u INNER JOIN UserRole AS ur ON u.ID=ur.UID ");
            stb.Append("INNER JOIN [Role] AS r ON ur.RID=r.ID INNER JOIN RoleMenu AS rm ");
            stb.Append("ON rm.RID=r.ID INNER JOIN Menu AS m ON m.ID=rm.MID WHERE u.Name=@0");

            return db.Query<Menu>(stb.ToString(), new SqlParameter("@0", name)).ToList();
        }

        public IEnumerable<dynamic> GetMenuAll()
        {
             
            StringBuilder stb = new StringBuilder();
            stb.Append("SELECT m.*,r.[Description] FROM [User] AS u INNER JOIN UserRole AS ur ON u.ID=ur.UID ");
            stb.Append("INNER JOIN [Role] AS r ON ur.RID=r.ID INNER JOIN RoleMenu AS rm ");
            stb.Append("ON rm.RID=r.ID INNER JOIN Menu AS m ON m.ID=rm.MID");

            return db.Query<dynamic>(stb.ToString()).ToList();
        }
    
    }
}
