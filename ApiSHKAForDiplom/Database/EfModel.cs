using ApiSHKAForDiplom.Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiSHKAForDiplom.Database
{
    public class EfModel:DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.UserLogin).IsUnique();
            //modelBuilder.Entity<User>().HasKey(u => u.UserId);
            //modelBuilder.Entity<User>()..WithMany().HasForeignKey(u => u.UserBalanceRef);
            //modelBuilder.Entity<UsersBank>().HasKey(a => a.UsersBankRef);
        }

        public EfModel(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<User> UserR { get; set; }
        public virtual DbSet<UsersBank> UsersBankK { get; set; }
        public virtual DbSet<Worker> WorkerR { get; set; }
        public virtual DbSet<UserInform> UserInformM { get; set; }
        public virtual DbSet<Credit> CreditT { get; set; }
    }
}
