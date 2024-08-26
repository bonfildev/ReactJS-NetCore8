using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReactJS_NetCore.Server.Models;

public partial class PersonsDbContext : DbContext
{
    public PersonsDbContext()
    {
    }

    public PersonsDbContext(DbContextOptions<PersonsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PersonsTask> PersonsTasks { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonsTask>(entity =>
        {
            entity.HasKey(e => e.Idtask);

            entity.Property(e => e.Idtask).HasColumnName("IDTask");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Finished).HasDefaultValue(false);
            entity.Property(e => e.RegisterDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
