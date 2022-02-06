using System;

namespace Observer
{
    public class ComingProduct : EventArgs// хранит информацию о поступившем товаре в магазин
    {
        private string _nameProduct;
        private decimal _priceProduct;
        private string _descriptionProduct;
        public string nameProduct => _nameProduct;
        public decimal priceProduct => _priceProduct;
        public string descriptionProduct => _descriptionProduct;

        public ComingProduct(string nameProduct, decimal priceProduct, string descriptionProduct)
        {
            _nameProduct = nameProduct;
            _priceProduct = priceProduct;
            _descriptionProduct = descriptionProduct;
        }
        public override string ToString()
        {
            return $"товар: {nameProduct}, цена: {priceProduct} руб, описание: {descriptionProduct}";
        }
    }
}
