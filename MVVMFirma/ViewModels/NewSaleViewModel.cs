using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Models.Validators;
using MVVMFirma.ViewModels.Abstract;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewSaleViewModel : OneViewModel<Sales>, IDataErrorInfo
    {
        #region Constructor
        public NewSaleViewModel()
            : base()
        {
            base.DisplayName = "New Sale";
            item = new Sales();
            item.sale_date = DateTime.Now;
            Messenger.Default.Register<PaymentsExtendedView>(this, getSelectedPayment);
            Messenger.Default.Register<OnlineSaleOffersExtendedView>(this, getSelectedOffer);
            SaleItemsList = new ObservableCollection<SalesItemExtended>();
            Messenger.Default.Register<ItemsExtendedView>(this, AddItemToBasket);
            Payment_Date = DateTime.Now;

        }
        #endregion

        #region Properties

        private ObservableCollection<SalesItemExtended> _SaleItemsList;
        public ObservableCollection<SalesItemExtended> SaleItemsList
        {
            get { return _SaleItemsList; }
            set
            {
                _SaleItemsList = value;
                OnPropertyChanged(() => SaleItemsList);
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

        public DateTime Sale_Date
        {
            get { return item.sale_date; }
            set
            {
                if (value != item.sale_date)
                {
                    item.sale_date = value;
                    OnPropertyChanged(() => Sale_Date);
                }
            }
        }

        public int Finalization_Source_Id
        {
            get { return item.finalization_source_id; }
            set
            {
                if (value != item.finalization_source_id)
                {
                    item.finalization_source_id = value;
                    OnPropertyChanged(() => Finalization_Source_Id);
                }
            }
        }

        public int? Online_Offer_Id
        {
            get { return item.online_offer_id; }
            set
            {
                if (value != item.online_offer_id)
                {
                    item.online_offer_id = value;
                    OnPropertyChanged(() => Online_Offer_Id);
                }
            }
        }

        public int Payment_Id
        {
            get { return item.payment_id; }
            set
            {
                if (value != item.payment_id)
                {
                    item.payment_id = value;
                    OnPropertyChanged(() => Payment_Id);
                }
            }
        }

        private string _Payment_Method;
        public string Payment_Method
        {
            get { return _Payment_Method; }
            set
            {
                if (value != _Payment_Method)
                {
                    _Payment_Method = value;
                    OnPropertyChanged(() => Payment_Method);
                }
            }
        }
        private DateTime _Payment_Date;
        public DateTime Payment_Date
        {
            get { return _Payment_Date; }
            set
            {
                if (value != _Payment_Date)
                {
                    _Payment_Date = value;
                    OnPropertyChanged(() => Payment_Date);
                }
            }
        }

        public int Status_Id
        {
            get { return item.status_id; }
            set
            {
                if (value != item.status_id)
                {
                    item.status_id = value;
                    OnPropertyChanged(() => Status_Id);
                }
            }
        }

        public bool Is_Active
        {
            get { return item.is_active; }
            set
            {
                if (value != item.is_active)
                {
                    item.is_active = value;
                    OnPropertyChanged(() => Is_Active);
                }
            }
        }
        private string _Offer_Title;
        public string Offer_Title
        {
            get { return _Offer_Title; }
            set
            {
                if (_Offer_Title != value)
                {
                    _Offer_Title = value;
                    OnPropertyChanged(() => Offer_Title);
                }
            }
        }

        private string _Platform;
        public string Platform
        {
            get { return _Platform; }
            set
            {
                if (_Platform != value)
                {
                    _Platform = value;
                    OnPropertyChanged(() => Platform);
                }
            }
        }

        // for FK
        public IQueryable<SalesStatuses> SalesStatusesItems
        {
            get
            {
                return pawnShopEntities.SalesStatuses.Where(ss => ss.is_active == true && ss.sale_type == "SALE").ToList().AsQueryable();
            }
        }
        public IQueryable<FinalizationSources> FinalizationSourcesItems
        {
            get
            {
                return pawnShopEntities.FinalizationSources.Where(ss => ss.is_active == true).ToList().AsQueryable();
            }
        }


        #endregion

        #region Commands
        public override void Save()
        {
            if (SaleItemsList == null || SaleItemsList.Count == 0)
            {
                MessageBox.Show("Please add items first!");
                return;
            }
            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.Sales.Add(item);
            pawnShopEntities.SaveChanges();

            foreach (SalesItemExtended viewItem in SaleItemsList)
            {
                SalesItems dbSaleItem = new SalesItems
                {
                    sale_id = item.sale_id,
                    item_id = viewItem.ItemId,
                    sold_price_for_item = viewItem.ItemPrice,
                    history_id = createRecordHistory()
                };

                pawnShopEntities.SalesItems.Add(dbSaleItem);

                Items originalItem = pawnShopEntities.Items.FirstOrDefault(x => x.item_id == viewItem.ItemId);
                if (originalItem != null)
                {
                    originalItem.item_status_id = 6;
                }
            }

            pawnShopEntities.SaveChanges();
        }

        private BaseCommand _ShowPayments;
        public ICommand ShowPayments
        {
            get
            {
                if (_ShowPayments == null) _ShowPayments = new BaseCommand(() =>
                    Messenger.Default.Send("Payments Show")
                    );
                return _ShowPayments;
            }
        }

        private ICommand _ShowOnlineOffers;
        public ICommand ShowOnlineOffers
        {
            get
            {
                if (_ShowOnlineOffers == null)
                    _ShowOnlineOffers = new BaseCommand(() => Messenger.Default.Send("Offers Show"));
                return _ShowOnlineOffers;
            }
        }

        private ICommand _AddItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (_AddItemCommand == null)
                    _AddItemCommand = new BaseCommand(() => Messenger.Default.Send("Items Show"));
                return _AddItemCommand;
            }
        }
        #endregion
        #region Helpers
        public void getSelectedPayment(PaymentsExtendedView payment)
        {
            Payment_Id = payment.PaymentID;
            Payment_Method = payment.PaymentMethod;
            Payment_Date = payment.PaymentDate;

        }
        private void getSelectedOffer(OnlineSaleOffersExtendedView offer)
        {
            Online_Offer_Id = offer.OnlineSaleOfferId;
            Offer_Title = offer.OfferTitle;
            Platform = offer.Platform;
        }
        private void AddItemToBasket(ItemsExtendedView selectedItem)
        {
            if (SaleItemsList.Any(x => x.ItemId == selectedItem.ItemId))
                return;

            SalesItemExtended newItem = new SalesItemExtended
            {
                ItemId = selectedItem.ItemId,
                ItemName = selectedItem.ItemName,
                ItemCondition = selectedItem.Condition,
                ItemPrice = selectedItem.SalePrice
            };

            SaleItemsList.Add(newItem);
            RecalculateTotal();
        }


        private void RecalculateTotal()
        {
            decimal total = 0;
            if (SaleItemsList != null)
            {
                foreach (SalesItemExtended line in SaleItemsList)
                {
                    total += line.ItemPrice;
                }
            }
            Sale_Price = total;
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

                

                if (columnName == "Sale_Price")
                {
                    message = BusinessValidator.IsGraterThanZero(this.Sale_Price);
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Sale_Price"] == null)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}