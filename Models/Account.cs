using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public int accountNumber { get; set; }
        [Required]
        public string currencyCode { get; set; }
        [Required]
        public int balance { get; set; }
    }
}
