using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon;
using static System.Net.Mime.MediaTypeNames;
using Lab_7_Ejercicio.lab7DataSetTableAdapters;

namespace Lab_7_Ejercicio
{
    public partial class frmEstudiante : Form
    {
        bool celebridad = false;
        public frmEstudiante()
        {
            InitializeComponent();
        }

        private void frmEstudiante_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ofdArchivo.ShowDialog();
            if (string.IsNullOrWhiteSpace(ofdArchivo.FileName) == false)
            {
                String foto = ofdArchivo.FileName;
                Amazon.Rekognition.Model.Image imagen = new Amazon.Rekognition.Model.Image();
                try
                {
                    using (FileStream fs = new FileStream(foto, FileMode.Open, FileAccess.Read))
                    {
                        byte[] data = null;
                        data = new byte[fs.Length];
                        fs.Read(data, 0, (int)fs.Length);
                        imagen.Bytes = new MemoryStream(data);
                    }
                }
                catch (Exception)
                {
                MessageBox.Show("Error al cargar la imagen: " + foto);
                }
                try
                {
                    AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient("AKIAIE5LZMZN4CR6IO5Q", "xUtzMH5IxZmuZYrc9KSN83JE+pgf5J60+FajM65J", RegionEndpoint.USEast1);

                    DetectTextRequest detectTextRequest = new DetectTextRequest()
                    {
                        Image = imagen
                    };

                    DetectTextResponse detectTextResponse = rekognitionClient.DetectText(detectTextRequest);

                    bool textoDetectado = (detectTextResponse.TextDetections.Count() > 0);

                    DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
                    {
                        Image = imagen
                    };

                    DetectFacesResponse detectFacesResponse = rekognitionClient.DetectFaces(detectFacesRequest);

                    bool unaSolaCara = (detectFacesResponse.FaceDetails.Count() == 1);

                    DetectModerationLabelsRequest detectLabelsRequest = new DetectModerationLabelsRequest()
                    {
                        Image = imagen
                    };

                    DetectModerationLabelsResponse detectLabelsResponse = rekognitionClient.DetectModerationLabels(detectLabelsRequest);

                    bool imagenApta = true;
                    foreach (var item in detectLabelsResponse.ModerationLabels)
                    {
                        if (item.Confidence < 90)
                        {
                            imagenApta = false;
                            break;
                        }
                    }

                    if (imagenApta && textoDetectado == false && unaSolaCara)
                    {
                        RecognizeCelebritiesRequest celebridadesRequest = new RecognizeCelebritiesRequest() { Image = imagen };

                        RecognizeCelebritiesResponse celebridadesResponse = rekognitionClient.RecognizeCelebrities(celebridadesRequest);

                        celebridad = (celebridadesResponse.CelebrityFaces.Count() > 0);

                        pbImage.ImageLocation = foto;
                    }
                    else
                    {
                        MessageBox.Show("La imagen ingresada no es valida para introducir en el sistema", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La API no está disponible");
                }
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string TipoDocumento = txtTipo.Text;
            string Documento = txtDocumento.Text;
            string Nombre = txtNombre.Text;
            string Apellido = txtApellido.Text;
            DateTime FechaNacimiento = dtpNacimiento.Value;
            string Estado = txtEstado.Text;
            string Foto = pbImage.ImageLocation;
            decimal PagoInscripcion = 10000;

            tblEstudiantesTableAdapter adaptador = new tblEstudiantesTableAdapter();
            int afectado = adaptador.spInsertEstudiante(TipoDocumento, Documento, Nombre, Apellido, FechaNacimiento, celebridad, Estado, PagoInscripcion, Foto);

            if (afectado > 0)
            {
                MessageBox.Show("Estudiante registrado", "Exito");
                frmReportes frmReportes = new frmReportes(TipoDocumento, Documento);
                
                frmReportes.Show();
            }
        }
    }
    
}
