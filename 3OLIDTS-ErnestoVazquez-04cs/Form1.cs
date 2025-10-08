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
using System.Text.RegularExpressions; //libreria para la validacion de formatos de textos
using MySql.Data.MySqlClient; //Libreria de conexion a MySQL - base de datos

namespace _3OLIDTS_ErnestoVazquez_04cs
{
    public partial class Form1 : Form
    {
        //datos de conexion a Mysql (XAMPP)
        string conexionSQL = "Server=localhost;Port=3306;Database=programacionavanzada;Uid=root;Pwd=;"; //Pwd = pasword
        //metodo para insertar registros
        public Form1()
        {
            InitializeComponent();

            //Agregar controladores de eventos TextChanged a los campos
            tbEdad.TextChanged += ValidarEdad;
            tbEstatura.TextChanged += ValidarEstatura;
            tbTelefono.Leave += ValidarTelefono;
            tbNombre.TextChanged += ValidarNombre;
            tbApellidos.TextChanged += ValidarApellidos;
        }
        private void InsertarRegistro(string nombre, string apellidos, int edad, decimal estatura, string telefono, string genero)
        {
            using(MySqlConnection conection = new MySqlConnection(conexionSQL))
            {
                conection.Open();

                string insertQuery = "INSERT INTO registros (Nombre, Apellidos, Edad, Estatura, Telefono, Genero)" +
                    "VALUES (@Nombre, @Apellidos, @ Edad, @Estatura, @Telefono, @Genero)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, conection))
                {
                    command.Parameters.AddWithValue("@Nombre", nombre);
                    command.Parameters.AddWithValue("@Apellidos", apellidos);
                    command.Parameters.AddWithValue("@Edad", edad);
                    command.Parameters.AddWithValue("@Estatura", estatura);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Genero", genero);

                    command.ExecuteNonQuery();
                }
                conection.Close();
            }
        }
        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsTextoValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsEnteroValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para la edad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsEnteroValido10Digitos(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el numero telefonico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsDecimalValido(textbox.Text))
            {
                MessageBox.Show("Ingrese valores correctos para la estatura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool EsEnteroValido(string valor)   
        {
            int resultado;
            return int.TryParse(valor, out resultado);
            //return false;
        }
        private bool EsDecimalValido(string valor)
        {
            decimal resultado;
            return decimal.TryParse(valor, out resultado);
            //return false;
        }

        private bool EsEnteroValido10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado)&& valor.Length==10;
            //return false;
        }
        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[A-Za-z\s]+$");
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
            string nombre = tbNombre.Text;
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
            string datos = $"Nombre: {nombre}\rApellidos: {apellidos}\r" +
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
                    //Si el archivo existe, añade un separador antes del nuevo
                    writer.WriteLine();
                    //Programacion de funcionalidad de insertar SQL
                    InsertarRegistro(nombre, apellidos, int.Parse(edad), decimal.Parse(estatura), telefono, genero);
                    MessageBox.Show("Datos ingresados correctamente");
                }
                else
                {
                    writer.WriteLine(datos);
                }
            }
            MessageBox.Show(datos, "Valores ingresados", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
