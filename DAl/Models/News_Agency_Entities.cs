using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAl
{
    public class News_Agency_Entities : DbContext
    {
        //public News_Agency_Entities():base("News_Agency_Entities")
        //{

        //}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //throw new UnintentionalCodeFirstException();
        //}

        public virtual DbSet<PageGroup> PageGroups { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageComment> PageComments { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
