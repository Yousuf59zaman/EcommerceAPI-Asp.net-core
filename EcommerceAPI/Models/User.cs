using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class User
        {
            [Key]
            public int UserId { get; set; }

            [Required]
            [StringLength(100)]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            
        }
    }

