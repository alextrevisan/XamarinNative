using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using Java.Interop;

namespace MeuPedido.Droid
{
    public class CatalogListAdapter : BaseAdapter<Product>
    {
        private static List<Product> curLists = new List<Product>();
        readonly Activity myContext;
        private static CatalogListAdapter instance = null;

        public override Product this[int position]
        {
            get
            {
                return curLists[position];
            }
        }

        public CatalogListAdapter(Activity context, List<Product> inpLists) : base()
        {
            this.myContext = context;
            curLists = inpLists;
            instance = this;
        }

        public static void UpdateCatalog()
        {

            var categories = AppData.Categories.FindAll(x => x.Selected);
            if(categories.Count == 0)
            {
                curLists = AppData.Products;
            }
            else
            {
                curLists = AppData.Products.FindAll(x =>
                                categories.Exists(cat => cat.Id == x.Category_id)
                            );
            }
            
            instance.NotifyDataSetChanged();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return curLists.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = myContext.LayoutInflater.Inflate(Resource.Layout.fragment_catalog_item, null);

            var product = curLists[position];

            var pixelToDp = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;

            view.SetMinimumHeight(106 * pixelToDp);

            ImageView productImage = view.FindViewById<ImageView>(Resource.Id.productImage);

            var img = Utils.GetImageBitmapFromUrl(product.Photo);
            productImage.SetImageBitmap(img);

            TextView productTitle = view.FindViewById<TextView>(Resource.Id.productTitle);
            TextView productDiscount = view.FindViewById<TextView>(Resource.Id.productSale);
            TextView productValue = view.FindViewById<TextView>(Resource.Id.productPrice);
            TextView itemCountText = view.FindViewById<TextView>(Resource.Id.quantity);
            RelativeLayout discountLayout = view.FindViewById<RelativeLayout>(Resource.Id.saleLayout);


            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);



            productTitle.Text = product.Name;
            itemCountText.Text = string.Format("{0} UN", quantity);
            productValue.Text = String.Format("R$ {0:0.00}", price).Replace(".", ",");
            itemCountText.Text = string.Format("{0} UN", quantity);
            discountLayout.Visibility = discount <= 0.0 ? ViewStates.Invisible : ViewStates.Visible;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");


            Button addButton = view.FindViewById<Button>(Resource.Id.addButon);
            Button subButton = view.FindViewById<Button>(Resource.Id.subButon);

            addButton.SetOnClickListener(new AddRemoveFromCart(product, 1, view));
            subButton.SetOnClickListener(new AddRemoveFromCart(product, -1, view));

            ImageButton favButton = view.FindViewById<ImageButton>(Resource.Id.favoriteBtn);
            favButton.SetOnClickListener(new FavoriteClickListener(product, favButton));
            favButton.SetImageResource(product.Favorited ? Resource.Mipmap.ic_star : Resource.Mipmap.ic_star_border);

            //Fake section
            var currentSale = AppData.Sales.Find(x => x.Category_id == product.Category_id);
            var hasSale = currentSale != null;

            var isNewCategory = position > 0 && product.Category_id != curLists[position - 1].Category_id;
            var hasSection = position == 0 || isNewCategory;

            var section = view.FindViewById<RelativeLayout>(Resource.Id.section);
            section.Visibility = hasSection ? ViewStates.Visible : ViewStates.Invisible;
            section.LayoutParameters.Height = hasSection ? 32 * pixelToDp : 0;

            var sectionText = view.FindViewById<TextView>(Resource.Id.sectionText);
            sectionText.Text = !hasSection? "" : hasSale ? currentSale.Name : "Outros";

            return view;
        }
    }

    internal class AddRemoveFromCart : Java.Lang.Object, View.IOnClickListener
    {
        readonly private Product product;
        readonly private int size;
        readonly private TextView itemCountText;
        readonly private TextView productDiscount;
        readonly private RelativeLayout discountLayout;
        readonly private TextView productValue;

        public AddRemoveFromCart(Product product, int size, View view)
        {
            this.product = product;
            this.size = size;
            itemCountText = view.FindViewById<TextView>(Resource.Id.quantity);
            productDiscount = view.FindViewById<TextView>(Resource.Id.productSale);
            discountLayout = view.FindViewById<RelativeLayout>(Resource.Id.saleLayout);
            productValue = view.FindViewById<TextView>(Resource.Id.productPrice);


        }
        public void OnClick(View v)
        {
            var _ = size > 0 ? AppData.CurrentCart.Add(product) : AppData.CurrentCart.Remove(product);

            var discount = AppData.CurrentCart.DiscountFor(product);
            var price = AppData.CurrentCart.PriceFor(product);
            var quantity = AppData.CurrentCart.QuantityFor(product);

            itemCountText.Text = string.Format("{0} UN", quantity);
            discountLayout.Visibility = discount <= 0.0 ? ViewStates.Invisible : ViewStates.Visible;
            productDiscount.Text = String.Format("↓{0:0.0}%", discount).Replace(".", ",");
            productValue.Text = string.Format("R$ {0:0.00}", price).Replace(".", ",");
            FragmentCatalog.UpdateBuyButton();
            FragmentCart.UpdateCart();
        }

    }

    internal class FavoriteClickListener : Java.Lang.Object, View.IOnClickListener
    {
        readonly Product product;
        readonly ImageButton favButton;
        public FavoriteClickListener(Product product, ImageButton favButton)
        {
            this.product = product;
            this.favButton = favButton;
        }
        public void OnClick(View v)
        {
            product.Favorited = !product.Favorited;

            favButton.SetImageResource(product.Favorited ? Resource.Mipmap.ic_star : Resource.Mipmap.ic_star_border);
        }
    }
}