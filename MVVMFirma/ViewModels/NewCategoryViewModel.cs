using MVVMFirma.Models;
using MVVMFirma.Models.Validators;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class NewCategoryViewModel : OneViewModel<Categories>, IDataErrorInfo
    {
        public NewCategoryViewModel()
                        : base()
        {
            base.DisplayName = "New Category";
            item = new Categories();
        }
        #region Properties
        // tylko dla pol towarow ktore bedziemy dodawac - dodajemu properties
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
        public int? Parent_ID
        {
            get
            {
                return item.parent_id;
            }
            set
            {
                if (value != item.parent_id)
                {
                    item.parent_id = value;
                    OnPropertyChanged(() => Parent_ID);
                }
            }
        }

        public IQueryable<Categories> CategoryParentItems
        {
            get
            {
                return pawnShopEntities.Categories.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }

        #endregion
        #region Commends
        // komendy przyciskow zapisz i zamknij
        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.Categories.Add(item);
            pawnShopEntities.SaveChanges();
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


                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Name"] == null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}

