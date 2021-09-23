using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;


namespace PostMaker.DataAccessLayer
{
    public class BookContext : DbContext
    {
        public DbSet<AuthorBookModel> Names { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionHelper.Con("EFPostMakerDB"));
        }
    }
}
