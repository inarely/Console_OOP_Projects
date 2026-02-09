using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace _2__Product_Catalog_App
{
    public class Product
    {
        private static int _nextId = 1;
        private int _id;

        private string _name;
        private decimal _price;
        private int _storageData;
        private int _amount;

        [JsonInclude]
        public string Name
        {
            get { return _name; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Cannot be empty");

                _name = value.Trim();
            }
        }
        public int Id { get { return _id; } set { _id = value; } }
        public int Amount { get { return _amount; } set { _amount = value; } }
        public decimal Price { get { return _price; } set { _price = value; } }
        public int storageData { get { return _storageData; } set { _storageData = value; } }

        public Product() { }
        public Product(string name, decimal price, int amount, int sData)
        {
            this.Name = name;
            this.Amount = amount;

            if (price < 0) { price = 0; }
            this.Price = price;

            if (sData <= 0) throw new ArgumentException("Data must be > 0");
            this.storageData = sData;

            this.Id = _nextId;
            _nextId++;
        }
    }

    public class Storage
    {
        [JsonInclude]
        private Dictionary<int, Product> _product = new Dictionary<int, Product>();
        public IReadOnlyDictionary<int, Product> Product => _product;
        public void AddProduct(Product product)
        {
            if (product == null) throw new ArgumentNullException("Product is null!");

            _product.Add(product.Id, product);
        }

        public void RemoveProductById(int productId)
        {
            if (productId < 0)
                throw new ArgumentException("Number must be > 0");

            if (!_product.ContainsKey(productId))
                throw new ArgumentException("Id isn't found!");

            _product.Remove(productId);
        }

        public void IncreaseAmountById(int id, int amount)
        {
            if (!_product.ContainsKey(id)) throw new ArgumentException("Product isn't found!");
            if (amount <= 0) throw new ArgumentException("Amount must be > 0");

            _product[id].Amount += amount;
        }

        public void DecreaseAmountById(int id, int amount)
        {
            if (!_product.ContainsKey(id)) throw new ArgumentException("Product isn't found!");
            if (amount <= 0) throw new ArgumentException("Amount must be > 0");

            _product[id].Amount -= amount;
        }
    }
    internal class ProductsClasses { }
}
