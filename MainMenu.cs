namespace Chinchon {
    public partial class MainMenu : Form {
        public MainMenu() {
            InitializeComponent();
        }

        private void MainMenu_Load(object sender, EventArgs e) {
            // Tamaño del formulario
            this.Size = new Size(520, 570);

            // No se puede redimensionar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // Enviar a un nuevo formulario donde estén las instrucciones
        // De cómo se juega al chinchón
        private void tutorial_Click(object sender, EventArgs e) {
            // Ocultamos el formulario actual
            this.Hide();

            // Mostramos el formulario del tutorial
            var tutorial = new Tutorial();
            tutorial.Show();
        }

        // Cerrar la aplicación
        private void exit_Click(object sender, EventArgs e) {
            this.Close();
            Application.Exit();
        }
    }
}