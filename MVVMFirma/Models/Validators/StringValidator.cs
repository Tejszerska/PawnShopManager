using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.Validators
{
    public class StringValidator : Validator
    {
        public static string CheckIfStartsWithCapitalLetter(string wartosc)
        {
            try
            {
                if (!char.IsUpper(wartosc, 0))
                {
                    return "Use capital letter.";
                }
            }
            catch (Exception) { }
            ;
            return null;
        }              
    }
}