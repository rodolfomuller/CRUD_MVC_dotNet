using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class Cidade
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Estado")]
        [Required]
        public int Iduf { get; set; }
        
        [DisplayName("IBGE")]
        [Required]
        public int Ibge { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Longitude { get; set; }
        [Required]
        public string Latitude { get; set; }
        [DisplayName("Região")]
        [Required]
        public int Idregiaouf { get; set; }
        [DisplayName("Estado")]
        public string UFNome { get; set; }
        [DisplayName("Região")]
        public string RegiaoNome { get; set; }
        public SelectList UFs { set; get; }
        public SelectList Regioes { set; get; }
    }
}
