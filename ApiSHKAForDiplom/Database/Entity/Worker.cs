using System.ComponentModel.DataAnnotations;

namespace ApiSHKAForDiplom.Database.Entity
{
    public class Worker
    {
        [Key, Required]
        public int WorkerId { get; set; }

        [MaxLength(30)]
        public string WorkerFam { get; set; }

        [MaxLength(30)]
        public string WorkerName { get; set; }

        [MaxLength(40)]
        public string WorkerOtch { get; set; }

        [ MaxLength(45)]
        public string WorkerLogin { get; set; }

        [ MaxLength(45)]
        public string WorkerPassword { get; set; }

        [ MaxLength(100)]
        public byte[] WorkerPicture { get; set; }

        [ MaxLength(45)]
        public string WorkerRole { get; set; }
    }
}
