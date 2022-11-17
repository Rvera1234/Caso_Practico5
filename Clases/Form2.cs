using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net.Http; // todo lo referente a ervicios
using System.IO; // Para trabajar con archivos
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clases
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            string nombreArchivo = txtnombre.Text;
            int tipoRespuesta = 2;
            string mensajeRespuesta = "";
            if (!string.IsNullOrWhiteSpace(nombreArchivo))
            {

                try
                {
                    using (HttpClient cliente = new HttpClient())
                    {
                        string url = "https://localhost:44316/api/Archivo/?nombreArchivo=" + nombreArchivo;
                        using (HttpResponseMessage respuestaConsulta = await cliente.GetAsync(url)) //consumo del servicio
                        {
                            if (respuestaConsulta.IsSuccessStatusCode)
                            {
                                byte[] arrContenido = await respuestaConsulta.Content.ReadAsAsync<byte[]>();
                                string nombreCompletoArchivo = @"C:\Users\iVera\Desktop\APIPRUEBA\" + nombreArchivo;
                                File.WriteAllBytes(nombreCompletoArchivo, arrContenido);
                                tipoRespuesta = 1;
                                mensajeRespuesta = "Se descargó correctamente el archivo " + nombreArchivo;
                            }
                            else
                            {
                                mensajeRespuesta = await respuestaConsulta.Content.ReadAsStringAsync();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    tipoRespuesta = 3;
                    mensajeRespuesta = ex.Message;
                }

            }

            MessageBoxIcon iconoMensaje;
            if (tipoRespuesta == 1)
                iconoMensaje = MessageBoxIcon.Information;
            else if (tipoRespuesta == 2)
                iconoMensaje = MessageBoxIcon.Warning;
            else
                iconoMensaje = MessageBoxIcon.Error;
            MessageBox.Show(mensajeRespuesta, "Descarga de archivos", MessageBoxButtons.OK, iconoMensaje);
        
    }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
