using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace PostMaker.DataAccessLayer
{
    public class DataAccess
    {
        public List<AuthorBookModel> GetNames()
        {
            //using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.Con("AutBookTableDb")))
            //{
            //    var data = connection.Query<AuthorBookModel>("SELECT AuthorName, BookName, LastUse FROM AutBookTableDb").ToList();
            //    return data;
            //}

            using (var context = new BookContext())
            {
                return context.Names.ToList();
            }
        }

        public void SaveName(AuthorBookModel name)
        {
            //using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.Con("AutBookTableDb")))
            //{
            //    List<AuthorBookModel> names = new List<AuthorBookModel>();
            //    names.Add(new AuthorBookModel { AuthorName = "bla", BookName = "goo", LastUse = DateTime.Today.Date });

            //    connection.Execute("dbo.SaveNameProcedure @AuthorName, @BookName, @LastUse", names);
            //}
            using (var context = new BookContext())
            {
                context.Names.Add(name);
                context.SaveChanges();
            }
        }

        public void Update(AuthorBookModel model)
        {
            using (var context = new BookContext())
            {
                context.Names.Update(model);

                context.SaveChanges();

            }
        }
    }
}
