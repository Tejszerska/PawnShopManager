using MVVMFirma.Models.BussinesLogic;
using MVVMFirma.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.Models.BussinessLogic
{
    public class PawnLoanSummaryB  : DatabaseClass
    {
        private IQueryable<PawnLoans> GetPawnLoans(int? statusId, DateTime startDate, DateTime endDate)
        {
            var query = pawnShopEntities.PawnLoans.Where(pawnLoans =>
                         pawnLoans.is_active == true
                      && pawnLoans.start_date >= startDate
                      && pawnLoans.start_date <= endDate
            );

            if(statusId.HasValue && statusId.Value > 0)
            {
                query = query.Where(pawnLoans =>
                pawnLoans.status_id == statusId.Value);
            }

            return query;

        }

        #region Constructor
        public PawnLoanSummaryB(PawnShopEntities pawnShopEntities) : base(pawnShopEntities)
        {
        }
        #endregion


        #region Bussiness functions
        public decimal GetTotalLoanAmount(int statusId, DateTime startDate, DateTime endDate)
        {
            return GetPawnLoans(statusId, startDate, endDate)
                .Sum(pl => (decimal?)pl.total_loan_amount) ?? 0m;
        }


        public decimal GetTotalInterest(int statusId, DateTime startDate, DateTime endDate)
        {
            return GetPawnLoans(statusId, startDate, endDate)
                .Sum(pl => (decimal?)(
                    (pl.total_loan_amount * (pl.InterestRates.rate_percent / 100.0m)) > pl.InterestRates.minimal_interest
                    ? (pl.total_loan_amount * (pl.InterestRates.rate_percent / 100.0m))
                    : pl.InterestRates.minimal_interest
                )) ?? 0m;
        }


        public int? GetPawnLoanAgreementCount(int statusId, DateTime startDate, DateTime endDate)
        {
            return GetPawnLoans(statusId, startDate, endDate).Count();
        }

        public decimal GetEstimatedCollateral(int statusId, DateTime startDate, DateTime endDate)
        {
            return GetPawnLoans(statusId, startDate, endDate)
                .SelectMany(pl => pl.PawnLoanItems)
                .Sum(pli => (decimal?) pli.Items.estimated_value) ?? 0m;

        }
        #endregion
    }   
}
