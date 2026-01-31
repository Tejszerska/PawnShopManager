using MVVMFirma.Models;
using MVVMFirma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMFirma.ViewModels
{
    public class NewSalesItemsViewModel : OneViewModel<SalesItems>
    {
        public NewSalesItemsViewModel()
            :base()
        {
            base.DisplayName = "New Sales Items";
            item = new SalesItems();

        }
        #region Properties

        public int Sale_Id
        {
            get { return item.sale_id; }
            set
            {
                if (value != item.sale_id)
                {
                    item.sale_id = value;
                    OnPropertyChanged(() => Sale_Id);
                }
            }
        }

        public int Item_Id
        {
            get { return item.item_id; }
            set
            {
                if (value != item.item_id)
                {
                    item.item_id = value;
                    OnPropertyChanged(() => Item_Id);
                }
            }
        }

        public decimal Sold_Price_For_Item
        {
            get { return item.sold_price_for_item; }
            set
            {
                if (value != item.sold_price_for_item)
                {
                    item.sold_price_for_item = value;
                    OnPropertyChanged(() => Sold_Price_For_Item);
                }
            }
        }

        #endregion

        #region Commands
        public override void Save()
        {
            item.history_id = createRecordHistory();
            pawnShopEntities.SalesItems.Add(item);
            pawnShopEntities.SaveChanges();
        }
        #endregion
    }
}