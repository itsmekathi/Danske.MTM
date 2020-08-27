using Danske.MTM.Persistence.Models.MTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Danske.MTM.Persistence.Context
{
    public partial class MTMContext : DbContext
    {
        private IConfiguration _configuration;
        public MTMContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MTMContext(DbContextOptions<MTMContext> options, string isTest)
            : base(options)
        {
        }

        public virtual DbSet<MunicipalityTaxSchedules> MunicipalityTaxSchedules { get; set; }
        public virtual DbSet<TaxScheduleTypes> TaxScheduleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration["DatabaseConnectionString"]);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MunicipalityTaxSchedules>(entity =>
            {
                entity.HasIndex(e => new { e.TaxSheduleTypeId, e.MunicipalityName, e.FromDate, e.Todate })
                    .HasName("UC_TaxSheduleTypeID_MunicipalityName_FromDate_TODate")
                    .IsUnique();

                entity.HasOne(d => d.TaxSheduleType)
                    .WithMany(p => p.MunicipalityTaxSchedules)
                    .HasForeignKey(d => d.TaxSheduleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Municipal__TaxSh__3C69FB99");
            });

            modelBuilder.Entity<TaxScheduleTypes>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UC_Name")
                    .IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
