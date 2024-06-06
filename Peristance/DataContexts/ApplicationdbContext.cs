using Domain.Entities.Cities;
using Domain.Entities.Counteries;
using Domain.Entities.States;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.DataContexts
{
    public class ApplicationdbContext:DbContext
    { 
        public ApplicationdbContext(DbContextOptions <ApplicationdbContext>options) : base(options)
        {
        
        }
       public DbSet<Countary> Countaries { get; set; }
       public DbSet<State> States { get; set; }
       public DbSet<City> Cities { get; set; }
    }
}
