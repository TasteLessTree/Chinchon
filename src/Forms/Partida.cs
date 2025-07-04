using Chinchon.src.Utils;

namespace Chinchon.src.forms {
    public partial class Partida : Form {
        // Campos para poder usar las cartas
        private Cartas mazo;
        private List<string> manoJugador;

        public Partida() {
            InitializeComponent();
        }

        // Mostrar la mano del jugador
        private void MostarManoJugador() {
            // Limpiar controles
            flpManoJugador.Controls.Clear();

            foreach (var carta in manoJugador) {
                // Crear la imagen
                PictureBox pictureBox = new PictureBox {
                    Width = 150,
                    Height = 276,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(5)
                };

                // Abrir las imágenes
                string rutaImagen = Path.Combine(Application.StartupPath, "assets", "images", carta + ".jpg");
                pictureBox.Image = Image.FromFile(rutaImagen);

                // Click para seleccionar la carta
                pictureBox.Click += (s, e) => {
                    // Borde rojo al seleccionarla
                    foreach (PictureBox child in flpManoJugador.Controls.OfType<PictureBox>())
                        child.BorderStyle = BorderStyle.None;

                    pictureBox.BorderStyle = BorderStyle.FixedSingle;

                    // ! Guardar la carta seleccionada
                };

                // Añadir la imagen al FlowLayoutPanel
                flpManoJugador.Controls.Add(pictureBox);
            }
        }

        private void Partida_Load(object sender, EventArgs e) {
            // Posicionar en el centro de la pantalla
            this.CenterToScreen();

            // Tamaño
            this.Size = new Size(1270, 680);

            // Creamos y barajeamos el mazo
            mazo = new Cartas();
            mazo.Barajear();

            // DEBUG: Repartimos 7 cartas a 2 jugadores
            // Asignamos una mano al jugador
            var manos = mazo.RepartirCartas(7, 2);
            manoJugador = manos[0];

            // Mostramos la mano del jugador
            MostarManoJugador();
        }
    }
}