using System;
using Foundation;
using MeuPedido.iOS;
using UIKit;

public class CatalogoDataSource : UITableViewSource
{

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
        UITableViewCell cell = tableView.DequeueReusableCell("catalogoTableViewCell") as CatalogoTableViewCell;

        return cell;
    }

    public override nint RowsInSection(UITableView tableview, nint section)
    {
        return 5;
    }
}