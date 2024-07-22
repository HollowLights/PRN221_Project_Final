using Library.DataAccess;

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
        public Product getProductByName(string name);
    }
}
