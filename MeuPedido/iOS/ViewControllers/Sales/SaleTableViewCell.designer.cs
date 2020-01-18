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
    [Register ("SaleTableViewCell")]
    partial class SaleTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel saleCategory { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel saleName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (saleCategory != null) {
                saleCategory.Dispose ();
                saleCategory = null;
            }

            if (saleName != null) {
                saleName.Dispose ();
                saleName = null;
            }
        }
    }
}