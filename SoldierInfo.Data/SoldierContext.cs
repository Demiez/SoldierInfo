using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SoldierInfo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoldierInfo.Data
{
    public class SoldierContext : DbContext
    {
        public DbSet<Soldier> Soldiers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public SoldierContext(DbContextOptions<SoldierContext> options) : base(options)
        {
            //SeedSoldiers();
        }

        private void SeedSoldiers()
        {
            Soldiers.Add(new Soldier { Id = 1, Name = "Ivan" });
            Soldiers.Add(new Soldier { Id = 2, Name = "Vasyl" });
        }

        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoldierBattle>()
                .HasKey(s => new { s.SoldierId, s.BattleId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(GetLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        public List<Soldier> getSoldiers() => Soldiers.Local.ToList<Soldier>();
    }
}
