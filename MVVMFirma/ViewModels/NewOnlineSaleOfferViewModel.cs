using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Models.Validators;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewOnlineSaleOfferViewModel : OneViewModel<OnlineSaleOffers>, IDataErrorInfo
    {
        #region Constructor
        public NewOnlineSaleOfferViewModel()
            : base()
        {
            base.DisplayName = "New Online Offer";
            item = new OnlineSaleOffers();
            item.listing_date = DateTime.Now; 
            item.expiration_date = DateTime.Now; 
            item.views_count = 0;  
            item.messages_count = 0;
            Messenger.Default.Register<ItemsExtendedView>(this, getSelectedItem);
        }
        #endregion
        #region Properties
        public int Item_Id
        {
            get
            {
                return item.item_id;
            }
            set
            {
                if (value != item.item_id)
                {
                    item.item_id = value;
                    OnPropertyChanged(() => Item_Id);
                }
            }
        }

        private string _Item_name;
        public string Item_name
        {
            get { return _Item_name; }
            set
            {
                if (value != _Item_name)
                {
                    _Item_name = value;
                    OnPropertyChanged(() => Item_name);
                }
            }
        }

        private string _Item_condition;
        public string Item_condition
        {
            get { return _Item_condition; }
            set
            {
                if (value != _Item_condition)
                {
                    _Item_condition = value;
                    OnPropertyChanged(() => Item_condition);
                }
            }
        }


        public int Platform_Id
        {
            get
            {
                return item.platform_id;
            }
            set
            {
                if (value != item.platform_id)
                {
                    item.platform_id = value;
                    OnPropertyChanged(() => Platform_Id);
                }
            }
        }

        public string Offer_Title
        {
            get
            {
                return item.offer_title;
            }
            set
            {
                if (value != item.offer_title)
                {
                    item.offer_title = value;
                    OnPropertyChanged(() => Offer_Title);
                }
            }
        }

        public decimal Listing_Price
        {
            get
            {
                return item.listing_price;
            }
            set
            {
                if (value != item.listing_price)
                {
                    item.listing_price = value;
                    OnPropertyChanged(() => Listing_Price);
                }
            }
        }

        
        public DateTime Expiration_Date
        {
            get
            {
                return item.expiration_date;
            }
            set
            {
                if (value != item.expiration_date)
                {
                    item.expiration_date = value;
                    OnPropertyChanged(() => Expiration_Date);
                }
            }
        }

        public string Url
        {
            get
            {
                return item.url;
            }
            set
            {
                if (value != item.url)
                {
                    item.url = value;
                    OnPropertyChanged(() => Url);
                }
            }
        }

        public int Status_Id
        {
            get
            {
                return item.status_id;
            }
            set
            {
                if (value != item.status_id)
                {
                    item.status_id = value;
                    OnPropertyChanged(() => Status_Id);
                }
            }
        }

        
        public string Notes
        {
            get
            {
                return item.notes;
            }
            set
            {
                if (value != item.notes)
                {
                    item.notes = value;
                    OnPropertyChanged(() => Notes);
                }
            }
        }

        public IQueryable<OnlinePlatforms> OnlinePlatformsItems
        {
            get
            {
                return pawnShopEntities.OnlinePlatforms.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }

        public IQueryable<SalesStatuses> SalesStatusesItems
        {
            get
            {
                return pawnShopEntities.SalesStatuses.Where(x => x.is_active == true && x.sale_type == "ONLINE_SALE").ToList().AsQueryable();
            }
        }

        #endregion
        #region Commends
        // komendy przyciskow zapisz i zamknij
        public override void Save()
        {
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.OnlineSaleOffers.Add(item);
            pawnShopEntities.SaveChanges();
        }

        private BaseCommand _ShowItems;
        public ICommand ShowItems
        {
            get
            {
                if (_ShowItems == null) _ShowItems = new BaseCommand(() =>
                    Messenger.Default.Send("Items Show")
                    );
                return _ShowItems;
            }
        }
        #endregion
        #region Helpers
        public void getSelectedItem(ItemsExtendedView item)
        {
            Item_Id = item.ItemId;
            Item_name = item.ItemName;
            Item_condition = item.Condition;
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
                    case "Expiration_Date":
                        message = BusinessValidator.IsDatePastOrToday(this.Expiration_Date);
                        break;
                    case "Listing_Price":
                        message = BusinessValidator.IsGraterThanZero(this.Listing_Price);
                        break;
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Expiration_Date"] == null && this["Listing_Price"] == null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
