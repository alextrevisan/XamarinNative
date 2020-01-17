using Foundation;
using System;
using UIKit;
//categoryTableViewCell
namespace MeuPedido.iOS
{
    public partial class CategoryTableViewController : UITableViewController
    {
        public CategoryTableViewController (IntPtr handle) : base (handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return AppData.Categories.Count + 1;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("categoryTableViewCell");
            if (indexPath.Row == 0)
            {
                cell.TextLabel.Text = "Todas as Categorias";
                
                cell.Accessory = AppData.Categories.FindAll(x => x.Selected).Count == AppData.Categories.Count ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
            }
            else
            {
                var category = AppData.Categories[indexPath.Row-1];
                cell.TextLabel.Text = category.Name;
                cell.Accessory = AppData.Categories[indexPath.Row-1].Selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;
            }
            
            
            return cell;
        }

        
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            if(indexPath.Row == 0)
            {
                if(AppData.Categories.FindAll(x => x.Selected).Count == AppData.Categories.Count)
                {
                    AppData.Categories.ForEach(x => x.Selected = false);
                }
                else
                {
                    AppData.Categories.ForEach(x => x.Selected = true);
                }
            }
            else
            {
                AppData.Categories[indexPath.Row-1].Selected = !AppData.Categories[indexPath.Row-1].Selected;
            }
            
            categoryTableView.ReloadData();
        }
    }
}