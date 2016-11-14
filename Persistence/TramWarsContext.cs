using Microsoft.EntityFrameworkCore;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TramWars.Persistence
{
    public class TramWarsContext : DbContext
    {
        public TramWarsContext(DbContextOptions options) : base(options) {}

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
