using GalaSoft.MvvmLight.Messaging;
using MVVMFirma.Helper;
using MVVMFirma.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMFirma.ViewModels.Abstract
{
    // klasa z której beda dziedziczyc wszystkie ViewModele wyswieltajace wszystki obiekty biznesowe
    public abstract class AllViewModel<T> : WorkspaceViewModel
    {
        #region DataBase
        // ten obiekt reprezentuje bd
        protected readonly PawnShopEntities pawnShopEntities;
        #endregion
        #region Command
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null) _LoadCommand = new BaseCommand(Load);
                return _LoadCommand;
            }
        }
        private BaseCommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null) _AddCommand = new BaseCommand(Add);
                return _AddCommand;
            }
        }

        private void Add()
        {
            Messenger.Default.Send(DisplayName + " Add");
        }
        #endregion
        #region Lista
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) Load();
                return _List;

            }
            set
            {
                if (_List != value)
                {
                    _List = value;
                    OnPropertyChanged(() => List); // odswieza wyswietlanie listy
                }
            }
        }

        public abstract void Load();

        #endregion
        #region Constructor
        public AllViewModel()
        {
            pawnShopEntities = new PawnShopEntities();
        }

        #endregion
        #region Sort and search
        public string SortField { get; set; }
        public List<string> SortComboboxItems
        {
            get
            {
                return getComboboxSortList();
            }
        }
        public string SearchField { get; set; }
        private string _SearchTextBox;
        public string SearchTextBox { 
            get
                {
                return _SearchTextBox;
            }
            set
            {
                if (_SearchTextBox != value)
                {
                    _SearchTextBox = value?.ToLower().Trim();
                    OnPropertyChanged(() => SearchTextBox);
                }
            } 
        }
        public List<string> SearchComboboxItems
        {
            get
            {
                return getComboboxSearchList();
            }
        }
        private BaseCommand _SortCommand; // command for sort button
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null) _SortCommand = new BaseCommand(Sort);
                return _SortCommand;
            }
        }
        private BaseCommand _SearchCommand; // command for search button
        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null) _SearchCommand = new BaseCommand(Search);
                return _SearchCommand;
            }
        }
        public abstract void Sort();
        public abstract void Search();
        public abstract List<string> getComboboxSortList();
        public abstract List<string> getComboboxSearchList();
        #endregion
    }
}
