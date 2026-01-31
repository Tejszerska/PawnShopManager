using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.EntitiesForView
{
    public class SalesItemExtended
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCondition { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
