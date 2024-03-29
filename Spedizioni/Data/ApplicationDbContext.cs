﻿using Microsoft.EntityFrameworkCore;
using Spedizioni.Models;

namespace Spedizioni.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Spedizione> Spedizione { get; set; } = default!;
        public DbSet<DettagliSpedizione> DettagliSpedizione { get; set; }


        // viene utilizzato per configurare il modello di entità che entity framework Core utilizzerà per creare il db
        // in questo caso si vuole che il campo username sia univoco e corregge il problema che avevo prima
        // non riuscivo a leggere lo username role perché non era univoco e andava in confusione
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Spedizione>()
                .HasOne(s => s.Cliente)
                .WithMany(c => c.Spedizioni)
                .HasForeignKey(s => s.IdCliente);

            modelBuilder.Entity<DettagliSpedizione>()
                .HasOne(d => d.Spedizione)
                .WithMany(s => s.DettagliSpedizione)
                .HasForeignKey(d => d.IdSpedizione);
        }


    }
}
