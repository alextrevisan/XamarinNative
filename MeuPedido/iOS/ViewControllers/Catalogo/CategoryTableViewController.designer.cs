// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MeuPedido.iOS
{
    [Register ("CategoryTableViewController")]
    partial class CategoryTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView categoryTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (categoryTableView != null) {
                categoryTableView.Dispose ();
                categoryTableView = null;
            }
        }
    }
}