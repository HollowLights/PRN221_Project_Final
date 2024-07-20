using Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Respository
{
    public interface IProductRepository
    {
        public List<dynamic> getProductList();
        public List<dynamic> getProductByFilter
            (string textSearch, string category, string orderBy);
        public bool addProduct(Product product);
        public bool deleteProduct(int id);
        public Product getProductById(int id);
        public bool EditProduct(Product product);
        public List<dynamic> getProductListInOrder(int orderId);
        public List<dynamic> getProductListItemByFilter
            (string textSearch, string category, string orderBy, int orderId);
    }
}
