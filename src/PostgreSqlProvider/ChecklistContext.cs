using System;
using System.Linq;
using ChecklistDomainModel.Model;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace PostgreSqlProvider
{
	public class ChecklistContext : DbContext
	{
		public DbSet<Checklist> Checklists { get; set; }

		public DbSet<ChecklistField> ChecklistFields { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Checklist>().HasKey(m => m.Id);
			builder.Entity<ChecklistField>().HasKey(m => m.Id);

			// shadow properties
			builder.Entity<Checklist>().Property<DateTime>("UpdatedTimestamp");
			builder.Entity<ChecklistField>().Property<DateTime>("UpdatedTimestamp");

			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder().AddJsonFile("../config.json").AddEnvironmentVariables();
			var configuration = builder.Build();

			var sqlConnectionString = configuration["DataAccessPostgreSqlProvider:ConnectionString"];

			optionsBuilder.UseNpgsql(sqlConnectionString);
		}

		public override int SaveChanges()
		{
			this.ChangeTracker.DetectChanges();

			this.UpdateUpdatedProperty<Checklist>();
			this.UpdateUpdatedProperty<ChecklistField>();

			return base.SaveChanges();
		}

		private void UpdateUpdatedProperty<T>() where T : class
		{
			var modifiedSourceInfo =
				this.ChangeTracker.Entries<T>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

			foreach (var entry in modifiedSourceInfo)
			{
				entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
			}
		}
	}
}