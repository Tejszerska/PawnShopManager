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
    public class NewPawnLoanItemViewModel : OneViewModel<Items>, IDataErrorInfo
    {
        public NewPawnLoanItemViewModel() : base()
        {
            base.DisplayName = "New Pawn Loan Item";
            item = new Items();

            item.item_status_id = 2;
            item.acquisition_source_type_id = 2;
            item.current_branch_id = 1;

        }

        #region Properties
        public string Name
        {
            get { return item.name; }
            set
            {
                if (value != item.name)
                {
                    item.name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        public string Description
        {
            get { return item.description; }
            set
            {
                if (value != item.description)
                {
                    item.description = value; OnPropertyChanged(() => Description);
                }
            }
        }

        public decimal Estimated_Value
        {
            get { return item.estimated_value; }
            set
            {
                if (value != item.estimated_value)
                {
                    item.estimated_value = value;
                    OnPropertyChanged(() => Estimated_Value);
                }
            }
        }

        public int Category_Id
        {
            get { return item.category_id; }
            set
            {
                if (value != item.category_id)
                {
                    item.category_id = value;
                    OnPropertyChanged(() => Category_Id);
                }
            }
        }
        public int Condition_Id
        {
            get { return item.condition_id; }
            set
            {
                if (value != item.condition_id)
                {
                    item.condition_id = value;
                    OnPropertyChanged(() => Condition_Id);
                }
            }
        }


        public decimal Sale_Price
        {
            get { return item.sale_price; }
            set
            {
                if (value != item.sale_price)
                {
                    item.sale_price = value;
                    OnPropertyChanged(() => Sale_Price);
                }
            }
        }

        public int? Current_Branch_Id
        {
            get { return item.current_branch_id; }
            set
            {
                if (value != item.current_branch_id)
                {
                    item.current_branch_id = value;
                    OnPropertyChanged(() => Current_Branch_Id);
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
            get { return pawnShopEntities.ItemConditions.Where(x => x.is_active == true).ToList().AsQueryable(); }
        }

        public IQueryable<Branches> BranchesItems
        {
            get { return pawnShopEntities.Branches.Where(x => x.is_active == true).ToList().AsQueryable(); }
        }
        #endregion

        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();

            pawnShopEntities.Items.Add(item);
            pawnShopEntities.SaveChanges();

            Messenger.Default.Send(item);
        }
     
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

                switch (columnName)
                {
                    case "Estimated_Value":
                        message = BusinessValidator.IsGraterThanZero(this.Estimated_Value);
                        break;
                    case "Sale_Price":
                        message = BusinessValidator.IsGraterThanZero(this.Sale_Price);
                        break;
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Estimated_Value"] == null && this["Sale_Price"] == null)
            {
                return true;
            }
            return false;
        }
    }
        #endregion
    }