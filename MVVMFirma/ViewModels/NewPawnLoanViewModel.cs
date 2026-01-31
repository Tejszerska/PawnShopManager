using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.Models.Validators;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MVVMFirma.ViewModels
{
    public class NewPawnLoanViewModel : OneViewModel<PawnLoans>, IDataErrorInfo
    {
        #region Constructor
        public NewPawnLoanViewModel()
            : base()
        {
            base.DisplayName = "New Pawn Loan";
            item = new PawnLoans();
            item.start_date = DateTime.Now;
            item.due_date = DateTime.Now;

            PawnLoanItemsList = new ObservableCollection<PawnLoanItemExtended>();
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

        public DateTime Start_Date
        {
            get { return item.start_date; }
            set
            {
                if (value != item.start_date)
                {
                    item.start_date = value;
                    OnPropertyChanged(() => Start_Date);
                }
            }
        }

        public DateTime Due_Date
        {
            get { return item.due_date; }
            set
            {
                if (value != item.due_date)
                {
                    item.due_date = value;
                    OnPropertyChanged(() => Due_Date);
                }
            }
        }

        public decimal Total_Loan_Amount
        {
            get { return item.total_loan_amount; }
            set
            {
                if (value != item.total_loan_amount)
                {
                    item.total_loan_amount = value;
                    OnPropertyChanged(() => Total_Loan_Amount);
                }
            }
        }

        public int Interest_Rate_Id
        {
            get { return item.interest_rate_id; }
            set
            {
                if (value != item.interest_rate_id)
                {
                    item.interest_rate_id = value;
                    OnPropertyChanged(() => Interest_Rate_Id);
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

        public int? Previous_Loan_Id
        {
            get { return item.previous_loan_id; }
            set
            {
                if (value != item.previous_loan_id)
                {
                    item.previous_loan_id = value;
                    OnPropertyChanged(() => Previous_Loan_Id);
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
        /// LAB5
        public IQueryable<ContractStatuses> ContractStatusesItems
        {
            get
            {
                return pawnShopEntities.ContractStatuses.Where(x => x.is_active == true && x.contract_type == "PAWN_LOAN").ToList().AsQueryable();
            }
        }
        public IQueryable<InterestRates> InterestRatesItems
        {
            get
            {
                return pawnShopEntities.InterestRates.Where(x => x.is_active == true).ToList().AsQueryable();
            }
        }
        private ObservableCollection<PawnLoanItemExtended> _PawnLoanItemsList;
        public ObservableCollection<PawnLoanItemExtended> PawnLoanItemsList
        {
            get { return _PawnLoanItemsList; }
            set
            {
                _PawnLoanItemsList = value;
                OnPropertyChanged(() => PawnLoanItemsList);
            }
        }

        private string _ClientName;
        public string ClientName
        {
            get { return _ClientName; }
            set
            {
                if (value != _ClientName)
                {
                    _ClientName = value;
                    OnPropertyChanged(() => ClientName);
                }
            }
        }

        private string _ClientDoc;
        public string ClientDoc
        {
            get { return _ClientDoc; }
            set
            {
                if (value != _ClientDoc)
                {
                    _ClientDoc = value;
                    OnPropertyChanged(() => ClientDoc);
                }
            }
        }
        #endregion

        #region Commands
        public override void Save()
        {
            if (PawnLoanItemsList == null || PawnLoanItemsList.Count == 0)
            {
                MessageBox.Show("Add items first!");
                return;
            }

            RecalculateTotal();

            item.is_active = true;
            item.history_id = createRecordHistory();
            pawnShopEntities.PawnLoans.Add(item);
            pawnShopEntities.SaveChanges();

            foreach (PawnLoanItemExtended viewItem in PawnLoanItemsList)
            {
                PawnLoanItems dbItem = new PawnLoanItems
                {
                    pawn_loan_id = item.pawn_loan_id,
                    item_id = viewItem.ItemId,
                    loan_amount_for_item = viewItem.LoanAmount,
                    notes = "", // uzupełnic view
                    history_id = createRecordHistory()
                };

                pawnShopEntities.PawnLoanItems.Add(dbItem);
                Items originalItem = pawnShopEntities.Items.FirstOrDefault(x => x.item_id == viewItem.ItemId);
                if (originalItem != null)
                {
                    originalItem.acquisition_source_id = item.pawn_loan_id;
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
                    _AddItemCommand = new BaseCommand(() => Messenger.Default.Send("PawnItem Add Modal"));
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
        #endregion
        #region Helpers

        private void AddItemFromModal(Items newItem)
        {
            if (newItem == null) return;

            PawnLoanItemExtended extendedItem = new PawnLoanItemExtended
            {
                ItemId = newItem.item_id,
                ItemName = newItem.name,
                LoanAmount = newItem.estimated_value
            };

            PawnLoanItemsList.Add(extendedItem);
            RecalculateTotal();

        }

        private void RecalculateTotal()
        {
            if (PawnLoanItemsList == null) return;

            decimal sum = 0;
            foreach (var item in PawnLoanItemsList)
            {
                sum += item.LoanAmount;
            }
            Total_Loan_Amount = sum;
        }
        private void GetSelectedClient(ClientsExtendedView client)
        {
            if (client != null)
            {
                Client_Id = client.ClientId;

                ClientName = client.FirstName + " " + client.LastName;
                ClientDoc = client.DocumentNumber;
            }
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

               
                if (columnName == "Due_Date")
                {
                    message = BusinessValidator.IsDatePastOrToday(this.Due_Date);
                }

                return message;
            }
        }

        public override bool IsValid()
        {
            if (this["Due_date"] == null)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}
