using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace MVVMFirma.ViewModels
{
public class AllItemsViewModel : AllViewModel<ItemsExtendedView>
    {
        #region  Abstract implemented methods
        public override void Sort()
        {
            if (SortField == "ID")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.OrderBy(x => x.ItemId));
            }
            if (SortField == "Name")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.OrderBy(x => x.ItemName));
            }
            if (SortField == "Estimated Value")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.OrderBy(x => x.EstimatedValue));
            }
            if (SortField == "Sale Price")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.OrderBy(x => x.SalePrice));
            }

        }

        public override void Search()
        {
            if (SearchField == "ID")
            {

                if (int.TryParse(SearchTextBox, out int search))
                    {
                    List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.ItemId == search));
                }
            }
            if (SearchField == "Name")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.ItemName != null && x.ItemName.ToLower().Contains(SearchTextBox)));
            }
            if (SearchField == "Estimated Value")
            {
                if (decimal.TryParse(SearchTextBox, out decimal search))
                {
                    List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.EstimatedValue == search));
                }

            }
            if (SearchField == "Sale Price")
            {
                if (decimal.TryParse(SearchTextBox, out decimal search))
                {
                    List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.SalePrice == search));
                }
            }
            if (SearchField == "Status")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.ItemStatus != null && x.ItemStatus.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Category")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.CategoryName != null && x.CategoryName.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Condition")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.Condition != null && x.Condition.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Acquisition Source")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.AcquisitionSource != null && x.AcquisitionSource.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Branch")
            {
                List = new ObservableCollection<ItemsExtendedView>(List.Where(x => x.BranchName != null && x.BranchName.ToLower().StartsWith(SearchTextBox)));
            }
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "ID", "Name", "Estimated Value", "Sale Price" };

        }

        public override List<string> getComboboxSearchList()
        {
            return new List<string> { "ID", "Name", "Estimated Value", "Sale Price", "Status", "Category", "Condition", "Acquisition Source", "Branch" };
        }
        public override void Load()
        {

            List = new ObservableCollection<ItemsExtendedView>
                (

                   from item in pawnShopEntities.Items
                    where item.is_active == true
                    select new ItemsExtendedView
                    {
                        ItemId = item.item_id,
                        ItemName = item.name,
                        ItemStatus = item.ItemStatuses.name,
                        CategoryName = item.Categories.name,
                        Description = item.description,
                        EstimatedValue = item.estimated_value,
                        SalePrice = item.sale_price,
                        Condition = item.ItemConditions.name,
                        AcquisitionSource = item.AcquisitionSourceTypes.name,
                        BranchName = item.Branches.name

                    }
                );
        }
        #endregion
        #region Constructor
        public AllItemsViewModel()
            : base()
        {
            base.DisplayName = "Items";

        }

        #endregion
        #region Properties
        private ItemsExtendedView _SelectedItem;
        public ItemsExtendedView SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (value != _SelectedItem)
                {
                    _SelectedItem = value;
                    Messenger.Default.Send(_SelectedItem);
                    OnRequestClose();
                }
            }
        }
        #endregion
    }
}