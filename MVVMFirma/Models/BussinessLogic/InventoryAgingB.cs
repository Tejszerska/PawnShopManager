using MVVMFirma.Models.BussinesLogic;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;


namespace MVVMFirma.Models.BussinessLogic
{
    public class InventoryAgingB : DatabaseClass
    {
        #region Constructor
        public InventoryAgingB(PawnShopEntities entities) : base(entities) { }
        #endregion
        #region Business functions
        public IQueryable<InventoryAging> GetAgingReport()
        {
            int[] allowedStatuses = {1, 4, 5, 9 }; // 1 IN_STOCK, 4 FORFITED, 5 IN SALE, 9 DEMAGED

            return from item in pawnShopEntities.Items
                   where item.is_active == true &&
                   allowedStatuses.Contains(item.item_status_id)
                   let days = DbFunctions.DiffDays(item.RecordHistory.created_at, DateTime.Now)
                   select new InventoryAging
                   {
                       ItemName = item.name,
                       DaysInStock = days ?? 0,
                       EstimatedValue = item.estimated_value,
                       AgeCategory = days < 30 ? "0-29 days" :
                                     days < 60 ? "30-59 days" :
                                     days < 90 ? "60-89 days" :
                                     "90+ days"
                   };

        }
        #endregion
     }
}
