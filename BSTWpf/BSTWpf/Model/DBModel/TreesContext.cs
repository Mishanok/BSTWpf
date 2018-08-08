namespace BSTWpf.Model.DBModel
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TreesContext : DbContext
    {
        
        public TreesContext()
            : base("name=TreesContext")
        {
        }

        public virtual DbSet<Tree> Trees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tree>().HasKey(b => b.ID);
            modelBuilder.Ignore<Node>();
        }
    }
}