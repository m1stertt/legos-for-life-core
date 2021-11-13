using System.Collections.Generic;
using System.IO;
using System.Linq;
using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.DataAccess.Entities;
using InnoTech.LegosForLife.Domain.IRepositories;

namespace InnoTech.LegosForLife.DataAccess.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly MainDbContext _ctx;

        public ProductRepository(MainDbContext ctx)
        {
            if (ctx == null) throw new InvalidDataException("Product Repository Must have a DBContext");
            _ctx = ctx;
        }
        public List<Product> FindAll()
        {
            return _ctx.Products
                .Select(pe => new Product
                {
                    Id = pe.Id,
                    Name = pe.Name
                })
                .ToList();
        }

        public Product FindById(int id)
        {
            return _ctx.Products.Select(product => new Product()
            {
                Id = product.Id,
                Name = product.Name
            }).FirstOrDefault(product => product.Id == id);
            
        }

        public Product DeleteById(int id)
        {
            var savedEntity = _ctx.Products.Remove(new ProductEntity() {Id = id}).Entity;
            _ctx.SaveChanges();
            return new Product()
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name,

            };
        }

        public Product Update(Product product)
        {
            var entity = new ProductEntity()
            {
                Id = product.Id,
                Name = product.Name
            };
            var savedEntity = _ctx.Products.Update(entity).Entity;
            _ctx.SaveChanges();
            return new Product()
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }

        public Product Create(Product product)
        {
            var entity = new ProductEntity()
            {
                Name = product.Name
            };
            var savedEntity = _ctx.Add(entity).Entity;
            _ctx.SaveChanges();
            return new Product()
            {
                Id = savedEntity.Id,
                Name = savedEntity.Name
            };
        }
    }
}