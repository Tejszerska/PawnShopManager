using MVVMFirma.Models.EntitiesForView;
using MVVMFirma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BussinesLogic
{
    public class ContractStatusesB : DatabaseClass
    {
        #region Constructor
        public ContractStatusesB(PawnShopEntities pawnShopEntities) : base(pawnShopEntities)
        {
        }
        #endregion

        #region Helping functions
        public IQueryable<KeyAndValue> GetPawnLoansStatusesKeyAndValue()
        {
            var statusesForCB =
                (
                from statuses in pawnShopEntities.ContractStatuses
                where statuses.is_active == true && statuses.contract_type == "PAWN_LOAN"
                select new KeyAndValue
                {
                    Key = statuses.contract_status_id,
                    Value = statuses.name + " (Code: " + statuses.code + ")"
                }
                ).ToList();

            statusesForCB.Insert(0, new KeyAndValue()
            {
                Key = 0,
                Value = "All statuses"
            });

            return statusesForCB.AsQueryable();
        }
        #endregion
    }
} 