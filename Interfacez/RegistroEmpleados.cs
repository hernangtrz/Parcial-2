using BLL;
using ENTITY;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interfacez
{
    public partial class empleadosForm : Form
    {
        EmpleadoService empleadoService;
        public empleadosForm()
        {
            empleadoService = new EmpleadoService();
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                MessageBox.Show(Guardar());
            }
            CargarGrilla(empleadoService.ConsultarTodos().Empleados);
        }

        public String Guardar()
        {
            Empleado e;
            int id = int.Parse(txtId.Text);
            String nombre = txtNombre.Text;
            Double salarioBase = Double.Parse(txtSalarioBase.Text);
            String estado = cbEstado.Text;
            var empleados = empleadoService.ConsultarTodos();
            Boolean encontro = false;
            foreach (var item in empleados.Empleados)
            {
                if(item.Id == id)
                {
                    encontro = true;    
                }
            }

            if (!encontro)
            {
                e = new Empleado(id, nombre, salarioBase, estado);
                return empleadoService.Guardar(e);
            }

            return "El empleado con id: " + id + " ya se encuentra registrado";
        }

        public bool validarCampos()
        {
            if (String.IsNullOrEmpty((txtId).Text))
            {
                MessageBox.Show("El campo ID se encuentra vacio");
                return false;
            }
            if (String.IsNullOrEmpty((txtNombre).Text))
            {
                MessageBox.Show("El campo NOMBRE se encuentra vacio");
                return false;
            }
            if (String.IsNullOrEmpty((txtSalarioBase).Text))
            {
                MessageBox.Show("El campo SALARIO BASE se encuentra vacio");
                return false;
            }
            if (String.IsNullOrEmpty((cbEstado).Text))
            {
                MessageBox.Show("El campo ESTADO se encuentra vacio");
                return false;
            }
            return true;
        }

        private void cbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSalarioBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        void CargarGrilla(List<Empleado> lista)
        {
            grillaEmpleados.Rows.Clear();

            foreach (var item in lista)
            {
                grillaEmpleados.Rows.Add(item.Id, item.Nombre, item.SalarioBase, item.Estado);
            }

        }



        private void empleadosForm_Load(object sender, EventArgs e)
        {
            CargarGrilla(empleadoService.ConsultarTodos().Empleados);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String nombre = nombreConsulta.Text;
            String estado = estadoConsulta.Text;
            CargarGrilla(empleadoService.BuscarNombreYEstado(nombre,estado));

        }
    }
}
