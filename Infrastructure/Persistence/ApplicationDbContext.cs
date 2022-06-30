using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IUserRequest _usuarioRequest;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserRequest usuarioRequest) : base(options)
        {
            _usuarioRequest = usuarioRequest;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


        private void AddCampoDeControle()
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityAuditavel> entry in ChangeTracker.Entries<EntityAuditavel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateBy = string.IsNullOrEmpty(_usuarioRequest.Email) ? "sistema": _usuarioRequest.Email;
                        entry.Entity.CreateAt = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdateBy = string.IsNullOrEmpty(_usuarioRequest.Email) ? "sistema" : _usuarioRequest.Email;
                        entry.Entity.UpdateAt = DateTime.UtcNow;
                        break;
                }
            }
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddCampoDeControle();

            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
