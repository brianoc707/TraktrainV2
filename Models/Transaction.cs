    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    namespace Commerce1.Models
    
    {
        public class Transaction
        {
            [Key]
            public int TransactionId {get; set;}
            public int BeatId {get;set;}
            public int UserId{get;set;}
            public User Guest {get;set;}
            public Beat Beat {get;set;}
        
           
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;
            
            
        }
    }