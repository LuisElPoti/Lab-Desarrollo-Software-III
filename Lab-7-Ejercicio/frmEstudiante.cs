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

namespace Lab_7_Ejercicio
{
    public partial class frmEstudiante : Form
    {
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
            pbImage.ImageLocation = ofdArchivo.FileName;

            String photo = ofdArchivo.FileName;
            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al cargar la imagen: " + photo);

            }
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient("AKIAIE5LZMZN4CR6IO5Q", "xUtzMH5IxZmuZYrc9KSN83JE+pgf5J60+FajM65J", RegionEndpoint.USEast1);

            DetectTextRequest detectTextRequest = new DetectTextRequest()
            {
                Image = image
            };

            DetectTextResponse detectTextResponse = rekognitionClient.DetectText(detectTextRequest);

            bool textoDetectado = (detectTextResponse.TextDetections.Count() > 0);

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest()
            {
                Image = image
            };

            DetectFacesResponse detectFacesResponse = rekognitionClient.DetectFaces(detectFacesRequest);

            bool rostroDetectado = (detectFacesResponse.FaceDetails.Count() > 0);

            DetectModerationLabelsRequest detectLabelsRequest = new DetectModerationLabelsRequest()
            {
                Image = image
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

            if (imagenApta && textoDetectado == false && rostroDetectado)
            {
                RecognizeCelebritiesRequest celebridadesRequest = new RecognizeCelebritiesRequest() { Image = image };

                RecognizeCelebritiesResponse celebridadesResponse = rekognitionClient.RecognizeCelebrities(celebridadesRequest);

                // Celebridad = (celebridadesResponse.CelebrityFaces.Count() > 0);

                pbImage.ImageLocation = ofdArchivo.FileName;
            }
            else
            {
                MessageBox.Show("La imagen ingresada no es valida para introducir en el sistema", "Error");
            }
        }
    }
}
