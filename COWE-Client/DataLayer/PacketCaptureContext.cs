using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;

namespace COWE.DataLayer
{
    public class PacketCaptureContext:DbContext
    {
        public DbSet<CapturePacket> CapturePackets { get; set; }
        public DbSet<CumulativeInterval> CumulativeIntervals { get; set; }
        public DbSet<CaptureBatch> CaptureBatches { get; set; }
        public DbSet<BatchInterval> BatchIntervals { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CapturePacket>().HasKey(c => c.CapturePacketId);
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
