using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Thitipong.Upload.Model;

namespace Thitipong.Upload.Service.Data
{
  
  public class AppDb : DbContext
  {
    public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
      //
    }

    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }

  }
}
