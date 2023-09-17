using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiSHKAForDiplom.Database.Entity
{
    public class UsersBank
    {
        //public UsersBank()
        //{
        //    Users = new List<User>();
        //}

        [Key, MaxLength(10)]
        public string UsersBankRef { get; set; }

        public decimal UsersBankBalance { get; set; }

        [Required, MaxLength(5)]
        public string Valuta { get; set; }


      //  public string UserUserBalanceRef { get; set; }
      //  public User User { get; set;}

        //   public virtual ICollection<User> Users { get; set; }


    }
}
