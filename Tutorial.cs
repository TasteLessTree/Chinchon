namespace Chinchon {
    public partial class Tutorial : Form {
        public Tutorial() {
            InitializeComponent();
        }

        // Redimensionar el formulario al cargarlo
        private void Tutorial_Load(object sender, EventArgs e) {
            // Tamaño
            this.Size = new Size(860, 960);

            // No se puede redimensionar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // Volver al menú principal
        private void goBack_Click(object sender, EventArgs e) {
            // Cerrar este formulario
            this.Hide();

            // Abrir el menú principal
            var mainMenu = new MainMenu();
            mainMenu.Closed += (s, args) => this.Close();
            mainMenu.Show();
        }
    }
}