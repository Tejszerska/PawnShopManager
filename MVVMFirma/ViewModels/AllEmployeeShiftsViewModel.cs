using MVVMFirma.Helper;
using MVVMFirma.Models;
using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels;
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
public class AllEmployeeShiftsViewModel : AllViewModel<EmpoloyeeShiftExtendedView>
    {
        #region  Abstract implemented methods
        public override void Sort()
        {
            if(SortField == "Last name")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.OrderBy(x => x.EmployeeLastName));
            }
            if (SortField == "Branch")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.OrderBy(x => x.BranchName));
            }
            if (SortField == "Shift Start")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.OrderBy(x => x.ShiftStart));
            }
            if (SortField == "Shift End")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.OrderBy(x => x.ShiftEnd));
            }
        }

        public override void Search()
        {
            if(SearchField == "Last name")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.Where(x => x.EmployeeLastName != null && x.EmployeeLastName.ToLower().StartsWith(SearchTextBox)));
            }
            if (SearchField == "Branch")
            {
                List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.Where(x => x.BranchName != null && x.BranchName.ToLower().StartsWith(SearchTextBox)));
            }
            
            if (SearchField == "Shift ID")
            {
                if (int.TryParse(SearchTextBox, out int search))
                {
                     List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.Where(x => x.EmployeeShiftId == search));
                }
            }
            if(SearchField == "Shift Start")
            {
                if(DateTime.TryParse(SearchTextBox, out DateTime searchDate))
                {
                    List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.Where(x => x.ShiftStart.Date == searchDate.Date));
                }
            }
            if (SearchField == "Shift End")
            {
                if (DateTime.TryParse(SearchTextBox, out DateTime searchDate))
                {
                    List = new ObservableCollection<EmpoloyeeShiftExtendedView>(List.Where(x => x.ShiftEnd.HasValue && x.ShiftEnd.Value.Date == searchDate.Date));
                }
            }
        }

        public override List<string> getComboboxSortList()
        {
            return new List<string> {"Shift Start", "Shift End" };
        }

        public override List<string> getComboboxSearchList()
        {
            return new List<string> { "Last name", "Shift ID", "Branch", "Shift Start", "Shift End" };
        }

        public override void Load()
        {

            List = new ObservableCollection<EmpoloyeeShiftExtendedView>
                (
                 from employeeShift in pawnShopEntities.EmployeeShifts
                 // where employeeShift.is_active == true zaciagnac od nowa baze
                 select new EmpoloyeeShiftExtendedView
                 {
                     EmployeeShiftId = employeeShift.shift_id,
                     EmployeeFirstName = employeeShift.Employees.first_name,
                     EmployeeLastName = employeeShift.Employees.last_name,
                     ShiftStart = employeeShift.shift_start,
                     ShiftEnd = employeeShift.shift_end,
                     Notes = employeeShift.notes
                 }
                );
        }
        #endregion
        #region Constructor
        public AllEmployeeShiftsViewModel()
            : base()
        {
            base.DisplayName = "Employee Shifts";

        }
        #endregion
    }
}