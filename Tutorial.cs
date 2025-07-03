namespace Chinchon {
    public partial class Tutorial : Form {
        public Tutorial() {
            InitializeComponent();
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