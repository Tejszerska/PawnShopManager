using MVVMFirma.Models.BussinesLogic;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BussinessLogic
{
    public class CategoriesB : DatabaseClass

    {
        #region Constructor
        public CategoriesB(PawnShopEntities pawnShopEntities) : base(pawnShopEntities)
        {
        }
        #endregion

        #region Helping functions
        public IQueryable<KeyAndValue> GetItemCategoriesKeyAndValue()
        {
            var categoriesForCB =
                (
                from categories in pawnShopEntities.Categories
                where categories.is_active == true
                select new KeyAndValue
                {
                    Key = categories.category_id,
                    Value = categories.name
                }
                ).ToList();

            return categoriesForCB.AsQueryable();
        }
        #endregion
    }
}