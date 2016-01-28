using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COWE.DataLayer
{
    public class UnitOfWorkCumulativeProbability: IDisposable
    {
        private readonly CumulativeProbabilityContext _context;

        public UnitOfWorkCumulativeProbability()
        {
            _context = new CumulativeProbabilityContext();
        }

        public UnitOfWorkCumulativeProbability(CumulativeProbabilityContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        internal CumulativeProbabilityContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
