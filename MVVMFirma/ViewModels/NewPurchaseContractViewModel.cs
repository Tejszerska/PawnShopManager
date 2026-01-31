using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewPurchaseContractViewModel : OneViewModel<PurchaseContracts>
    {
        #region Constructor
        public NewPurchaseContractViewModel()
            : base()
        {
            base.DisplayName = "New Purchase Contract";
            item = new PurchaseContracts();
            item.purchase_date = DateTime.Now;

            PurchaseContractItemsList = new ObservableCollection<PurchaseContractItemExtended>();

            Messenger.Default.Register<Items>(this, AddItemFromModal);
            Messenger.Default.Register<ClientsExtendedView>(this, GetSelectedClient);
        }
        #endregion

        #region Properties
        public int Client_Id
        {
            get { return item.client_id; }
            set
            {
                if (value != item.client_id)
                {
                    item.client_id = value;
                    OnPropertyChanged(() => Client_Id);
                }
            }
        }

        private string _ClientName_Preview;
        public string ClientName_Preview
        {
            get { return _ClientName_Preview; }
            set
            {
                if (value != _ClientName_Preview)
                {
                    _ClientName_Preview = value;
                    OnPropertyChanged(() => ClientName_Preview);
                }
            }
        }

        private string _ClientDoc_Preview;
        public string ClientDoc_Preview
        {
            get { return _ClientDoc_Preview; }
            set
            {
                if (value != _ClientDoc_Preview)
                {
                    _ClientDoc_Preview = value;
                    OnPropertyChanged(() => ClientDoc_Preview);
                }
            }
        }

        public string Agreement_Number
        {
            get { return item.agreement_number; }
            set
            {
                if (value != item.agreement_number)
                {
                    item.agreement_number = value;
                    OnPropertyChanged(() => Agreement_Number);
                }
            }
        }

        public DateTime Purchase_Date
        {
            get { return item.purchase_date; }
            set
            {
                if (value != item.purchase_date)
                {
                    item.purchase_date = value;
                    OnPropertyChanged(() => Purchase_Date);
                }
            }
        }

        public decimal Total_Purchase_Price
        {
            get { return item.total_purchase_price; }
            set
            {
                if (value != item.total_purchase_price)
                {
                    item.total_purchase_price = value;
                    OnPropertyChanged(() => Total_Purchase_Price);
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
        public IQueryable<ContractStatuses> ContractStatusesItems
        {
            get
            {
                return pawnShopEntities.ContractStatuses.Where(x => x.is_active == true && x.contract_type == "PURCHASE_CONTRACT").ToList().AsQueryable();
            }
        }
        private ObservableCollection<PurchaseContractItemExtended> _PurchaseContractItemsList;
        public ObservableCollection<PurchaseContractItemExtended> PurchaseContractItemsList
        {
            get { return _PurchaseContractItemsList; }
            set
            {
                _PurchaseContractItemsList = value;
                OnPropertyChanged(() => PurchaseContractItemsList);
            }
        }
        #endregion

        #region Commands
        public override void Save()
        {
            if (PurchaseContractItemsList == null || PurchaseContractItemsList.Count == 0)
            {
                MessageBox.Show("Add items first!");
                return;
            }
            RecalculateTotal();

            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.PurchaseContracts.Add(item);
            pawnShopEntities.SaveChanges();

            foreach (PurchaseContractItemExtended viewItem in PurchaseContractItemsList)
            {
                PurchaseContractItems dbLink = new PurchaseContractItems
                {
                    purchase_contract_id = item.purchase_contract_id,
                    item_id = viewItem.ItemId,
                    purchase_price = viewItem.PurchasePrice,
                    notes = viewItem.Notes,
                    history_id = createRecordHistory()
                };
                pawnShopEntities.PurchaseContractItems.Add(dbLink);

                Items originalItem = pawnShopEntities.Items.FirstOrDefault(x => x.item_id == viewItem.ItemId);
                if (originalItem != null)
                {
                    originalItem.acquisition_source_id = item.purchase_contract_id;
                }
            }
            pawnShopEntities.SaveChanges();
        }

        private ICommand _AddItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (_AddItemCommand == null)
                    _AddItemCommand = new BaseCommand(() => Messenger.Default.Send("PurchaseItem Add Modal"));
                return _AddItemCommand;
            }
        }

        private ICommand _ShowClientsCommand;
        public ICommand ShowClientsCommand
        {
            get
            {
                if (_ShowClientsCommand == null)
                    _ShowClientsCommand = new BaseCommand(() => Messenger.Default.Send("Clients Show"));
                return _ShowClientsCommand;
            }
        }

        private ICommand _AddClientsCommand;
        public ICommand AddClientsCommand
        {
            get
            {
                if (_AddClientsCommand == null)
                    _AddClientsCommand = new BaseCommand(() => Messenger.Default.Send("Clients Add"));
                return _AddClientsCommand;
            }
        }
        #endregion

        #region Helpers

        private void AddItemFromModal(Items newItem)
        {
            if (newItem == null) return;

            PurchaseContractItemExtended extendedItem = new PurchaseContractItemExtended
            {
                ItemId = newItem.item_id,
                ItemName = newItem.name,
                PurchasePrice = newItem.estimated_value,
                Notes = ""
            };

            PurchaseContractItemsList.Add(extendedItem);
            RecalculateTotal();
        }

        private void GetSelectedClient(ClientsExtendedView client)
        {
            if (client != null)
            {
                Client_Id = client.ClientId;
                ClientName_Preview = client.FirstName + " " + client.LastName;
                ClientDoc_Preview = client.DocumentNumber;
            }
        }

        private void RecalculateTotal()
        {
            if (PurchaseContractItemsList == null) return;

            decimal sum = 0;
            foreach (var item in PurchaseContractItemsList)
            {
                sum += item.PurchasePrice;
            }
            Total_Purchase_Price = sum;
        }
        #endregion
    }
}