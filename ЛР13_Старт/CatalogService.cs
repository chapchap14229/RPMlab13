using System;
using System.Collections.Generic;
using System.IO;

namespace Lab13
{
    public class CatalogService
    {
        private const int Zero = 0;
        private const double DiscountRate = 0.25;
        private const int ManyThreshold = 5;
        private const int LowStockThreshold = 3;
        private const string AllManufacturers = "все";
        private const string ManyText = "много";
        private const string FewText = "мало";
        private const string DefaultImage = "picture.png";

        public List<Product> Products { get; private set; }


//Конструктор Создает новый список продуктов
        public CatalogService()
        {
            Products = new List<Product>();
        }

        // Заполнение тестовыми данными (из конструктора окна)
        public void LoadSampleData()
        {
            Products.Add(new Product { Name = "Кроссовки Run", Category = "Спорт", Manufacturer = "Nike", Material = "текстиль", Price = 8000, Quantity = 12, HasDiscount = false, ImageFileName = "p1.png" });
            Products.Add(new Product { Name = "Кроссовки Air", Category = "Спорт", Manufacturer = "Nike", Material = "кожа", Price = 10000, Quantity = 3, HasDiscount = true, ImageFileName = "p2.png" });
            Products.Add(new Product { Name = "Туфли Office", Category = "Классика", Manufacturer = "Ralf", Material = "кожа", Price = 5000, Quantity = 6, HasDiscount = false, ImageFileName = "p3.png" });
            Products.Add(new Product { Name = "Сапоги Winter", Category = "Сезон", Manufacturer = "Ralf", Material = "нубук", Price = 7500, Quantity = 2, HasDiscount = true, ImageFileName = "p4.png" });
            Products.Add(new Product { Name = "Тапки Home", Category = "Дом", Manufacturer = "NoName", Material = "текстиль", Price = 800, Quantity = 20, HasDiscount = false, ImageFileName = "p5.png" });
            Products.Add(new Product { Name = "Босоножки Summer", Category = "Сезон", Manufacturer = "Nike", Material = "кожа", Price = 3200, Quantity = 5, HasDiscount = true, ImageFileName = "p6.png" });
        }

        // Поиск (возвращает список подходящих)
        public List<Product> Find(string searchText, string manufacturer)
        {
            List<Product> result = new List<Product>();

            foreach (Product product in Products)
            {
                if (!Matches(product, searchText, manufacturer)) continue;

                LoadImagePath(product);
                ApplyDiscount(product);
                UpdateQuantityText(product);

                result.Add(product);
            }

            return result;
        }

        // Подсчёт скидок
        public int CountDiscounts(string searchText, string manufacturer)
        {
            int count = Zero;

            foreach (Product product in Products)
            {
                if (!Matches(product, searchText, manufacturer)) continue;
                if (product.HasDiscount) count++;
            }

            return count;
        }

        // Проверка фильтра
        private bool Matches(Product product, string searchText, string manufacturer)
        {
            bool nameMatches = string.IsNullOrEmpty(searchText) ||
                               (product.Name != null && product.Name.ToLower().Contains(searchText.ToLower()));

            bool manufacturerMatches = (manufacturer == AllManufacturers) ||
                                       (product.Manufacturer == manufacturer);

            return nameMatches && manufacturerMatches;
        }

        // Подбор картинки
        private void LoadImagePath(Product product)
        {
            string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            string file = product.ImageFileName;

            if (string.IsNullOrEmpty(file))
                file = DefaultImage;

            string fullPath = Path.Combine(dir, file);

            if (File.Exists(fullPath))
                product.ImagePath = fullPath;
            else
                product.ImagePath = Path.Combine(dir, DefaultImage);
        }

        // Скидка
        private void ApplyDiscount(Product product)
        {
            if (product.HasDiscount && product.Price > Zero)
                product.PriceWithDiscount = product.Price - product.Price * DiscountRate;
            else
                product.PriceWithDiscount = product.Price;

            product.PriceText = product.PriceWithDiscount.ToString("F2") + " руб.";
        }

        // Текст остатка
        private void UpdateQuantityText(Product product)
        {
            product.IsLowStock = false;

            if (product.Quantity > ManyThreshold)
            {
                product.QuantityText = ManyText;
            }
            else
            {
                product.QuantityText = FewText;
                if (product.Quantity <= LowStockThreshold && product.Quantity > Zero)
                    product.IsLowStock = true;
            }
        }
    }
}