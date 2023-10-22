using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmpleadoService
    {
        private readonly EmpleadoRepository EmpleadoRepository;
        private List<Empleado> empleados;

        public EmpleadoService()
        {
            EmpleadoRepository = new EmpleadoRepository();
        }
        void RefrescarLista()
        {
            empleados = EmpleadoRepository.ConsultarTodos();
        }
        public string Guardar(Empleado empleado)
        {
            try
            {

                if (EmpleadoRepository.Buscar(empleado.Id) == null)
                {
                    EmpleadoRepository.Guardar(empleado);
                    return $"Se han guardado correctamente los datos del empleado: {empleado.Nombre} ";
                }
                else
                {
                    return $"Lo sentimos,ya hay un empleado con la Identificación {empleado.Id}";
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }
        }


        public ConsultaEmpleadoResponse ConsultarTodos()
        {

            try
            {
                List<Empleado> empleados = EmpleadoRepository.ConsultarTodos();
                if (empleados != null)
                {
                    return new ConsultaEmpleadoResponse(empleados);
                }
                else
                {
                    return new ConsultaEmpleadoResponse("La Persona buscada no se encuentra Registrada");
                }

            }
            catch (Exception e)
            {

                return new ConsultaEmpleadoResponse("Error de Aplicacion: " + e.Message);
            }
        }

        public List<Empleado> BuscarNombreYEstado(string nombre, string estado)
        {
            List<Empleado> listaFiltrada = new List<Empleado>();
            RefrescarLista();
            foreach (var item in empleados)
            {
                if (String.Equals(item.Nombre, nombre, StringComparison.OrdinalIgnoreCase) 
                || String.Equals(item.Estado, estado, StringComparison.OrdinalIgnoreCase))
                {
                    listaFiltrada.Add(item);
                }
            }
            return listaFiltrada;
        }


        public class ConsultaEmpleadoResponse
        {
            public List<Empleado> Empleados { get; set; }
            public string Message { get; set; }
            public bool Encontrado { get; set; }

            public ConsultaEmpleadoResponse(List<Empleado> parciales)
            {
                Empleados = new List<Empleado>();
                Empleados = parciales;
                Encontrado = true;
            }
            public ConsultaEmpleadoResponse(string message)
            {
                Message = message;
                Encontrado = false;
            }
        }
    }
}
