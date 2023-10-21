using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ParcialRepository
    {
        private readonly string FileName = "Parcial.txt";

        public void Guardar(Empleado parcial)
        {
            FileStream file = new FileStream(FileName, FileMode.Append);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine($"{}");
            writer.Close();
            file.Close();

        }

        public List<Empleado> ConsultarTodos()
        {
            List<Empleado> parciales = new List<Empleado>();
            FileStream file = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string linea = string.Empty;
            while ((linea = reader.ReadLine()) != null)
            {

                Empleado usuario = Map(linea);
                parciales.Add(usuario);
            }
            reader.Close();
            file.Close();
            return parciales;
        }
        private Empleado Map(string linea)
        {
            Empleado parcial = new Empleado();
            char delimiter = ';';
            string[] matrizParcial = linea.Split(delimiter);
            parcial.x = matrizParcial[0];


            return parcial;
        }
        private bool EsEncontrado(int identificacioRegistrada, int identificacionBuscada)
        {
            return identificacioRegistrada == identificacionBuscada;
        }

        public Empleado Buscar(int identificacion)
        {
            List<Empleado> parciales = ConsultarTodos();
            foreach (var item in parciales)
            {
                if (EsEncontrado(item.Id, identificacion))
                {
                    return item;
                }
            }
            return null;
        }

        public void Eliminar(string identificacion)
        {
            List<Empleado> parciales = new List<Empleado>();
            parciales = ConsultarTodos();
            FileStream file = new FileStream(FileName, FileMode.Create);
            file.Close();
            foreach (var item in parciales)
            {
                if (!EsEncontrado(item.x, identificacion))
                {
                    Guardar(item);
                }

            }

        }
        public void Modificar(Empleado personaOld, Empleado personaNew)
        {
            List<Empleado> parciales = new List<Empleado>();
            parciales = ConsultarTodos();
            FileStream file = new FileStream(FileName, FileMode.Create);
            file.Close();
            foreach (var item in parciales)
            {
                if (!EsEncontrado(item.x, personaOld.x))
                {
                    Guardar(item);
                }
                else
                {
                    Guardar(personaNew);
                }

            }

        }
    }
}
