using Foundation;
using System;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class SaleTableViewCell : UITableViewCell
    {
        public SaleTableViewCell (IntPtr handle) : base (handle)
        {
        }
        
        public void SetData(Sale sale)
        {
            saleName.Text = sale.Name;
            var category = AppData.Categories.Find(x => x.Id == sale.Category_id);
            if(category != null)
            {
                saleCategory.Text = "Descontos progressivos em "+category.Name;
            }
        }
    }
}