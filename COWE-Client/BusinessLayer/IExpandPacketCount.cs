using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using COWE.DomainClasses;

namespace COWE.BusinessLayer
{
    interface IExpandPacketCount
    {
        SortedDictionary<int, decimal> ExpandPacketCount();
    }
}
