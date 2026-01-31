using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace MVVMFirma.ViewModels
{
    public class AllPawnLoanItemsViewModel : AllViewModel<PawnLoanItems>
    {
        #region  Abstract implemented methods
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
        public override void Load()
        {

            List = new ObservableCollection<PawnLoanItems>
                (
                  pawnShopEntities.PawnLoanItems.ToList()
                );
        }
        #endregion
        #region Constructor
        public AllPawnLoanItemsViewModel()
            : base()
        {
            base.DisplayName = "Pawn Loan Items";

        }

        #endregion
    }
}