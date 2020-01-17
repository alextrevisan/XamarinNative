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

        void OnAddButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.ItemCount++;
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            UpdateSale();
        }

        void OnSubButtonClicked(object sender, EventArgs args)
        {
            CurrentProduct.ItemCount--;
            if(CurrentProduct.ItemCount < 0)
            {
                CurrentProduct.ItemCount = 0;
            }
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            UpdateSale();
        }

        public void SetData(Product product, List<Sale> sales)
        {
            CurrentProduct = product;
            CurrentSales = sales;

            productTitle.Text = product.Name;
            itemCountText.Text = CurrentProduct.ItemCount + " UN";
            productImage.Image = Utils.UIImageFromUrl(product.Photo);

                      
            
            //@TODO corrigir a formatacão do preço
            productValue.Text = String.Format("R$ {0:0.00}", product.Price).Replace(".",",");

            UpdateSale();
            CreateButtonEvents();
        }

        void UpdateSale()
        {
            var currentSale = CurrentSales.Find(x => x.Category_id == CurrentProduct.Category_id);
            var currentDiscount = LoadCurrentDiscount(currentSale);
            if (currentDiscount != null)
            {
                productDiscount.Hidden = false;
                productDiscount.Text = currentDiscount;
            }
            else
            {
                productDiscount.Hidden = true;
            }
        }

        private string LoadCurrentDiscount(Sale currentSale)
        {
            if (currentSale != null)
            {
                currentSale.Policies.Sort((x, y) => x.Min > y.Min ? -1 : 1);
                var currentPolicy = currentSale.Policies.Find(x => x.Min <= CurrentProduct.ItemCount);
                if(currentPolicy != null)
                {
                    return String.Format("↓{0:0.0}%", currentPolicy.Discount).Replace(".",",");
                }
            }
            else
            {
                productDiscount.Hidden = true;
            }
            return null;
        }

        private void CreateButtonEvents()
        {
            addItemBtn.TouchUpInside -= OnAddButtonClicked;
            subItemBtn.TouchUpInside -= OnSubButtonClicked;

            addItemBtn.TouchUpInside += OnAddButtonClicked;
            subItemBtn.TouchUpInside += OnSubButtonClicked;
        }
    }
}