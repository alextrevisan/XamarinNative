using Foundation;
using System;
using System.Collections.Generic;
using System.Drawing;
using UIKit;
using static MeuPedido.Sale;

namespace MeuPedido.iOS
{
    public partial class CatalogTableViewCell : UITableViewCell
    {
        private Product CurrentProduct;
        private List<Sale> CurrentSales;
        public CatalogTableViewCell (IntPtr handle) : base (handle)
        {

        }

        public void SetData(Product product, List<Sale> sales)
        {
            CurrentProduct = product;
            CurrentSales = sales;

            productTitle.Text = product.Name;
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            productImage.Image = Utils.UIImageFromUrl(product.Photo);

                      
            
            

            UpdateSale();
            UpdateFavorite();
            CreateButtonEvents();

            
        }

        void UpdateSale()
        {
            var currentSale = CurrentSales.Find(x => x.Category_id == CurrentProduct.Category_id);
            var currentDiscount = LoadCurrentDiscount(currentSale);
            if (currentDiscount > 0.0)
            {
                productDiscount.Hidden = false;
                CurrentProduct.SalePrice = CurrentProduct.Price - (CurrentProduct.Price * currentDiscount) / 100.0;
                productDiscount.Text = String.Format("↓{0:0.0}%", currentDiscount).Replace(".", ",");
                //@TODO corrigir a formatacão do preço
                productValue.Text = String.Format("R$ {0:0.00}", CurrentProduct.SalePrice).Replace(".", ",");
            }
            else
            {
                CurrentProduct.SalePrice = CurrentProduct.Price;
                productDiscount.Hidden = true;
                //@TODO corrigir a formatacão do preço
                productValue.Text = String.Format("R$ {0:0.00}", CurrentProduct.SalePrice).Replace(".", ",");
            }
        }

        void UpdateFavorite()
        {
            if(CurrentProduct.Favorited)
            {
                favoritedBtn.SetTitle("★", UIControlState.Normal);
                favoritedBtn.TintColor = UIColor.Gray;
            }
            else
            {
                favoritedBtn.SetTitle("☆", UIControlState.Normal);
                favoritedBtn.TintColor = UIColor.LightGray;
            }
        }

        private double LoadCurrentDiscount(Sale currentSale)
        {
            if (currentSale != null)
            {
                currentSale.Policies.Sort((x, y) => x.Min > y.Min ? -1 : 1);
                var currentPolicy = currentSale.Policies.Find(x => x.Min <= CurrentProduct.ItemCount);
                if(currentPolicy != null)
                {
                    return currentPolicy.Discount;
                }
            }
            else
            {
                productDiscount.Hidden = true;
            }
            return 0;
        }

        private void CreateButtonEvents()
        {
            addItemBtn.TouchUpInside -= OnAddButtonClicked;
            subItemBtn.TouchUpInside -= OnSubButtonClicked;
            favoritedBtn.TouchUpInside -= OnFavoriteButtonClicked;

            addItemBtn.TouchUpInside += OnAddButtonClicked;
            subItemBtn.TouchUpInside += OnSubButtonClicked;
            favoritedBtn.TouchUpInside += OnFavoriteButtonClicked;
        }

        void OnAddButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.ItemCount++;
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            UpdateSale();
        }

        void OnSubButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.ItemCount--;
            CurrentProduct.ItemCount = Math.Max(CurrentProduct.ItemCount, 0);

            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            UpdateSale();
        }

        void OnFavoriteButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.Favorited = !CurrentProduct.Favorited;
            UpdateFavorite();
        }
    }
}