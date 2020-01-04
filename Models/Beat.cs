    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    namespace Commerce1.Models
    
    {
        public class Beat
        {   
            [Key]
            public int BeatId { get; set; }
            [Required]
            [MinLength(2)]
            public string Name { get; set; }
             [Required]
            [MinLength(2)]
            public string Genre { get; set; }
            [Required]
            public int BPM { get; set; }
            [Required]
            public int UserId { get; set; }
            public User Creator { get; set; }
            [Required]
            [Range(0.00, 1000000.00)]
            public double Price {get; set;}
            List<Transaction> Buyers {get; set;}
        
           
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            
            
        }
    }