using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Models
{
    public class Relatorio
    {
        [DisplayName("Quantidade")]
        public int Qtde { get; set; }
      
        public string Nome { get; set; }        
    }
}
