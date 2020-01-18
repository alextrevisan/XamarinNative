using Foundation;
using System;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class SalesTableViewController : UITableViewController
    {
        public SalesTableViewController (IntPtr handle) : base (handle)
        {
        }
        public override nint RowsInSection(UITableView tableView, nint section)
        {
            
            return AppData.Sales.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            SaleTableViewCell cell = tableView.DequeueReusableCell("saleTableViewCell") as SaleTableViewCell;
            var sale = AppData.Sales[indexPath.Row];
            cell.SetData(sale);
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            AppData.Categories.ForEach(x =>
            {
                x.Selected = x.Id == AppData.Sales[indexPath.Row].Category_id;
            });
            this.TabBarController.SelectedIndex = 0;
        }
    }
}