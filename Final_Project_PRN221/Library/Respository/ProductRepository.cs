using Library.DataAccess;
using Library.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public class ProductRepository:IProductRepository
    {
        public List<dynamic> getProductList()
            => ProductManagement.Instance.getProductList();
        public List<dynamic> getProductByFilter
            (string textSearch, string category, string orderBy)
            => ProductManagement.Instance.getProductByFilter(textSearch, category, orderBy);
        public bool addProduct(Product product)
            => ProductManagement.Instance.addProduct(product);
        public bool deleteProduct(int id)
            => ProductManagement.Instance.deleteProduct(id);
        public Product getProductById(int id)
            => ProductManagement.Instance.getProductById(id);
        public bool EditProduct(Product product)
            => ProductManagement.Instance.EditProduct(product);
        public List<dynamic> getProductListInOrder(int orderId)
            => ProductManagement.Instance.getProductListInOrder(orderId);
        public List<dynamic> getProductListItemByFilter
            (string textSearch, string category, string orderBy, int orderId)
            => ProductManagement.Instance.getProductListItemByFilter(textSearch, category, orderBy, orderId);     
    }
}
