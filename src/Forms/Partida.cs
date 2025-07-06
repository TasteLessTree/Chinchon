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
                    Height = 230,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Margin = new Padding(5)
                };

                // Habilitar Drag and Drop
                pictureBox.MouseDown += PB_MouseDown;

                // Abrir las imágenes
                string rutaImagen = Path.Combine(Application.StartupPath, "assets", "images", carta + ".jpg");

                // Ignorar la imágen de la carta dada la vuelta
                if (carta == "REVERSO") continue;

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

        // Mouse Down
        private void PB_MouseDown(object? sender, MouseEventArgs e) {
            PictureBox? pb = sender as PictureBox;
            if (pb != null)
                pb.DoDragDrop(pb, DragDropEffects.Move);
        }

        // Entrar en la acción
        private void FlpManoJugador_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        // La acción termina
        private void FlpManoJugador_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        // Comprueba al soltar y reposiciona definitivamente
        private void FlpManoJugador_DragDrop(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox))) {
                PictureBox? pb = e.Data.GetData(typeof(PictureBox)) as PictureBox;
                Point point = flpManoJugador.PointToClient(new Point(e.X, e.Y));
                Control? target = flpManoJugador.GetChildAtPoint(point);

                int index = target == null ? flpManoJugador.Controls.Count - 1 : flpManoJugador.Controls.GetChildIndex(target, false);

                if (pb != null) {
                    flpManoJugador.Controls.SetChildIndex(pb, index);
                    flpManoJugador.Invalidate();
                }
            }
        }

        // Mostrar pila de descarte, donde se dejan las cartas y luego se cierra
        private void MostrarPilaDescarte(string carta) {
            flpPilaDescarte.Controls.Clear();

            // Ruta a la imagen
            string rutaImagen = Path.Combine(Application.StartupPath, "assets", "images", carta + ".jpg");

            // Crear la imagen
            PictureBox pictureBox = new PictureBox {
                Width = 150,
                Height = 230,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(rutaImagen)
            };

            // Añadir la imagen
            flpPilaDescarte.Controls.Add(pictureBox);
        }

        private void Partida_Load(object sender, EventArgs e) {
            // Posicionar en el centro de la pantalla
            this.CenterToScreen();

            // Tamaño
            this.Size = new Size(1230, 700);

            // Creamos y barajeamos el mazo
            mazo = new Cartas();
            mazo.Barajear();

            // DEBUG: Repartimos 7 cartas a 2 jugadores
            // Asignamos una mano al jugador
            var manos = mazo.RepartirCartas(7, 2);
            manoJugador = manos[0];

            // Mostramos la mano del jugador
            MostarManoJugador();

            // Creamos la pila de descarte y la mostramos
            string cartaPinta = mazo.CrearPilaDescarte();
            MostrarPilaDescarte(cartaPinta);
        }
    }
}