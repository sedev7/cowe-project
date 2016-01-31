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
        //private readonly PacketCaptureContext _context;

        public UnitOfWorkCumulativeProbability()
        {
            _context = new CumulativeProbabilityContext();
            //_context = new PacketCaptureContext();
        }

        public UnitOfWorkCumulativeProbability(CumulativeProbabilityContext context)
        //public UnitOfWorkCumulativeProbability(PacketCaptureContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        //internal CumulativeProbabilityContext Context
        public CumulativeProbabilityContext Context
        //public PacketCaptureContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
