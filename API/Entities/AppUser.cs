using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Entities
{
    public class AppUser
    {
         [Key]
        public int Id { get; set; } 
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public byte [] PasswordSalt { get; set; }
        public byte [] PasswordHash { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateOfBirth { get; set; }
         public DateTime Created { get; set; } = DateTime.UtcNow;
          public string? KnownAs { get; set; }
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string? Introduction { get; set; }
     
        public string? LookingFor { get; set; }
         public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
         public List<Photo> Photos  { get; set; } = new ();

        // public int  GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
        
    }
}