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
    [Register ("CartViewController")]
    partial class CartViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView cartTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel totalItemCount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel totalValue { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (cartTableView != null) {
                cartTableView.Dispose ();
                cartTableView = null;
            }

            if (totalItemCount != null) {
                totalItemCount.Dispose ();
                totalItemCount = null;
            }

            if (totalValue != null) {
                totalValue.Dispose ();
                totalValue = null;
            }
        }
    }
}