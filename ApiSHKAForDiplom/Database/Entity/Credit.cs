using System;
using System.ComponentModel.DataAnnotations;

namespace ApiSHKAForDiplom.Database.Entity
{
    public class Credit
    {
        [Key, Required]
        public int CreditUserId { get; set; }
        [Required]
        public string CreditType { get; set; }
        [Required]
        public decimal CreditCash { get; set; }
        [Required]
        public decimal CreditCashAfterYears { get; set; }
        [Required]
        public decimal CreditCashOstatok { get; set; }
        [Required, MaxLength(3)]
        public int CreditPercent { get; set; }
        [Required, MaxLength(3)]
        public string CreditValuta { get; set; }
        [Required]
        public DateTime CreditGiveDate { get; set; }
        [Required]
        public DateTime CreditBackDate { get; set; }


    }
}
