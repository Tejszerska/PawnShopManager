using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Validators
{
    public class BusinessValidator
    {
        public static string checkPercent(decimal? input)
        {
            if (input < 0 || input > 100)
                return "Percent should be a number between 0 and 100";
            return null;
        }
        public static string IsDatePastOrToday(DateTime dateToCheck)
        {
            if (dateToCheck < DateTime.Today) return "Date must be in the future";
            return null;
        }
        public static string IsGraterThanZero(decimal number)
        {
            if(number <= 0) return "Must be grater than 0";
            return null;
        }
    }
}
