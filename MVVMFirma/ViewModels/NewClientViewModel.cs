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
    public class NewClientViewModel : OneViewModel<Clients>, IDataErrorInfo
    {
        #region Constructor
        public NewClientViewModel()
            : base()
        {
            base.DisplayName = "New Client";
            item = new Clients();
        }
        #endregion
        #region Properties
        // tylko dla pol towarow ktore bedziemy dodawac - dodajemu properties
        public string First_name
        {
            get
            {
                return item.first_name;
            }
            set
            {
                if (value != item.first_name)
                {
                    item.first_name = value;
                    OnPropertyChanged(() => First_name);
                }
            }
        }

        public string Last_name
        {
            get
            {
                return item.last_name;
            }
            set
            {
                if (value != item.last_name)
                {
                    item.last_name = value;
                    OnPropertyChanged(() => Last_name);
                }
            }
        }

        public int Document_type
        {
            get
            {
                return item.document_type_id;
            }
            set
            {
                if (value != item.document_type_id)
                {
                    item.document_type_id = value;
                    OnPropertyChanged(() => Document_type);
                }
            }
        }

        public string Document_number
        {
            get
            {
                return item.document_number;
            }
            set
            {
                if (value != item.document_number)
                {
                    item.document_number = value;
                    OnPropertyChanged(() => Document_number);
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


        public int? Address_source
        {
            get
            {
                return item.address_source_id;
            }
            set
            {
                if (value != item.address_source_id)
                {
                    item.address_source_id = value;
                    OnPropertyChanged(() => Address_source);
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


        public string Email
        {
            get
            {
                return item.email;
            }
            set
            {
                if (value != item.email)
                {
                    item.email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        public IQueryable<DocumentTypes> DocumentTypesItems
        {
            get
            {
                return pawnShopEntities.DocumentTypes.Where(ss => ss.is_active == true).ToList().AsQueryable();
            }
        }

        public IQueryable<AddressSources> AddressSourcesItems
        {
            get
            {
                return pawnShopEntities.AddressSources.Where(ss => ss.is_active == true).ToList().AsQueryable();
            }
        }

        #endregion
        #region Commends
        // komendy przyciskow zapisz i zamknij
        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.Clients.Add(item);
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

                switch (columnName)
                {
                    case "First_name":
                        message = StringValidator.CheckIfStartsWithCapitalLetter(this.First_name);
                        break;
                    case "Last_name":
                        message = StringValidator.CheckIfStartsWithCapitalLetter(this.Last_name);
                        break;
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["First_name"] == null && this["Last_name"] == null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
