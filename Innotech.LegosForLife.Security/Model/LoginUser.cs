using System.Collections.Generic;

namespace InnoTech.LegosForLife.Security.Model
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        
        public List<Permission> Permissions { get; set; }

        public int DbUserId { get; set; }
    }
}