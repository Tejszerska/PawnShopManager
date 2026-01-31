using MVVMFirma.Models.BussinesLogic;
using MVVMFirma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMFirma.Models.BussinessLogic
{
    public class CategoryStatsSummaryB : DatabaseClass
    {
        private IQueryable<Items> GetCategoryItems(int categoryId)
        {
            int[] allowedStatuses = { 1, 4, 5 }; // 1 IN_STOCK, 4 FORFITED, 5 IN SALE
            var query = pawnShopEntities.Items.Where(item => item.is_active == true && allowedStatuses.Contains(item.item_status_id));

            if (categoryId > 0)
            {
                query = query.Where(item => item.category_id == categoryId);
            }
            return query;
        }

        #region Constructor
        public CategoryStatsSummaryB(PawnShopEntities pawnShopEntities) : base(pawnShopEntities)
        {
        }
        #endregion

        #region Bussiness functions
        public int GetItemsCount(int categoryId)
        {
            return GetCategoryItems(categoryId).Count();
        }

        public double GetAverageAging(int categoryId)
        {
            var items = GetCategoryItems(categoryId).ToList();
            if (!items.Any()) return 0;

            List<int> daysList = new List<int>();
            foreach (var item in items)
            {
                var history = pawnShopEntities.RecordHistory.FirstOrDefault(h => h.history_id == item.history_id);
                if (history != null)
                {
                    daysList.Add((DateTime.Now - history.created_at).Days);
                }
            }
            return daysList.Any() ? daysList.Average() : 0;
        }

        public decimal GetTotalPotentialProfit(int categoryId)
        {
            return GetCategoryItems(categoryId)
                .Sum(i => (decimal?)(i.sale_price - i.estimated_value)) ?? 0m;
        }
        #endregion
    }
}