using GalaSoft.MvvmLight.Messaging;
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
    public class AllPaymentsViewModel : AllViewModel<PaymentsExtendedView>
    {
        #region  Abstract implemented methods
        public override void Sort()
        {
            if (SortField == "ID")
            {
                List = new ObservableCollection<PaymentsExtendedView>(List.OrderBy(x => x.PaymentID));
            }
            if (SortField == "Date")
            {
                List = new ObservableCollection<PaymentsExtendedView>(List.OrderBy(x => x.PaymentDate));
            }
            if (SortField == "Amount")
            {
                List = new ObservableCollection<PaymentsExtendedView>(List.OrderBy(x => x.Amount));
            }
        }

        public override void Search()
        {
            if (SearchField == "ID")
            {
                if (int.TryParse(SearchTextBox, out int search))
                {
                    List = new ObservableCollection<PaymentsExtendedView>(List.Where(x => x.PaymentID == search));
                }
            }
            if (SearchField == "Date")
            {
                if (DateTime.TryParse(SearchTextBox, out DateTime searchDate))
                {
                    List = new ObservableCollection<PaymentsExtendedView>(List.Where(x => x.PaymentDate.Date == searchDate.Date));
                }
            }
            if (SearchField == "Amount")
            {
                if (decimal.TryParse(SearchTextBox, out decimal search))
                {
                    List = new ObservableCollection<PaymentsExtendedView>(List.Where(x => x.Amount == search));
                }
            }
            if (SearchField == "Payment method")
            {
                List = new ObservableCollection<PaymentsExtendedView>(List.Where(x => x.PaymentMethod != null && x.PaymentMethod.ToLower().StartsWith(SearchTextBox)));
            }
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> { "Date", "ID", "Amount"};
        }

        public override List<string> getComboboxSearchList()
        {
            return new List<string> { "Date", "ID", "Amount", "Payment method" };
        }
        public override void Load()
        {

            List = new ObservableCollection<PaymentsExtendedView>
                (
                from payment in pawnShopEntities.Payments
                where payment.is_active == true
                select new PaymentsExtendedView
                {
                    PaymentID = payment.payment_id,
                    PaymentDate = payment.payment_date,
                    Amount = payment.amount,
                    PaymentMethod = payment.PaymentMethods.name,
                    Description = payment.description
                }
                );
        }
        #endregion Abstract implemented methods
      
        #region Constructor
        public AllPaymentsViewModel()
            : base()
        {
            base.DisplayName = "Payments";

        }
        #endregion
        #region Properties
        private PaymentsExtendedView _SelectedPayment;
        public PaymentsExtendedView SelectedPayment
        {
            get
            {
                return _SelectedPayment;
            }
            set
            {
                if (value != _SelectedPayment)
                {
                    _SelectedPayment = value;
                    Messenger.Default.Send(_SelectedPayment);
                    OnRequestClose();
                }
            }
        }
        #endregion
    }
}