using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMFirma.ViewModels
{
    public class AllSalesItemsViewModel : AllViewModel<SalesItems>
    {
        #region 
        public override void Load()
        {

            List = new ObservableCollection<SalesItems>
                (
                  pawnShopEntities.SalesItems.ToList()
                );
        }
        #endregion  Abstract implemented methods
        public override void Sort()
        {

        }

        public override void Search()
        {

        }

        public override List<string> getComboboxSortList()
        {
            return null;
        }

        public override List<string> getComboboxSearchList()
        {
            return null;
        }
        #region Constructor
        public AllSalesItemsViewModel()
            : base()
        {
            base.DisplayName = "Sales Items";
        }

        #endregion
    }
}