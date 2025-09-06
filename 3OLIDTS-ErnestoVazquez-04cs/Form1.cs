using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //Libreria para lectura y escritura de archivos


namespace _3OLIDTS_ErnestoVazquez_04cs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            tbApellidos.Clear();
            tbNombre.Clear();
            tbTelefono.Clear();
            tbEstatura.Clear();
            tbEdad.Clear();
            rbMasculino.Checked = false;
            rbFemenino.Checked = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombres = tbNombre.Text;
            string apellidos = tbApellidos.Text;
            string telefono = tbTelefono.Text;
            string estatura = tbEstatura.Text;
            string edad = tbEdad.Text;
            string genero = "";
            if (rbFemenino.Checked)
            {
                genero = "Femenino";
            } 
            else if (rbMasculino.Checked)
            {
                genero = "Masculino";
            }
            string datos = $"Nombre: {nombres}\rApellidos: {apellidos}\r" +
                $"Telefono: {telefono}\rEstatura: {estatura}\r" +
                $"Edad: {edad}\rGenero: {genero}";

            //string ruta = "D:/Desktop/Prog Avanzada 3°O/3OLIDTS-250902.txt";
            //string ruta = "D:\\Desktop\\Prog Avanzada 3°O\\3OLIDTS-250902.txt";
            string ruta = @"D:\Desktop\Prog Avanzada 3°O\3OLIDTS-250902.txt";
            bool archivoExiste = File.Exists(ruta);
            using(StreamWriter writer = new StreamWriter(ruta, true))
            {
                if (archivoExiste)
                {
                    writer.WriteLine();
                }
                writer.WriteLine(datos);
            }
            MessageBox.Show(datos, "Valores ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
