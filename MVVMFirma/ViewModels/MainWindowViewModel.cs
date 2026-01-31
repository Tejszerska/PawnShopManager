using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MVVMFirma.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region FirstAdditionalFeature
        // top menu
        #region Fields1

        // Contracts Menu
        private ICommand _ShowPawnLoanOverviewCommand;
        private ICommand _AddInterestRatesCommand;
        private ICommand _ShowInterestRatesCommand;
        private ICommand _AddPurchaseContractCommand;
        private ICommand _ShowPurchaseContractCommand;
        private ICommand _AddPawnLoanCommand;
        private ICommand _ShowPawnLoanCommand;
        private ICommand _AddClientsCommand;
        private ICommand _ShowClientsCommand;

        //  Inventory Menu     
        private ICommand _ShowPriceChangesCommand;        
        private ICommand _AddCategoryCommand;
        private ICommand _ShowCategoryCommand;        
        private ICommand _ShowInventoryAgingCommand;        
        private ICommand _ShowCategoryStatisticsCommand;        
        private ICommand _ShowItemsCommand;
        private ICommand _ShowPawnItemsCommand;
        private ICommand _AddItemsCommand;

        //  Sales Menu
        private ICommand _AddSalesCommand;
        private ICommand _ShowSalesCommand;
        private ICommand _AddOnlineOffersCommand;
        private ICommand _ShowOnlineOffersCommand;
        private ICommand _AddPaymentsCommand;
        private ICommand _ShowPaymentsCommand;

        // Network Menu
        private ICommand _ShowBranchesCommand;
        private ICommand _AddBranchesCommand;
        private ICommand _ShowEmployeesCommand;
        private ICommand _AddEmployeesCommand;
        private ICommand _ShowEmployeesShiftsCommand;

        #endregion

        #region Commands1
        // Contracts Menu
     
        public ICommand ShowPawnLoanOverviewCommand
        {
            get
            {
                if (_ShowPawnLoanOverviewCommand == null)
                {
                    _ShowPawnLoanOverviewCommand = new BaseCommand(() => ShowPawnLoanOverview());
                }
                return _ShowPawnLoanOverviewCommand;
            }
        }
        public ICommand ShowInterestRatesCommand
        {
            get
            {
                if (_ShowInterestRatesCommand == null)
                {
                    _ShowInterestRatesCommand = new BaseCommand(() => ShowInterestRates());
                }
                return _ShowInterestRatesCommand;
            }
        }
        public ICommand AddPurchaseContractCommand
        {
            get
            {
                if (_AddPurchaseContractCommand == null)
                {
                    _AddPurchaseContractCommand = new BaseCommand(() => AddPurchaseContract());
                }
                return _AddPurchaseContractCommand;
            }
        }
        public ICommand ShowPurchaseContractCommand
        {
            get
            {
                if (_ShowPurchaseContractCommand == null)
                {
                    _ShowPurchaseContractCommand = new BaseCommand(() => ShowPurchaseContract());
                }
                return _ShowPurchaseContractCommand;
            }
        }
        public ICommand AddPawnLoanCommand
        {
            get
            {
                if (_AddPawnLoanCommand == null)
                {
                    _AddPawnLoanCommand = new BaseCommand(() => AddPawnLoan());
                }
                return _AddPawnLoanCommand;
            }
        }
        public ICommand ShowPawnLoanCommand
        {
            get
            {
                if (_ShowPawnLoanCommand == null)
                {
                    _ShowPawnLoanCommand = new BaseCommand(() => ShowPawnLoan());
                }
                return _ShowPawnLoanCommand;
            }
        }
        public ICommand AddClientsCommand
        {
            get
            {
                if (_AddClientsCommand == null)
                {
                    _AddClientsCommand = new BaseCommand(() => AddClients());
                }
                return _AddClientsCommand;
            }
        }
        public ICommand ShowClientsCommand
        {
            get
            {
                if (_ShowClientsCommand == null)
                {
                    _ShowClientsCommand = new BaseCommand(() => ShowClients());
                }
                return _ShowClientsCommand;
            }
        }
        public ICommand AddInterestRatesCommand
        {
            get
            {
                if (_AddInterestRatesCommand == null)
                {
                    _AddInterestRatesCommand = new BaseCommand(() => AddInterestRates());
                }
                return _AddInterestRatesCommand;
            }
        }
        // Inventory Menu
        public ICommand ShowPriceChangesCommand
        {
            get
            {
                if (_ShowPriceChangesCommand == null)
                {
                    _ShowPriceChangesCommand = new BaseCommand(() => ShowPriceChanges());
                }
                return _ShowPriceChangesCommand;
            }
        }
        public ICommand AddCategoryCommand
        {
            get
            {
                if (_AddCategoryCommand == null)
                {
                    _AddCategoryCommand = new BaseCommand(() => AddCategory());
                }
                return _AddCategoryCommand;
            }
        }
        public ICommand ShowCategoryCommand
        {
            get
            {
                if (_ShowCategoryCommand == null)
                {
                    _ShowCategoryCommand = new BaseCommand(() => ShowCategory());
                }
                return _ShowCategoryCommand;
            }
        }
        public ICommand ShowInventoryAgingCommand
        {
            get
            {
                if (_ShowInventoryAgingCommand == null)
                {
                    _ShowInventoryAgingCommand = new BaseCommand(() => ShowInventoryAging());
                }
                return _ShowInventoryAgingCommand;
            }
        }
        public ICommand ShowCategoryStatisticsCommand
        {
            get
            {
                if (_ShowCategoryStatisticsCommand == null)
                {
                    _ShowCategoryStatisticsCommand = new BaseCommand(() => ShowCategoryStatistics());
                }
                return _ShowCategoryStatisticsCommand;
            }
        }
      
        public ICommand ShowItemsCommand
        {
            get
            {
                if (_ShowItemsCommand == null)
                {
                    _ShowItemsCommand = new BaseCommand(() => ShowItems());
                }
                return _ShowItemsCommand;
            }
        }

        public ICommand ShowPawnItemsCommand
        {
            get
            {
                if (_ShowPawnItemsCommand == null)
                {
                    _ShowPawnItemsCommand = new BaseCommand(() => ShowPawnItems());
                }
                return _ShowPawnItemsCommand;
            }
        }

        public ICommand AddItemsCommand
        {
            get
            {
                if (_AddItemsCommand == null)
                {
                    _AddItemsCommand = new BaseCommand(() => AddItems());
                }
                return _AddItemsCommand;
            }
        }

        // Sales Menu

        public ICommand AddSalesCommand
        {
            get
            {
                if (_AddSalesCommand == null)
                {
                    _AddSalesCommand = new BaseCommand(() => AddSales());
                }
                return _AddSalesCommand;
            }
        }
        public ICommand ShowSalesCommand
        {
            get
            {
                if (_ShowSalesCommand == null)
                {
                    _ShowSalesCommand = new BaseCommand(() => ShowSales());
                }
                return _ShowSalesCommand;
            }
        }
        public ICommand AddOnlineOffersCommand
        {
            get
            {
                if (_AddOnlineOffersCommand == null)
                {
                    _AddOnlineOffersCommand = new BaseCommand(() => AddOnlineOffers());
                }
                return _AddOnlineOffersCommand;
            }
        }
        public ICommand ShowOnlineOffersCommand
        {
            get
            {
                if (_ShowOnlineOffersCommand == null)
                {
                    _ShowOnlineOffersCommand = new BaseCommand(() => ShowOnlineOffers());
                }
                return _ShowOnlineOffersCommand;
            }
        }
        public ICommand AddPaymentsCommand
        {
            get
            {
                if (_AddPaymentsCommand == null)
                {
                    _AddPaymentsCommand = new BaseCommand(() => AddPayments());
                }
                return _AddPaymentsCommand;
            }
        }
        public ICommand ShowPaymentsCommand
        {
            get
            {
                if (_ShowPaymentsCommand == null)
                {
                    _ShowPaymentsCommand = new BaseCommand(() => ShowPayments());
                }
                return _ShowPaymentsCommand;
            }
        }

        // Network Menu
        public ICommand ShowBranchesCommand
        {
            get
            {
                if (_ShowBranchesCommand == null)
                {
                    _ShowBranchesCommand = new BaseCommand(() => ShowBranches());
                }
                return _ShowBranchesCommand;
            }
        }
        public ICommand AddBranchesCommand
        {
            get
            {
                if (_AddBranchesCommand == null)
                {
                    _AddBranchesCommand = new BaseCommand(() => AddBranches());
                }
                return _AddBranchesCommand;
            }
        }
        public ICommand ShowEmployeesCommand
        {
            get
            {
                if (_ShowEmployeesCommand == null)
                {
                    _ShowEmployeesCommand = new BaseCommand(() => ShowEmployees());
                }
                return _ShowEmployeesCommand;
            }
        }
        public ICommand AddEmployeesCommand
        {
            get
            {
                if (_AddEmployeesCommand == null)
                {
                    _AddEmployeesCommand = new BaseCommand(() => AddEmployees());
                }
                return _AddEmployeesCommand;
            }
        }
        public ICommand ShowEmployeesShiftsCommand
        {
            get
            {
                if (_ShowEmployeesShiftsCommand == null)
                {
                    _ShowEmployeesShiftsCommand = new BaseCommand(() => ShowEmployeesShifts());
                }
                return _ShowEmployeesShiftsCommand;
            }
        }
        #endregion

        #region Helper1
        
        // Contracts Menu         
        private void ShowPawnLoanOverview()
        {
            this.CreateView(new PawnLoansRaportViewModel());
          }
        private void ShowInterestRates()
        {
            this.ShowAllView<AllInterestRatesViewModel>();
        }
        private void AddPurchaseContract()
        {
            this.CreateView(new NewPurchaseContractViewModel());
        }
        private void ShowPurchaseContract()
        {
            this.ShowAllView<AllPurchaseContractsViewModel>();
        }
        private void AddPawnLoan()
        {
            this.CreateView(new NewPawnLoanViewModel());
        }
        private void ShowPawnLoan()
        {
            this.ShowAllView<AllPawnLoansViewModel>();
        }
        private void AddClients()
        {
            this.CreateView(new NewClientViewModel());
        }
        private void ShowClients()
        {
            this.ShowAllView<AllClientsViewModel>();
        }

        private void AddInterestRates()
        {
            this.CreateView(new NewInterestRateViewModel());
        }
        // Inventory Menu 
        private void ShowPriceChanges()
        {
            this.ShowAllView<AllPriceHistoryViewModel>();
        }
        private void AddCategory()
        {
            this.CreateView(new NewCategoryViewModel());
        }
        private void ShowCategory()
        {
            this.ShowAllView<AllCategoriesViewModel>();
        }
        private void ShowInventoryAging()
        {
            this.ShowAllView<InventoryAgingRaportViewModel>();
        }
        private void ShowCategoryStatistics()
        {
            this.CreateView(new CategoryStatsRaportViewModel());
        }
        private void ShowItems()
        {
            this.ShowAllView<AllItemsViewModel>();
        }
        private void ShowPawnItems()
        {
            this.ShowAllView<AllPawnLoanItemsViewModel>();
        }

        private void AddItems()
        {
            this.CreateView(new NewItemViewModel());
        }
        // Sales Menu 
        private void AddSales()
        {
            this.CreateView(new NewSaleViewModel());
        }
        private void ShowSales()
        {
            this.ShowAllView<AllSalesViewModel>();
        }
        private void AddOnlineOffers()
        {
            this.CreateView(new NewOnlineSaleOfferViewModel());
        }
        private void ShowOnlineOffers()
        {
            this.ShowAllView<AllOnlineSaleOffersViewModel>();
        }
        private void AddPayments()
        {
            this.CreateView(new NewPaymentViewModel());
        }
        private void ShowPayments()
        {
            this.ShowAllView<AllPaymentsViewModel>();
        }

        // Network Menu
        private void ShowBranches()
        {
            this.ShowAllView<AllBranchesViewModel>();
        }
        private void AddBranches()
        {
            this.CreateView(new NewBranchViewModel());
        }
        private void ShowEmployees()
        {
            this.ShowAllView<AllEmployeesViewModel>();
        }
        private void AddEmployees()
        {
            this.CreateView(new NewEmployeeViewModel());
        }
        private void ShowEmployeesShifts()
        {
            this.ShowAllView<AllEmployeeShiftsViewModel>();
        }
        #endregion
        #endregion

        #region SecondAdditionalFeature
        //dark mode
        private bool _isDarkMode = true;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                ((App)Application.Current).UpdateTheme(_isDarkMode); // Call the method from App.xaml.cs 
                OnPropertyChanged(() => IsDarkMode); //notify UI about the change

            }
        }

        #endregion

        #region Fields
        private ReadOnlyCollection<CommandViewModel> _Commands;
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        #endregion

        #region Commands


        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_Commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            Messenger.Default.Register<string>(this, open);

            return new List<CommandViewModel>
            {
                new CommandViewModel("Pawn Loans Overview", ShowPawnLoanOverviewCommand, "\uE9D2"),
                new CommandViewModel("New Pawn Loan", AddPawnLoanCommand, "\uE710"),
                new CommandViewModel("Pawn Loan Contracts", ShowPawnLoanCommand, "\uE8A5"),
                new CommandViewModel("New Purchase Contract", AddPurchaseContractCommand, "\uE82D"),
                new CommandViewModel("Purchase Contacts", ShowPurchaseContractCommand, "\uE77B"),
                new CommandViewModel("Interest Rates", ShowInterestRatesCommand, "\uE94C"),
                new CommandViewModel("Items", ShowItemsCommand, "\uE7B8"),
                new CommandViewModel("Aging Inventory", ShowInventoryAgingCommand, "\uE81C"),
                new CommandViewModel("Categories Statistics", ShowCategoryStatisticsCommand, "\uE99A"),
                new CommandViewModel("New Sale", AddSalesCommand, "\uE8A1"),
                new CommandViewModel("Sales", ShowSalesCommand, "\uE890"),
                new CommandViewModel("New Payment", AddPaymentsCommand, "\uE8C7"),
                new CommandViewModel("Payments", ShowPaymentsCommand, "\uE171"),
                new CommandViewModel("Branches", ShowBranchesCommand, "\uE707"),
                new CommandViewModel("Employees", ShowEmployeesCommand, "\uE779")
            };
        }
        #endregion

        #region Workspaces
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }
        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion 

        #region Private Helpers
        private void CreateView(WorkspaceViewModel workspace)
        {
            this.Workspaces.Add(workspace);
            this.SetActiveWorkspace(workspace);
        }
        private void ShowAllView<T>() where T : WorkspaceViewModel, new()
        {
            T workspace =
                this.Workspaces.FirstOrDefault(vm => vm is T)
                as T;
            if (workspace == null)
            {
                workspace = new T();
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);

        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        private void open(string name)
        {
            if (name == "Pawn Loans Add") AddPawnLoan();
            if (name == "Purchase Contracts Add") AddPurchaseContract();
            if (name == "Clients Add") AddClients();
            if (name == "Interest Rates Add") AddInterestRates();
            if (name == "Categories Add") AddCategory();
            if (name == "Items Add") AddItems();
            if (name == "Sales Add") AddSales();
            if (name == "Online Offers Add") AddOnlineOffers();
            if (name == "Payments Add") AddPayments();
            if (name == "Branches Add") AddBranches();
            if (name == "Employees Add") AddEmployees();

            if (name == "Payments Show") ShowModalPayments();
            if (name == "Items Show") ShowModalItems();
            if (name == "Offers Show") ShowModalOffers();
            if (name == "Items Add Modal") ShowModalNewItem();
            if (name == "PawnItem Add Modal") ShowModalNewPawnItem();
            if (name == "PurchaseItem Add Modal") ShowModalNewPurchaseItem();
            if (name == "Clients Show") ShowModalClients();



        }
        #endregion

        #region Modal windows
        //for adding with FK

        private void ShowModalClients()
        {
            AllClientsViewModel vm = new AllClientsViewModel();
            AllClientsView view = new AllClientsView();

            view.DataContext = vm;

            Window window = new Window
            {
                Title = "Select Client",
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.NoResize
            };

            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");

            vm.RequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }
        private void ShowModalPayments()
        {
           AllPaymentsViewModel vm = new AllPaymentsViewModel();
           AllPayments view = new AllPayments();

              view.DataContext = vm;
            Window window = new Window
            {
                Title = "Select Payment",
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.NoResize
            };
            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");

            vm.RequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }

        private void ShowModalItems()
        {
            AllItemsViewModel vm = new AllItemsViewModel();
            AllItems  view = new AllItems();

            view.DataContext = vm;
            Window window = new Window
            {
                Title = "Select Item",
                Content = view,
                SizeToContent = SizeToContent.Manual,
                Height = 600,
                Width = 1000,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.CanResize
            };
            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");

            vm.RequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }
        private void ShowModalOffers()
        {
            AllOnlineSaleOffersViewModel vm = new AllOnlineSaleOffersViewModel();
            AllOnlineSaleOffers view = new AllOnlineSaleOffers(); 

            view.DataContext = vm;

            Window window = new Window
            {
                Title = "Select Online Offer",
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.NoResize
            };

            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");
           
            vm.RequestClose += (s, e) => window.Close();

            window.ShowDialog();
        }

        private void ShowModalNewItem()
        {
            NewItemViewModel vm = new NewItemViewModel();
            NewItem view = new NewItem(); 

            view.DataContext = vm;

            Window window = new Window
            {
                Title = "Create New Item", 
                Content = view,            
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize, 
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow 
            };

              window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");
            

              vm.RequestClose += (s, e) => window.Close();

            window.ShowDialog();
        }

        private void ShowModalNewPawnItem()
        {
            NewPawnLoanItemViewModel vm = new NewPawnLoanItemViewModel();
            NewPawnLoanItemView view = new NewPawnLoanItemView();
            view.DataContext = vm;

            Window window = new Window
            {
                Title = "Add Item to Pawn",
                Content = view,
                SizeToContent = SizeToContent.Manual,
                Height = 600,
                Width = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.CanResize
            };
            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");

            vm.RequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }

        private void ShowModalNewPurchaseItem()
        {
            NewPurchaseContractItemViewModel vm = new NewPurchaseContractItemViewModel();
            NewPurchaseContractItem view = new NewPurchaseContractItem();
            view.DataContext = vm;

            Window window = new Window
            {
                Title = "Add Item to Purchase",
                Content = view,
                SizeToContent = SizeToContent.Manual,
                Height = 600,
                Width = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Owner = Application.Current.MainWindow,
                ResizeMode = ResizeMode.CanResize
            };
            window.SetResourceReference(Window.BackgroundProperty, "WorkspaceBg");

            vm.RequestClose += (s, e) => window.Close();
            window.ShowDialog();
        }
        #endregion
    }
}
