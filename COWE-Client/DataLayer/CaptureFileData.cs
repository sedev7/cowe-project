using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;
using COWE.Enumerations;

namespace COWE.DataLayer
{
    public class CaptureFileData
    {
        public CaptureFileData() { }

        public CurrentCaptureFile GetCurrentCaptureFile(string fileName)
        {
            CurrentCaptureFile ccf = null;

            //using (var context = new PacketCaptureContext())
            using (var context = new PacketAnalysisEntity())
            {
                var captureBatchId = (from b in context.CaptureBatches
                                      where b.FileName == fileName
                                      select b.CaptureBatchId).FirstOrDefault();

                var marked = (from m in context.CapturePackets
                              where m.CaptureBatchId == Convert.ToInt32(captureBatchId) 
                              select m.Marked).FirstOrDefault();

                ccf = new CurrentCaptureFile(fileName, Convert.ToInt32(captureBatchId), marked == true? CaptureState.Marked : CaptureState.Unmarked);
            }
            return ccf;
        }

        public BindingList<CapturePacket> GetRawPacketData()
        {
            BindingList<CapturePacket> packets = new BindingList<CapturePacket>();

            using (var context = new PacketAnalysisEntity())
            {
                foreach (CapturePacket cp in context.CapturePackets)
                {
                    packets.Add(cp);
                }                
            }
            return packets;
        }

        public BindingList<CurrentCaptureFile> GetBatchIds()
        {
            BindingList<CurrentCaptureFile> batchIds = new BindingList<CurrentCaptureFile>();

            using (var context = new PacketAnalysisEntity())
            {
                var items = (from b in context.CaptureBatches
                             select new
                             {
                                 b.FileName,
                                 b.CaptureBatchId,
                                 b.Marked
                             }).ToList();

                foreach (var item in items)
                {
                    CurrentCaptureFile ccf = new CurrentCaptureFile();
                    ccf.FileName = item.FileName;
                    ccf.CaptureBatchId = item.CaptureBatchId;
                    ccf.Marked = item.Marked == true ? CaptureState.Marked : CaptureState.Unmarked;
                    batchIds.Add(ccf);
                }
            }

            return batchIds;
        }
    }
}
