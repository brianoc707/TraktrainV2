    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    namespace Commerce1.Models
    
    {
        public class User
        {
            [Key]
            public int UserId { get; set; }
            [Required]
            [MinLength(2)]
            public string Name { get; set; }
            [EmailAddress]
            [Required]
            public string Email { get; set; }

            List<Beat> BeatList {get; set;}
            List<Transaction> Purchases {get; set;}

           
            
            [Required]
            [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            [DataType(DataType.Password)]
            [RegularExpression("(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage="Password must contain at least one number, one letter, and one special character")]
            public string Password { get; set; }
           
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            
            [NotMapped]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string ConfirmPW {get;set;}
        }
    }