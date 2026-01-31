using MVVMFirma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BussinesLogic
{
    public class DatabaseClass
    {
        #region Database
        protected PawnShopEntities pawnShopEntities;
        #endregion

        #region Constructor
        public DatabaseClass(PawnShopEntities pawnShopEntities) 
        {
            this.pawnShopEntities = pawnShopEntities;
        }
        #endregion
    }
}
