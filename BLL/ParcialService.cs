using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ParcialService
    {
        private readonly ParcialRepository parcialRepository;
        public ParcialService()
        {
            parcialRepository = new ParcialRepository();
        }

        public string Guardar(Empleado parcial)
        {
            try
            {

                if (parcialRepository.Buscar(parcial.Id) == null)
                {
                    parcialRepository.Guardar(parcial);
                    return $"Se han guardado correctamente los datos del parcial: {parcial.x} ";
                }
                else
                {
                    return $"Lo sentimos,ya hay un parcial con la Identificación {parcial.Id}";
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }
        }

        public string Eliminar(string identificacion)
        {
            try
            {
                if (parcialRepository.Buscar(identificacion) != null)
                {
                    parcialRepository.Eliminar(identificacion);
                    return ($"se han Eliminado Satisfactoriamente los datos de la persona con Identificación: {identificacion} ");
                }
                else
                {
                    return ($"Lo sentimos, no se encuentra registrada una persona con Identificacion {identificacion}");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }

        }

        public string Modificar(Empleado personaOld, Empleado personaNew)
        {
            try
            {
                if (parcialRepository.Buscar(personaOld.x) != null)
                {
                    parcialRepository.Modificar( personaOld,  personaNew);
                    return ($"se han Eliminado Satisfactoriamente los datos de la persona con Identificación: {identificacion} ");
                }
                else
                {
                    return ($"Lo sentimos, no se encuentra registrada una persona con Identificacion {identificacion}");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }

        }

        public ConsultaParcialResponse ConsultarTodos()
        {

            try
            {
                List<Empleado> parciales = parcialRepository.ConsultarTodos();
                if (parciales != null)
                {
                    return new ConsultaParcialResponse(parciales);
                }
                else
                {
                    return new ConsultaParcialResponse("La Persona buscada no se encuentra Registrada");
                }

            }
            catch (Exception e)
            {

                return new ConsultaParcialResponse("Error de Aplicacion: " + e.Message);
            }
        }

        public class ConsultaParcialResponse
        {
            public List<Empleado> Parciales { get; set; }
            public string Message { get; set; }
            public bool Encontrado { get; set; }

            public ConsultaParcialResponse(List<Empleado> parciales)
            {
                Parciales = new List<Empleado>();
                Parciales = parciales;
                Encontrado = true;
            }
            public ConsultaParcialResponse(string message)
            {
                Message = message;
                Encontrado = false;
            }
        }
    }
}
