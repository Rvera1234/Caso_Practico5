using Clases.ApiRest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases
{
    public partial class Form1 : Form
    {
        DBApi dBApi = new DBApi();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrueba_Click(object sender, EventArgs e)
        {
            dynamic respuesta = dBApi.Get("https://reqres.in/api/users?page=1");
            pictureBox1.ImageLocation = respuesta.data[int.Parse(txtnum.Text)].avatar.ToString();
            txtNombreGET.Text = respuesta.data[int.Parse(txtnum.Text)].first_name.ToString();
            txtApellidoGET.Text = respuesta.data[int.Parse(txtnum.Text)].last_name.ToString();
            txtEmail.Text = respuesta.data[int.Parse(txtnum.Text)].email.ToString();
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona
            {
                job = txtTrabajador.Text,
                name = txtNombresPost.Text
            };

            string json = JsonConvert.SerializeObject(persona);

            dynamic respuesta = dBApi.Post("https://reqres.in/api/users",json);

            MessageBox.Show(respuesta.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }
    }

    public class Persona
    {
        public string name { get; set; }
        public string job { get; set; }
    }
}

