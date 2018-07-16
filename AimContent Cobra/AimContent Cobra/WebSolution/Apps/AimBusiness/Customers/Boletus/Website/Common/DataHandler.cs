using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace AimBusiness.Customers.Boletus.Website.Common
{
    public class DataHandler
    {
        private List<Containers.ProductCategoryGroup> _categoryGroups = new List<Containers.ProductCategoryGroup>();
        private List<Containers.Product> _products = new List<Containers.Product>();

        private string _filepath;

        public DataHandler(string customerpath)
        {
            _filepath = HttpContext.Current.Server.MapPath(customerpath + "/articles/");

            XmlHandler xmlHandler = new XmlHandler();

            xmlHandler.FilePath = _filepath + ObjectName.categories.ToString() + ".xml";
            _categoryGroups = (List<Containers.ProductCategoryGroup>)xmlHandler.ReadDataItems(_categoryGroups.GetType());

            try
            {
                xmlHandler.FilePath = _filepath + ObjectName.products + ".xml";
                _products = (List<Containers.Product>)xmlHandler.ReadDataItems(_products.GetType());
            }
            catch
            {
                _products = new List<Containers.Product>();
            }
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Containers.Product> GetProducts()
        {
            return (from o in _products
                    where !o.IsDeleted
                    orderby o.Name
                    select o);
        }

        /// <summary>
        /// Returns products in this group
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<Containers.Product> GetProducts(int category)
        {
            return (from o in _products
                    where o.ProductCategories.Any(p => p == category) &&
                    !o.IsDeleted
                    orderby o.Name
                    select o);
        }

        public Containers.Product GetProduct(int id)
        {
            return _products.Single(p => p.ID == id);
        }

        /// <summary>
        /// Adds a product
        /// </summary>
        /// <param name="id">if null then creating new</param>
        /// <param name="product"></param>
        public void AddProduct(int? id, Containers.Product product)
        {
            product.LastUpdated = DateTime.Now;

            if (!id.HasValue)
            {
                if (_products.Any())
                    product.ID = _products.Max(p => p.ID) + 1;
                else
                    product.ID = 1;

                product.Created = product.LastUpdated;
                product.IsDeleted = false;
                _products.Add(product);
            }
            else
            {
                var oldProduct = _products.Single(p => p.ID == id.Value);

                if (product.ImagePath != String.Empty)
                    oldProduct.ImagePath = product.ImagePath;

                oldProduct.IsDeleted = product.IsDeleted;
                oldProduct.Name = product.Name;
                oldProduct.ProductCategories = new List<int>();

                foreach (var cat in product.ProductCategories)
                    oldProduct.ProductCategories.Add(cat);

                oldProduct.ProductItems = new List<Containers.ProductItem>();

                foreach (var item in product.ProductItems)
                    oldProduct.ProductItems.Add(item);
            }

            save();
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            var product = _products.Single(p => p.ID == id);
            product.LastUpdated = DateTime.Now;
            product.IsDeleted = true;

            save();
        }

        public List<Containers.ProductCategory> GetCategories(Containers.ProductCategoryGroup.GroupName groupName)
        {
            return _categoryGroups.Single(p => p.ShortName == groupName).Categories;
        }

        private void save()
        {
            //A backup is made on the date
            if (File.Exists(_filepath + ObjectName.products.ToString() + ".xml"))
                File.Copy(_filepath + ObjectName.products.ToString() + ".xml", _filepath + ObjectName.products.ToString() + "_bak" + DateTime.Now.Day + ".xml", true);

            XmlHandler xmlHandler = new XmlHandler() { FilePath = _filepath + ObjectName.products.ToString() + ".xml" };
            xmlHandler.WriteDataItems(_products);
        }

        public enum ObjectName { categories, products };
    }
}