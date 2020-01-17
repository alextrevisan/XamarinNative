using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class CatalogTableViewController : UITableViewController
    {
        private List<Product> FilteredList = new List<Product>();
        public CatalogTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            loadData();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            FilterProductsByCategory();
            this.catalogListTableView.ReloadData();
        }

        private async void loadData()
        {
            await AppData.GetInstance().UpdateData();
            FilterProductsByCategory();
            this.catalogListTableView.ReloadData();
        }

        private void FilterProductsByCategory()
        {
            var categories = AppData.Categories.FindAll(x => x.Selected);
            FilteredList = AppData.Products.FindAll(x =>
                categories.Exists(cat => cat.Id == x.Category_id)
            );
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return FilteredList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            CatalogTableViewCell cell = tableView.DequeueReusableCell("catalogTableViewCell") as CatalogTableViewCell;
            var product = FilteredList[indexPath.Row];
            cell.SetData(product, AppData.Sales);
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }
    }
}