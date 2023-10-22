using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmpleadoRepository
    {
        private readonly string FileName = "Empleado.txt";

        public void Guardar(Empleado empleado)
        {
            FileStream file = new FileStream(FileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{empleado.Id};{empleado.Nombre};{empleado.SalarioBase};{empleado.Estado}");
            writer.Close();
            file.Close();

        }

        public List<Empleado> ConsultarTodos()
        {
            List<Empleado> empleados = new List<Empleado>();
            FileStream file = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                Empleado empleado = Map(linea);
                empleados.Add(empleado);
            }
            reader.Close();
            file.Close();
            return empleados;
        }
        private Empleado Map(string linea)
        {
            Empleado empleado = new Empleado();
            char delimiter = ';';
            string[] matrizEmpleado = linea.Split(delimiter);
            empleado.Id = int.Parse(matrizEmpleado[0]);
            empleado.Nombre = matrizEmpleado[1];
            empleado.SalarioBase = Double.Parse(matrizEmpleado[2]);
            empleado.Estado = matrizEmpleado[3];



            return empleado;
        }
        private bool EsEncontrado(int identificacioRegistrada, int identificacionBuscada)
        {
            return identificacioRegistrada == identificacionBuscada;
        }

        public Empleado Buscar(int identificacion)
        {
            List<Empleado> empleados = ConsultarTodos();
            foreach (var item in empleados)
            {
                if (EsEncontrado(item.Id, identificacion))
                {
                    return item;
                }
            }
            return null;
        }

    }
}
