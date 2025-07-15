using Chinchon.src.Utils;

namespace Chinchon.src.forms {
    public partial class Partida : Form {
        // Campos para poder usar las cartas
        private Cartas mazo;
        private List<string> manoJugador;
        private List<string> manoComputadora;

        // Lógica de la computadora
        private Computadora computadora;

        // Controlar el turno
        private enum EstadoTurno {
            EsperandoRobo,
            EsperandoDescarte,
            EsperandoOponente
        }

        private EstadoTurno estadoTurno = EstadoTurno.EsperandoRobo;

        public Partida() {
            InitializeComponent();
        }

        // ==========================================
        // MANO DEL JUGADOR
        // ==========================================

        // Mostrar la mano del jugador
        private void MostarManoJugador() {
            // Limpiar controles
            flpManoJugador.Controls.Clear();

            foreach (var carta in manoJugador) {
                // Crear la imagen
                PictureBox pictureBox = new() {
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

            // DEBUG -> Cambiar el tipo de ejecución a aplicación de consola (proyecto > propiedades) o:
            // Chinchon.csproj -> cambiar: "<OutputType>WinExe</OutputType>" -> a: "<OutputType>Exe</OutputType>"
            // Console.WriteLine($"\t[{string.Join(", ", manoJugador)}]");
        }

        // Mouse Down (flpManoJugador)
        private void PB_MouseDown(object? sender, MouseEventArgs e) {
            if (sender is PictureBox pb)
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

                // Actualizar la lista interna según el nuevo orden del panel
                ActualizarManoJugadorDesdePanel();
            }
        }

        // Recontruye la lista manoJugador desde el orden de flpManoJugador.Controls
        // Ejemplo: visualmente tienes; 12 COPAS, 4 OROS, 5 ESPADAS, 5 BASTOS, ...
        // Pero internamente tienes; 4 OROS, 12 COPAS, 5 ESPADAS, 5 BASTOS, ...
        // Este método hace que la parte interna se actualize acorde a la parte visual
        private void ActualizarManoJugadorDesdePanel() {
            manoJugador = flpManoJugador.Controls
                .OfType<PictureBox>()
                .Select(pb => pb.Tag.ToString())
                .ToList();
        }

        // ==========================================
        // PILA DE DESCARTE
        // ==========================================

        // Mostrar pila de descarte, donde se dejan las cartas y luego se cierra
        private void MostrarPilaDescarte(string carta) {
            flpPilaDescarte.Controls.Clear();

            // Ruta a la imagen
            string rutaImagen = Path.Combine(Application.StartupPath, "assets", "images", carta + ".jpg");

            // Crear la imagen
            PictureBox pictureBox = new() {
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
            if (estadoTurno != EstadoTurno.EsperandoRobo) return;

            if (sender is PictureBox pb) {
                pb.DoDragDrop(pb, DragDropEffects.Move);

                CambiarEstadoTurno(EstadoTurno.EsperandoDescarte);
            }
        }

        // Entrar en la acción (flpPilaDescarte)
        // Permite drag si NO se ha robado una carta (ya sea del mazo o la propia pila)
        private void FlpPilaDescarte_DragEnter(object sender, DragEventArgs e) {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        // Terminar la acción (flpPilaDescarte)
        private void FlpPilaDescarte_DragDrop(object sender, DragEventArgs e) {
            if (estadoTurno != EstadoTurno.EsperandoDescarte) return;

            if (e.Data != null && e.Data.GetDataPresent(typeof(PictureBox))) {
                var pb = e.Data.GetData(typeof(PictureBox)) as PictureBox;

                flpManoJugador.Controls.Remove(pb);
                flpPilaDescarte.Controls.Clear(); // Solo una carta en la pila
                flpPilaDescarte.Controls.Add(pb);

                // Actualiza la lista interna de la mano
                string nombreCarta = pb.Tag.ToString();
                manoJugador.Remove(nombreCarta);

                MostarManoJugador();

                // Turno de la computadora
                CambiarEstadoTurno(EstadoTurno.EsperandoOponente);
                TurnoComputadora();
            }
        }

        // ==========================================
        // TURNO DE LA COMPUTADORA
        // ==========================================
        private async void TurnoComputadora() {
            // Esperar un par de segundos para que no sea instantáneo
            await Task.Delay(2500);

            var (cartaRobada, cartaDescartada, cierra) = computadora.JugarTurno();

            // Si roba de la pila, quitar la carta actual y añadir cartaDescarte
            if (flpPilaDescarte.Controls.Count > 0 &&
                flpPilaDescarte.Controls[0].Tag.ToString() == cartaRobada) {
                flpPilaDescarte.Controls.Clear();
            }

            MostrarPilaDescarte(cartaDescartada);

            // Comprobar si puede robar
            if (cierra)
                Cerrar_Click(null, EventArgs.Empty);
            else
                CambiarEstadoTurno(EstadoTurno.EsperandoRobo);
        }

        // ==========================================
        // MAZO PARA ROBAR
        // ==========================================
        private void MazoRobar_Click(object sender, EventArgs e) {
            if (estadoTurno != EstadoTurno.EsperandoRobo) return;

            try {
                // Robar la carta y añadirla a la mano del jugador
                string carta = mazo.RobarCarta();
                manoJugador.Add(carta);

                MostarManoJugador();

                CambiarEstadoTurno(EstadoTurno.EsperandoDescarte);
            }
            catch {
                // No hay más cartas para robar
                MessageBox.Show("¡NO HAY MÁS CARTAS EN EL MAZO!",
                                "Atención",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // MANEJO DEL ESTADO DEL TURNO
        // ==========================================
        private void CambiarEstadoTurno(EstadoTurno nuevoEstado) {
            estadoTurno = nuevoEstado;

            switch (estadoTurno) {
                case EstadoTurno.EsperandoRobo:
                    // Se puede robar
                    mazoRobar.Enabled = true;
                    flpPilaDescarte.AllowDrop = false;
                    flpPilaDescarte.Enabled = true;

                    lblEstado.Text = "Roba una carta del mazo o la pila de descarte";
                    break;

                case EstadoTurno.EsperandoDescarte:
                    // No puedes robar, solo descartar
                    mazoRobar.Enabled = false;
                    flpPilaDescarte.AllowDrop = true;
                    flpPilaDescarte.Enabled = true;

                    lblEstado.Text = "Descarta una carta";
                    break;

                case EstadoTurno.EsperandoOponente:
                    // Desactivar todo durante el turno de la computadora
                    mazoRobar.Enabled = false;
                    flpPilaDescarte.AllowDrop = false;
                    flpPilaDescarte.Enabled = false; 

                    lblEstado.Text = "Esperando al oponente...";
                    break;
            }
        }

        // ==========================================
        // CERRAR, TERMINA EL JUEGO
        // ==========================================
        private void Cerrar_Click(object sender, EventArgs e) {
            if (!PartidaHelpers.PuedeCerrar(manoJugador)) {
                MessageBox.Show("¡TODAVÍA NO PUEDES CERRAR!",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            } else {
                // Ocultar todos los elemetos
                lblEstado.Hide();
                flpPilaDescarte.Hide();
                flpManoJugador.Hide();
                mazoRobar.Hide();
                centrarElementos.Hide();

                // Calcular puntuaciones
                int puntosJugador = PartidaHelpers.CalcularPuntos(manoJugador);
                int puntosComputadora = PartidaHelpers.CalcularPuntos(manoComputadora);

                string resultado;

                // Suponemos que hacer chinchón son 0 puntos
                if (puntosJugador == 0 && puntosComputadora == 0)
                    resultado = "¡EMPATE! Ambos hicieron Chinchón.";
                else if (puntosJugador == 0)
                    resultado = "¡Ganaste con un Chinchón!";
                else if (puntosComputadora == 0)
                    resultado = "¡La computadora hizo Chinchón y gana!";
                else if (puntosJugador > puntosComputadora)
                    resultado = $"¡Ganaste! Tienes {puntosJugador} puntos. Computadora: {puntosComputadora} puntos.";
                else if (puntosComputadora > puntosJugador)
                    resultado = $" ¡Perdiste! La computadora tiene {puntosComputadora} puntos. Tú: {puntosJugador} puntos.";
                else
                    resultado = $"Empate a {puntosJugador} puntos.";

                // Mostrar el texto
                puntuaciones.Text = resultado;
                puntuaciones.Show();
            }
        }

        // ==========================================
        // CARGAR EL FORMULARIO
        // ==========================================
        private void Partida_Load(object sender, EventArgs e) {
            puntuaciones.Hide();

            this.CenterToScreen();
            this.Size = new Size(1230, 700);

            // Creamos y barajeamos el mazo
            mazo = new Cartas();
            mazo.Barajear();

            // DEBUG: Repartimos 7 cartas a 2 jugadores
            // Asignamos una mano al jugador
            var manos = mazo.RepartirCartas(7, 2);
            manoJugador = manos[0];
            manoComputadora = manos[1];

            // Crear computadora
            computadora = new Computadora(
                mazo,
                () => flpPilaDescarte.Controls.Count > 0
                    ? flpPilaDescarte.Controls[0].Tag.ToString()
                    : null);

            computadora.RecibirManoInicial(manoComputadora);

            MostarManoJugador();

            string cartaPinta = mazo.CrearPilaDescarte();
            MostrarPilaDescarte(cartaPinta);

            // Colocar el mazo
            string rutaReverso = Path.Combine(Application.StartupPath, "assets", "images", "REVERSO.jpg");
            mazoRobar.Image = Image.FromFile(rutaReverso);
            mazoRobar.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // ==========================================
        // CERRAR EL FORMULARIO
        // ==========================================
        private void Partida_FormClosing(object sender, FormClosingEventArgs e) {
            Application.Exit();
        }
    }
}