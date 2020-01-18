using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace MeuPedido.iOS
{
    public class TableSource : UITableViewSource
    {
        private List<Product> CartList = new List<Product>();
        public TableSource(List<Product> items)
        {
            CartList = items;
        }

        public void UpdateCartList(List<Product> items)
        {
            CartList = items;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return CartList.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            CartTableViewCell cell = tableView.DequeueReusableCell("cartTableViewCell") as CartTableViewCell;
            var product = CartList[indexPath.Row];
            cell.SetData(product, AppData.Sales);
            return cell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }
    }

    public partial class CartViewController : UIViewController
    {
        private List<Product> CartList = new List<Product>();
        private TableSource tableSource = new TableSource(new List<Product>());
        public CartViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            FilterProductsOnCart();
            tableSource = new TableSource(CartList);
            cartTableView.Source = tableSource;
            cartTableView.TableFooterView = new UIView();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        
            FilterProductsOnCart();
            tableSource.UpdateCartList(CartList);
            CalculateTotal();
            cartTableView.ReloadData();

        }
        private void FilterProductsOnCart()
        {
            CartList = AppData.Products.FindAll(x =>
                x.ItemCount > 0
            );
        }

        private void CalculateTotal()
        {
            long itemCount = 0;
            double valueTotal = 0;
            CartList.ForEach(x => {
                itemCount += x.ItemCount;
                valueTotal += x.ItemCount * x.SalePrice;
            });

            totalItemCount.Text = itemCount + " UN";
            totalValue.Text = String.Format("R$ {0:0.00}", valueTotal).Replace(".", ",");


        }

    }
}