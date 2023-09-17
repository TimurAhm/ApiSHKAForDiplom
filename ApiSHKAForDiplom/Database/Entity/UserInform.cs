using System.ComponentModel.DataAnnotations;

namespace ApiSHKAForDiplom.Database.Entity
{
    public class UserInform
    {
        [Key, Required]
        public int UserInformUserId { get; set; }
        [Required]
        public int UserInformPassportSeria { get; set; }

        [Required]
        public int UserInformNomer { get; set; }
        
        [Required, MaxLength(200)]
        public string UserInformWorkPlace { get; set; }

        [Required, MaxLength(200)]
        public string UserInformFamilyType { get; set; }

        [Required, MaxLength(400)]
        public string UserInformRealEstate { get; set; }
        
        public decimal UserInformIncome { get; set; }
    }
}
