using Chinchon.src.Utils;

namespace Chinchon.src.forms {
    public partial class Partida : Form {
        // Campos para poder usar las cartas
        private Cartas mazo;
        private List<string> manoJugador;
        private List<string> manoComputadora;

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

            var pb = sender as PictureBox;
            if (pb != null) {
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

                CambiarEstadoTurno(EstadoTurno.EsperandoRobo);

                MostarManoJugador();
            }
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

                    lblEstado.Text = "Roba una carta del mazo o la pila de descarte";
                    break;

                case EstadoTurno.EsperandoDescarte:
                    // No puedes robar
                    mazoRobar.Enabled = false;
                    flpPilaDescarte.AllowDrop = true;

                    lblEstado.Text = "Descarta una carta";
                    break;

                case EstadoTurno.EsperandoOponente:
                    // Desactivar todo
                    mazoRobar.Enabled = false;
                    flpPilaDescarte.Enabled = false;

                    lblEstado.Text = "Esperando al oponente...";
                    break;
            }
        }

        // ==========================================
        // CERRAR, TERMINA EL JUEGO
        // ==========================================
        private void Cerrar_Click(object sender, EventArgs e) {
            if (!PuedeCerrar(manoJugador)) {
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
                int puntosJugador = CalcularPuntos(manoJugador);
                int puntosComputadora = CalcularPuntos(manoComputadora);

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

        // Calcular la puntuación de la mano del jugador
        private int CalcularPuntos(List<string> mano) {
            // Chinchón
            var palos = mano.Select(c => c.Split(' ')[1]).Distinct();

            foreach (var palo in palos) {
                var numeros = mano
                    .Where(c => c.EndsWith(palo))
                    .Select(c => int.Parse(c.Split(' ')[0]))
                    .OrderBy(n => n)
                    .ToList();

                if (numeros.Count == 7 && EsEscalera(numeros)) return 0;
            }

            // Obtener los grupos
            var grupos = ObtenerGrupos(mano);

            // Selección golosa de grupos disjuntos
            var seleccionados = new List<int>();
            foreach (var g in grupos.OrderByDescending(g => g.Count)) {
                if (!g.Any(idx => seleccionados.Contains(idx)))
                    seleccionados.AddRange(g);
            }

            // Sumamos valores de cartas no agrupadas
            int total = 0;
            
            for (int i = 0; i < mano.Count; i++) {
                if (!seleccionados.Contains(i))
                    total += int.Parse(mano[i].Split(' ')[0]);
            }
            return total;
        }

        // Dada una mano, comprueba si la carta sin casar es <= 5, o si están todas las cartas casadas
        public bool PuedeCerrar(List<string> mano) {
            // Agrupa cartas por número y por palo
            var numeros = mano.Select(c => int.Parse(c.Split(' ')[0])).ToList();
            var palos = mano.Select(c => c.Split(' ')[1]).ToList();

            // Comprobar si es escalera de 7 cartas del mismo palo (chinchón)
            foreach (var palo in palos.Distinct()) {
                var numerosDePalo = mano
                    .Where(c => c.EndsWith(palo))
                    .Select(c => int.Parse(c.Split(' ')[0]))
                    .OrderBy(n => n)
                    .ToList();

                // ¡Chinchón!
                if (numerosDePalo.Count == 7 && EsEscalera(numerosDePalo)) return true;
            }

            // Comprobar si es menos 10 (4 cartas casadas por un lado, 3 cartas casadas por el otro)
            var grupos = ObtenerGrupos(mano);

            if (grupos.Any(g => g.Count == 4) &&
                grupos.Any(g => g.Count == 3) &&
                grupos.Sum(g => g.Count) == 7) return true;

            // Comprobar seis cartas casadas y una suelta <= 5
            if (grupos.Sum(g => g.Count) == 6 && mano.Count == 7) {
                var indicesAgrupados = grupos.SelectMany(g => g).ToList();
                var cartaSuelta = mano.Where((c, i) => !indicesAgrupados.Contains(i)).FirstOrDefault();

                if (cartaSuelta != null) {
                    int valorSuelto = int.Parse(cartaSuelta.Split(' ')[0]);
                    
                    if (valorSuelto <= 5) return true;
                }
            }

            return false;
        }

        // Devuelve una lista de grupos de índices de cartas agrupadas (por número o escalera)
        private List<List<int>> ObtenerGrupos(List<string> mano) {
            var grupos = new List<List<int>>();

            // Buscar tríos y cuartetos
            var numeros = mano.Select(c => int.Parse(c.Split(' ')[0])).ToList();
            for (int n = 1; n <= 12; n++) {
                var indices = numeros
                    .Select((num, idx) => num == n ? idx : -1)
                    .Where(idx => idx != -1)
                    .ToList();
                if (indices.Count >= 3)
                    grupos.Add(indices);
            }

            // Buscar escaleras por palo
            var palos = mano.Select(c => c.Split(' ')[1]).Distinct();

            foreach (var palo in palos) {
                var cartasDePalo = mano
                    .Select((c, idx) => new { c, idx })
                    .Where(x => x.c.EndsWith(palo))
                    .Select(x => new { Num = int.Parse(x.c.Split(' ')[0]), x.idx })
                    .OrderBy(x => x.Num)
                    .ToList();

                for (int i = 0; i <= cartasDePalo.Count - 3; i++) {
                    var escalera = new List<int> { cartasDePalo[i].idx };
                    int actual = cartasDePalo[i].Num;

                    for (int j = i + 1; j < cartasDePalo.Count; j++) {

                        if (cartasDePalo[j].Num == actual + 1) {
                            escalera.Add(cartasDePalo[j].idx);
                            actual++;
                        } else if (cartasDePalo[j].Num > actual + 1) break;
                    }
                    if (escalera.Count >= 3) grupos.Add(escalera);
                }
            }
            return grupos;
        }

        // Comprobar si es una escalera de números consecutivos
        private bool EsEscalera(List<int> numeros) {
            numeros.Sort();

            for (int i = 1; i < numeros.Count; i++) {
                if (numeros[i] != numeros[i - 1] + 1) return false;
            }
            return true;
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
            manoComputadora = manos[1]; // Será la mano que use la computadora

            MostarManoJugador();

            string cartaPinta = mazo.CrearPilaDescarte();
            MostrarPilaDescarte(cartaPinta);

            // Colocar el mazo
            string rutaReverso = Path.Combine(Application.StartupPath, "assets", "images", "REVERSO.jpg");
            mazoRobar.Image = Image.FromFile(rutaReverso);
            mazoRobar.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}