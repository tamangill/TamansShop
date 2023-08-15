using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TamansShop.Areas.Identity.Data;

// Add profile data for application users by adding properties to the TamansShopUser class
public class TamansShopUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    // Add more custom properties here as needed
        
    // You can also add navigation properties for relationships, if required
    // public ICollection<Order> Orders { get; set; }
}