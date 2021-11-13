using System.Collections.Generic;
using InnoTech.LegosForLife.Core.Models;

namespace InnoTech.LegosForLife.Domain.IRepositories
{
    public interface IProductRepository
    {
        List<Product> FindAll();
        Product FindById(int id);
        Product DeleteById(int id);

        Product Update(Product product);

        Product Create(Product product);
    }
}