using System;

namespace MVVMFirma.Models.EntitiesForView
{
    public class PurchaseContractItemExtended
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Notes { get; set; }

        public PurchaseContractItemExtended()
        {
            Notes = "";
        }

    }
}