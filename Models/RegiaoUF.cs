using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class RegiaoUF
    {
        [Key]
        public int Id { get; set; }        
        public string Nome { get; set; }
        public int IdUF { get; set; }

    }
}
