using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }    
        public Double SalarioBase { get; set;}
        public String Estado { get; set; }

        public Empleado(int id, string nombre, double salarioBase, string estado)
        {
            Id = id;
            Nombre = nombre;
            SalarioBase = salarioBase;
            Estado = estado;
        }

        public Empleado()
        {
        }
    }
}
