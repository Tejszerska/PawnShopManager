using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.Validators;
using MVVMFirma.ViewModels.Abstract;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewItemViewModel : OneViewModel<Items>, IDataErrorInfo
    {
        #region Constructor
        public NewItemViewModel()
            : base()
        {
            base.DisplayName = "New Item";
            item = new Items();
        }
        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return item.name;
            }
            set
            {
                if (value != item.name)
                {
                    item.name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public int Category_Id
        {
            get
            {
                return item.category_id;
            }
            set
            {
                if (value != item.category_id)
                {
                    item.category_id = value;
                    OnPropertyChanged(() => Category_Id);
                }
            }
        }

        public string Description
        {
            get
            {
                return item.description;
            }
            set
            {
                if (value != item.description)
                {
                    item.description = value;
                    OnPropertyChanged(() => Description);
                }
            }
        }

        public decimal Estimated_Value
        {
            get
            {
                return item.estimated_value;
            }
            set
            {
                if (value != item.estimated_value)
                {
                    item.estimated_value = value;
                    OnPropertyChanged(() => Estimated_Value);
                }
            }
        }

        public decimal Sale_Price
        {
            get
            {
                return item.sale_price;
            }
            set
            {
                if (value != item.sale_price)
                {
                    item.sale_price = value;
                    OnPropertyChanged(() => Sale_Price);
                }
            }
        }

        public int Condition_Id
        {
            get
            {
                return item.condition_id;
            }
            set
            {
                if (value != item.condition_id)
                {
                    item.condition_id = value;
                    OnPropertyChanged(() => Condition_Id);
                }
            }
        }

        public int Acquisition_Source_Type_Id
        {
            get
            {
                return item.acquisition_source_type_id;
            }
            set
            {
                if (value != item.acquisition_source_type_id)
                {
                    item.acquisition_source_type_id = value;
                    OnPropertyChanged(() => Acquisition_Source_Type_Id);
                }
            }
        }

        public int? Acquisition_Source_Id
        {
            get
            {
                return item.acquisition_source_id;
            }
            set
            {
                if (value != item.acquisition_source_id)
                {
                    item.acquisition_source_id = value;
                    OnPropertyChanged(() => Acquisition_Source_Id);
                }
            }
        }

        public int? Current_Branch_Id
        {
            get
            {
                return item.current_branch_id;
            }
            set
            {
                if (value != item.current_branch_id)
                {
                    item.current_branch_id = value;
                    OnPropertyChanged(() => Current_Branch_Id);
                }
            }
        }

        public int Item_Status_Id
        {
            get
            {
                return item.item_status_id;
            }
            set
            {
                if (value != item.item_status_id)
                {
                    item.item_status_id = value;
                    OnPropertyChanged(() => Item_Status_Id);
                }
            }
        }

        public IQueryable<Categories> CategoriesItems
        {
            get
            {
                return pawnShopEntities.Categories.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }
        public IQueryable<ItemConditions> ItemConditionsItems
        {
            get
            {
                return pawnShopEntities.ItemConditions.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }
        public IQueryable<AcquisitionSourceTypes> AcquisitionSourceTypesItems
        {
            get
            {
                return pawnShopEntities.AcquisitionSourceTypes.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }
        public IQueryable<ItemStatuses> ItemStatusesItems
        {
            get
            {
                return pawnShopEntities.ItemStatuses.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }
        public IQueryable<Branches> BranchesItems
        {
            get
            {
                return pawnShopEntities.Branches.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }

        #endregion

        #region Commands
        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.Items.Add(item);
            pawnShopEntities.SaveChanges();

            Messenger.Default.Send(item); // for NewPawnLoanView to receive the new item

        }
        #endregion
        #region Validation (IDataErrorInfo Members)

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                string message = null;

                if (columnName == "Name")
                {
                    message = StringValidator.CheckIfStartsWithCapitalLetter(this.Name);
                }
                if (columnName == "Estimated_Value")
                {
                    message = BusinessValidator.IsGraterThanZero(this.Estimated_Value);
                }
                if (columnName == "Sale_Price")
                {
                    message = BusinessValidator.IsGraterThanZero(this.Sale_Price);
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Name"] == null && this["Sale_Price"] == null && this["Estimated_Value"] == null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
