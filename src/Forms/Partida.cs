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

                // Habilitar drag and drop
                pictureBox.MouseDown += PB_MouseDown;
                pictureBox.DragEnter += PB_DragEnter;
                pictureBox.DragOver += PB_DragOver;
                pictureBox.DragDrop += PB_DragDrop;

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

        // Hacer click en una de las cartas
        private void PB_MouseDown(object? sender, MouseEventArgs e) {
            PictureBox? pb = sender as PictureBox;
            
            if (pb !=null)
                flpManoJugador.DoDragDrop(pb, DragDropEffects.Move);
        }

        // Empezar a arrastrar la carta
        private void PB_DragEnter(object? sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
        }

        // Enseñar la posición en tiempo real
        private void PB_DragOver(object? sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;

            // Establecer índice en tiempo real mientras la carta es arrastrada
            Point point = flpManoJugador.PointToClient(new Point(e.X, e.Y));
            Control? hover = flpManoJugador.GetChildAtPoint(point);

            if (hover != null && e.Data != null) {
                PictureBox? arrastrada = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                int indice = flpManoJugador.Controls.GetChildIndex(hover, false);

                if (arrastrada != null) {
                    flpManoJugador.Controls.SetChildIndex(arrastrada, indice);
                    flpManoJugador.Invalidate();
                }
            }
        }

        // Comprueba al soltar y reposiciona definitivamente.
        private void PB_DragDrop(object? sender, DragEventArgs e) {
            if (e.Data != null) {
                PictureBox? arrastrada = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                Point point = flpManoJugador.PointToClient(new Point(e.X, e.Y));
                Control? objetivo = flpManoJugador.GetChildAtPoint(point);

                if (objetivo != null && arrastrada != null) {
                    int indice = flpManoJugador.Controls.GetChildIndex(objetivo, false);

                    flpManoJugador.Controls.SetChildIndex(arrastrada, indice);
                    flpManoJugador.Invalidate();
                }
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
        }
    }
}