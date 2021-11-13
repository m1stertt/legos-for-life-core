using System.Collections.Generic;
using InnoTech.LegosForLife.Core.Models;

namespace InnoTech.LegosForLife.Core.IServices
{
    public interface IProductService
    {
        List<Product> GetProducts();

        Product GetProductById(int id);
        
        Product DeleteById(int id);

        Product Update(Product product);

        Product Create(Product product);


    }
}