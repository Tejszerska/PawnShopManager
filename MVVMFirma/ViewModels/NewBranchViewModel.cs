using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System.ComponentModel;
using System.Windows.Input;
using MVVMFirma.Models.Validators;
namespace MVVMFirma.ViewModels
{
    public class NewBranchViewModel : OneViewModel<Branches>, IDataErrorInfo
    {
        #region Constructor
        public NewBranchViewModel()
            : base()
        {
            base.DisplayName = "New Branch";
            item = new Branches();
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
        public string Address
        {
            get
            {
                return item.address;
            }
            set
            {
                if (value != item.address)
                {
                    item.address = value;
                    OnPropertyChanged(() => Address);
                }
            }
        }
        public string Phone
        {
            get
            {
                return item.phone;
            }
            set
            {
                if (value != item.phone)
                {
                    item.phone = value;
                    OnPropertyChanged(() => Phone);
                }
            }
        }
        #endregion
        #region Commends
        // komendy przyciskow zapisz i zamknij
        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.Branches.Add(item);
            pawnShopEntities.SaveChanges();
        }
        #endregion
        #region Validation
        public string Error
        {
            get { return null; }
        }
        public string this[string name]
        {
            get
            {
                string message = null;

                if (name == "Name")
                {
                    message = StringValidator.CheckIfStartsWithCapitalLetter(this.Name);
                }
                return message;
            }
        }
        public override bool IsValid()
        {  return this["Name"] == null;
        }

        #endregion

    }
}
