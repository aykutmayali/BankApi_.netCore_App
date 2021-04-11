using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class AccountingVM
    {
        public int Id { get; set; }
        [Required]
        public int senderAccountNumber { get; set; }
        [Required]
        public int receiverAccountNumber { get; set; }
        [Required]
        public int amount { get; set; }
    }
}
