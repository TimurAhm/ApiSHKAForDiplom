using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using ApiSHKAForDiplom.Database.Entity;

namespace ApiSHKAForDiplom.Database.Entity
{
    public class User
    {
        [Key, Required]
        public int UserId { get; set; }

        [Required, MaxLength(45)]
        public string UserFam { get; set; }

        [Required, MaxLength(45)]
        public string UserName { get; set; }

        [MaxLength(45)]
        public string UserOtch { get; set; }

        [Required, MaxLength(10)]
        public string UserBalanceRef { get; set; }

        [Required, MaxLength(45)]
        public string UserRole { get; set; }

        [Required, MaxLength(100)]
        public string UserLogin { get; set; }


    //    public List<UsersBank> UsersBanks { get; set; }

        // public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    }
}
