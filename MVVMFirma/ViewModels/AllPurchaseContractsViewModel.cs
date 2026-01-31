using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace MVVMFirma.ViewModels
{
    public class AllPurchaseContractsViewModel : AllViewModel<PurchaseContractForAllView>
    {
        #region  Abstract implemented methods
        public override void Sort()
        {
            if (SortField == "Purchase Date")
            {
                List = new ObservableCollection<PurchaseContractForAllView>(List.OrderBy(x => x.PurchaseDate));
            }
            if (SortField == "Total Amount")
            {
                List = new ObservableCollection<PurchaseContractForAllView>(List.OrderBy(x => x.TotalAmount));
            }
        }

        public override void Search()
        {
            if (SearchField == "Last name")
            {
                List = new ObservableCollection<PurchaseContractForAllView>(List.Where(x => x.LastName != null && x.LastName.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Agreement Number")
            {
                List = new ObservableCollection<PurchaseContractForAllView>(List.Where(x => x.AgreementNumber != null && x.AgreementNumber.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Purchase Date")
            {
                if (DateTime.TryParse(SearchTextBox, out DateTime searchDate))
                {
                    List = new ObservableCollection<PurchaseContractForAllView>(List.Where(x => x.PurchaseDate.Date == searchDate.Date));
                }

            }
            if (SearchField == "Total Amount")
            {
                if (decimal.TryParse(SearchTextBox, out decimal search))
                {
                    List = new ObservableCollection<PurchaseContractForAllView>(List.Where(x => x.TotalAmount == search));
                }
            }
            if (SearchField == "Item")
            {
                List = new ObservableCollection<PurchaseContractForAllView>(List.Where(x => x.Items.Any(item => item.ItemName != null && item.ItemName.ToLower().Contains(SearchTextBox))));
            }
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Purchase Date", "Total Amount" };
        }

        public override List<string> getComboboxSearchList()
        {
            return new List<string> { "Last name", "Agreement Number", "Purchase Date", "Total  Amount", "Item" };
        }
        public override void Load()
        {

            List = new ObservableCollection<PurchaseContractForAllView>
                (
                 from purchaseContract in pawnShopEntities.PurchaseContracts
                 where purchaseContract.is_active == true
                 select new PurchaseContractForAllView
                 {
                     AgreementNumber = purchaseContract.agreement_number,
                        Status = purchaseContract.ContractStatuses.name,
                        FirstName = purchaseContract.Clients.first_name,
                        LastName = purchaseContract.Clients.last_name,
                        PurchaseDate = purchaseContract.purchase_date,
                        TotalAmount = purchaseContract.total_purchase_price,
                     Items = purchaseContract.PurchaseContractItems.Select(item => new PawnItemForAllView
                     {     
                         ItemID = item.item_id,
                         ItemName = item.Items.name,
                         EstimatedValue = item.Items.estimated_value,
                         SellingPrice = item.Items.sale_price,
                         Condition = item.Items.ItemConditions.name
                     }).ToList()
                 }
                );
        }
        #endregion 
        #region Constructor
        public AllPurchaseContractsViewModel()
            : base()
        {
            base.DisplayName = "Purchase Contracts";

        }

        #endregion
    }
}