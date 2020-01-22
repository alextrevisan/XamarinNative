using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MeuPedido.iOS
{
    public partial class CatalogTableViewController : UITableViewController
    {
        private List<Product> FilteredList = new List<Product>();
        private Product currentProduct;
        public CatalogTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            LoadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            FilterProductsByCategory();
            this.catalogListTableView.ReloadData();
        }

        private async void LoadData()
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

            if(categories.Count == AppData.Categories.Count)
            {
                FilteredList.AddRange(AppData.Products.FindAll(x => x.Category_id == null));
            }
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return AppData.Sales.Count + 1;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return AppData.Sales.Count > 0 && AppData.Sales.Count > section ? AppData.Sales[(int)section].Name : "Outros";
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return ProductsInSection((int)section).Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            CatalogTableViewCell cell = tableView.DequeueReusableCell("catalogTableViewCell") as CatalogTableViewCell;
            var product = ProductsInSection(indexPath.Section)[indexPath.Row];
            cell.SetData(product);
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //base.RowSelected(tableView, indexPath);
            currentProduct = ProductsInSection(indexPath.Section)[indexPath.Row];            
            this.PerformSegue("showDetailSegue", this);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "showDetailSegue")
            {
                ProductDetailViewController vc = segue.DestinationViewController as ProductDetailViewController;
                vc.SetProduct(currentProduct);
            }
        }

        private List<Product> ProductsInSection(int section)
        {
            if (section < AppData.Sales.Count)
            {
                return FilteredList.FindAll(x => x.Category_id == AppData.Sales[(int)section].Category_id);
            }
            return FilteredList.FindAll(x => !AppData.Sales.Exists(sale => sale.Category_id == x.Category_id));
        }
    }
}