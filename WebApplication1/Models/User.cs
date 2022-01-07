using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebApplication1.Models
{
    public partial class User
    {
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }   

        [Required]
        public string Password { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
