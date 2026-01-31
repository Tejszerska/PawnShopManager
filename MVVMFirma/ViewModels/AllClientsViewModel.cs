using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class AllClientsViewModel : AllViewModel<ClientsExtendedView>
    {
        #region  Abstract implemented methods
        public override void Sort()
        {
            if (SortField == "Last name")
            {
                List = new ObservableCollection<ClientsExtendedView>(List.OrderBy(x => x.LastName));
            }
            if (SortField == "Client's ID")
            {
                               List = new ObservableCollection<ClientsExtendedView>(List.OrderBy(x => x.ClientId));
            }
            if (SortField == "Document Type")
            {
                List = new ObservableCollection<ClientsExtendedView>(List.OrderBy(x => x.DocumentType));
            }
        }

        public override void Search()
        {
            if(SearchField == "Last name")
            {
                List = new ObservableCollection<ClientsExtendedView>(List.Where(x => x.LastName != null && x.LastName.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Client's ID")
            {
                if (int.TryParse(SearchTextBox, out int search))
                {
                   List = new ObservableCollection<ClientsExtendedView>(List.Where(x => x.ClientId == search));
                }

             }
            if (SearchField == "Document Number")
                {
                List = new ObservableCollection<ClientsExtendedView>(List.Where(x => x.DocumentNumber != null && x.DocumentNumber.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "PESEL")
            {
                List = new ObservableCollection<ClientsExtendedView>(List.Where(x => x.PESEL != null && x.PESEL.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Email")
            {
                List = new ObservableCollection<ClientsExtendedView>(List.Where(x => x.Email != null && x.Email.ToLower().StartsWith(SearchTextBox)));
            }
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> {"Last name", "Client's ID", "Document Type" };
        }

        public override List<string> getComboboxSearchList()
        {
            return new List<string> { "Last name", "Client's ID", "Document Number", "PESEL", "Email" };
        }
        public override void Load()
        {

            List = new ObservableCollection<ClientsExtendedView>
                (
                from client in pawnShopEntities.Clients
                where client.is_active == true
                select new ClientsExtendedView
                {
                    ClientId = client.client_id,
                    FirstName = client.first_name,
                    LastName = client.last_name,
                    DocumentType = client.DocumentTypes.name,
                    DocumentNumber = client.document_number,
                    PESEL = client.pesel,
                    Address = client.address,
                    AddressSource = client.AddressSources.name,
                    Phone = client.phone,
                    Email = client.email
                }
                );
        }


        #endregion
        #region Constructor
        public AllClientsViewModel()
            : base()
        {
            base.DisplayName = "Clients";

        }

        #endregion
        #region Properties
        private ClientsExtendedView _SelectedClient;
        public ClientsExtendedView SelectedClient
        {
            get
            {
                return _SelectedClient;
            }
            set
            {
                if (value != _SelectedClient)
                {
                    _SelectedClient = value;
                    Messenger.Default.Send(_SelectedClient);
                    OnRequestClose();
                }
            }
        }
        #endregion
    }
}