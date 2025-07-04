using Chinchon.src.forms;

namespace Chinchon {
    public partial class MainMenu : Form {
        public MainMenu() {
            InitializeComponent();
        }

        // Al cargar el menú
        private void MainMenu_Load(object sender, EventArgs e) {
            // Posicionar en el centro de la pantalla
            this.CenterToScreen();

            // Tamaño del formulario
            this.Size = new Size(520, 570);

            // No se puede redimensionar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // Empezar a jugar
        private void Play_Click(object sender, EventArgs e) {
            // Ocultamos este formulario
            this.Hide();

            /* TODO: MENÚ INTERMEDIO CON LA POSIBILIDAD DE
            * SI QUIERES JUGAR CONTRA LA COMPUTADORA (y cuantas)
            * O JUGAR CONTRA UN AMIGO EN LÍNEA
            */
            // Abrimos la partida (de momento contra un ordenador)
            var partida = new Partida();
            partida.Show();
        }

        // Enviar a un nuevo formulario donde estén las instrucciones
        // De cómo se juega al chinchón
        private void Tutorial_Click(object sender, EventArgs e) {
            // Ocultamos el formulario actual
            this.Hide();

            // Mostramos el formulario del tutorial
            var tutorial = new Tutorial();
            tutorial.Show();
        }

        // Cerrar la aplicación
        private void Exit_Click(object sender, EventArgs e) {
            this.Close();
            Application.Exit();
        }
    }
}