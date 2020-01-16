// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MeuPedido.iOS
{
    [Register ("CatalogoTableViewController")]
    partial class CatalogoTableViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView catalogoListTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (catalogoListTableView != null) {
                catalogoListTableView.Dispose ();
                catalogoListTableView = null;
            }
        }
    }
}