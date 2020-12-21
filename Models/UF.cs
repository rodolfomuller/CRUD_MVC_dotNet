
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class UF
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
       
    }
}
