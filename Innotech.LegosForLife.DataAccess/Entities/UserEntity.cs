using System.Collections.Generic;

namespace InnoTech.LegosForLife.DataAccess.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductEntity> Products { get; set; }
    }
}