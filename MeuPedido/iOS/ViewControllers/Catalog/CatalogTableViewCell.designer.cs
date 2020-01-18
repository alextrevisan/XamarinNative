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
    [Register ("CatalogTableViewCell")]
    partial class CatalogTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addItemBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton favoritedBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel itemCountText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel productDiscount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView productImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel productTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel productValue { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton subItemBtn { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (addItemBtn != null) {
                addItemBtn.Dispose ();
                addItemBtn = null;
            }

            if (favoritedBtn != null) {
                favoritedBtn.Dispose ();
                favoritedBtn = null;
            }

            if (itemCountText != null) {
                itemCountText.Dispose ();
                itemCountText = null;
            }

            if (productDiscount != null) {
                productDiscount.Dispose ();
                productDiscount = null;
            }

            if (productImage != null) {
                productImage.Dispose ();
                productImage = null;
            }

            if (productTitle != null) {
                productTitle.Dispose ();
                productTitle = null;
            }

            if (productValue != null) {
                productValue.Dispose ();
                productValue = null;
            }

            if (subItemBtn != null) {
                subItemBtn.Dispose ();
                subItemBtn = null;
            }
        }
    }
}