using System.Collections.Generic;
using System.IO;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.Domain.IRepositories;

namespace InnoTech.LegosForLife.Domain.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new InvalidDataException("ProductRepository Cannot Be Null");
        }
        public List<Product> GetProducts()
        {
            return _productRepository.FindAll();
        }
        
        public Product GetProductById(int id)
        {
            return _productRepository.FindById(id);
        }

        public Product DeleteById(int id)
        {
           return  _productRepository.DeleteById(id);
        }

        public Product Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public Product Create(Product product)
        {
            return _productRepository.Create(product);
        }
    }
}