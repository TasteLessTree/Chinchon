using Chinchon.src.Utils;

namespace Chinchon.src.forms {
    public partial class Partida : Form {
        // Campos para poder usar las cartas
        private Cartas mazo;
        private List<string> manoJugador;

        public Partida() {
            InitializeComponent();
        }

        // ============================
        // MANO DEL JUGADOR
        // ============================

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
                    Margin = new Padding(5),
                    Tag = carta
                };

                // Habilitar Drag and Drop
                pictureBox.MouseDown += PB_MouseDown; // PB = Picture Box

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

        // Mouse Down (flpManoJugador)
        private void PB_MouseDown(object? sender, MouseEventArgs e) {
            var pb = sender as PictureBox;
            if (pb != null)
                pb.DoDragDrop(pb, DragDropEffects.Move);
        }

        // Entrar en la acción (flpManoJugador)
        private void FlpManoJugador_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        // La acción termina (flpManoJugador)
        private void FlpManoJugador_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Move;
        }

        // Comprobar al soltar y reposiciona definitivamente (flpManoJugador)
        private void FlpManoJugador_DragDrop(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox))) {
                var pb = e.Data.GetData(typeof(PictureBox)) as PictureBox;
                
                // Si la carta es de la pila de descarte, actualizamos los controles
                if (pb != null && pb.Parent != flpManoJugador) {
                    flpPilaDescarte.Controls.Remove(pb);
                    flpManoJugador.Controls.Add(pb);

                    // Actualizar la mano del jugador internamente
                    manoJugador.Add(pb.Tag.ToString());
                }

                // Calcular la posición de las cartas
                Point point = flpManoJugador.PointToClient(new Point(e.X, e.Y));
                Control? target = flpManoJugador.GetChildAtPoint(point);

                int indice = (target == null)
                    ? flpManoJugador.Controls.Count - 1
                    : flpManoJugador.Controls.GetChildIndex(target, false);

                // Reposicionar la imagen
                flpManoJugador.Controls.SetChildIndex(pb, indice);
                flpManoJugador.Invalidate();
            }
        }

        // ============================
        // PILA DE DESCARTE
        // ============================

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
                Image = Image.FromFile(rutaImagen),
                Tag = carta
            };

            pictureBox.MouseDown += PD_MouseDown; // Mouse Down en la Pila de Descarte

            // Añadir la imagen
            flpPilaDescarte.Controls.Add(pictureBox);
        }

        // Mouse Down en la pila de descarte
        private void PD_MouseDown(object? sender, MouseEventArgs e) {
            var pb = sender as PictureBox;
            if (pb != null)
                pb.DoDragDrop(pb, DragDropEffects.Move);
        }

        // Entrar en la acción (flpPilaDescarte)
        private void FlpPilaDescarte_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        // Terminar la acción (flpPilaDescarte)
        private void FlpPilaDescarte_DragDrop(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox))) {
                var pb = e.Data.GetData(typeof(PictureBox)) as PictureBox;
                flpManoJugador.Controls.Remove(pb);
                flpPilaDescarte.Controls.Clear(); // Solo una carta en la pila
                flpPilaDescarte.Controls.Add(pb);

                // Actualiza la lista interna de la mano
                string nombreCarta = pb.Tag.ToString();
                manoJugador.Remove(nombreCarta);
                // Agrega a la pila de descarte si tienes una lista para ello
            }
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