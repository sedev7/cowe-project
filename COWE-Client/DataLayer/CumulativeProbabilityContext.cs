using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COWE.DomainClasses;

namespace COWE.DataLayer
{
    public class CumulativeProbabilityContext: DbContext
    {
        public DbSet<CumulativeProbabilityDistribution> CumulativeProbabilityDistributions { get; set; }
    }
}
