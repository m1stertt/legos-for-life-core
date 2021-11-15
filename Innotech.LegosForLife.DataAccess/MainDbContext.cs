using InnoTech.LegosForLife.Core.Models;
using InnoTech.LegosForLife.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace InnoTech.LegosForLife.DataAccess
{
    public class MainDbContext: DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options): base(options)
        {
            
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
        
        public virtual DbSet<UserEntity> Users { get; set; }
    }
}