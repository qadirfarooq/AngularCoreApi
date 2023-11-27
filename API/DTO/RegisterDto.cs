using System.ComponentModel.DataAnnotations;
 
namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        public string? username { get; set; }
         [Required]
         [StringLength (8,MinimumLength =4)]
        public string? password { get; set; } 

         [Required]
        public string? knownAs  { get; set; } 

         [Required]
        public string? gender { get; set; } 

         [Required]
        public string? city { get; set; } 
         [Required]
        public string? Country { get; set; } 

        [Required]
        public string? Interests { get; set; } 

         

        [Required]
        public string? Introduction { get; set; } 

        [Required]
        public string? LookingFor { get; set; } 

         [Required]
        public DateOnly? dateOfBirth { get; set; } 
    }
}