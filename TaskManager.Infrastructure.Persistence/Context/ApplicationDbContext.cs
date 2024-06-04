using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Domain.Entities;
using TaskManager.Core.Domain.Common;
using TaskManager.Common;


namespace TaskManager.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly UserContext _userContext;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, UserContext userContext) : base(options)
        {
            _userContext = userContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("dbo");
        }

        public DbSet<User> User { get; set; }
        public DbSet<Tasks> Task { get; set; }


        private void CustomSave()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedById = _userContext != null ? _userContext.Id : 0;
                        entry.Entity.Created = Server.GetDate();

                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedById = _userContext != null ? _userContext.Id : 0;
                        entry.Entity.LastModified = Server.GetDate();
                        break;
                }
            }

        }

        public override int SaveChanges()
        {
            CustomSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            CustomSave();
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
