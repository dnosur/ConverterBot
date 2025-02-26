using ConverterBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConverterBot.Db
{
    internal class Db: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UsersCurrencies> UsersCurrencies => Set<UsersCurrencies>();
        public DbSet<Symbols> Symbols => Set<Symbols>();

        public Db() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=ConverterBotDb;User=root;Password=;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.telegram_id).IsRequired();
            });

            modelBuilder.Entity<Symbols>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.title).IsRequired();
                entity.Property(e => e.description);
            });

            modelBuilder.Entity<UsersCurrencies>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.user_id)
                    .HasColumnName("user_id")
                    .IsRequired();
                entity.Property(e => e.symbol_from_id)
                    .HasColumnName("symbol_from_id")
                    .IsRequired();
                entity.Property(e => e.symbol_to_id)
                    .HasColumnName("symbol_to_id")
                    .IsRequired();

                entity.HasOne(uc => uc.user)
                    .WithMany()
                    .HasForeignKey(uc => uc.user_id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.symbol_from)
                    .WithMany() 
                    .HasForeignKey(uc => uc.symbol_from_id)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uc => uc.symbol_to)
                    .WithMany()
                    .HasForeignKey(uc => uc.symbol_to_id)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
