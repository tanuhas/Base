using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplication5.Models
{
    public class User:IdentityUser
    {
        public virtual Manager? Manager { get; set; }
        public virtual Owner? Owner { get; set; }
        public virtual Storekeeper? Storekeeper { get; set; }
        public virtual Packer? Packer { get; set; }
        public virtual Accountant? Accountant { get; set; }
        
    }
}
