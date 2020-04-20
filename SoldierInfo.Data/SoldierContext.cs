using Microsoft.EntityFrameworkCore;
using SoldierInfo.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoldierInfo.Data
{
    public class SoldierContext : DbContext
    {
        public SoldierContext(DbContextOptions<SoldierContext> options) : base(options)
        {}
        public DbSet<Soldier> Soldiers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoldierBattle>()
                .HasKey(s => new { s.SoldierId, s.BattleId });
        }
    }
}
