namespace Lab13
{
    public class Product
    {
        private string _name;
        private string _category;
        private string _manufacturer;
        private string _material;
        private double _price;
        private double _priceWithDiscount;
        private string _priceText;
        private int _quantity;
        private string _quantityText;
        private bool _hasDiscount;
        private bool _isLowStock;
        private string _imagePath;
        private string _imageFileName;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Manufacturer
        {
            get { return _manufacturer; }
            set { _manufacturer = value; }
        }

        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public double PriceWithDiscount
        {
            get { return _priceWithDiscount; }
            set { _priceWithDiscount = value; }
        }

        public string PriceText
        {
            get { return _priceText; }
            set { _priceText = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public string QuantityText
        {
            get { return _quantityText; }
            set { _quantityText = value; }
        }

        public bool HasDiscount
        {
            get { return _hasDiscount; }
            set { _hasDiscount = value; }
        }

        public bool IsLowStock
        {
            get { return _isLowStock; }
            set { _isLowStock = value; }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }
    }
}