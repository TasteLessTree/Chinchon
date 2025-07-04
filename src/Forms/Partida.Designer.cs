namespace Chinchon.src.forms {
    partial class Partida {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            flpManoJugador = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flpManoJugador
            // 
            flpManoJugador.BackColor = SystemColors.Control;
            flpManoJugador.ForeColor = SystemColors.ControlText;
            flpManoJugador.Location = new Point(12, 353);
            flpManoJugador.Name = "flpManoJugador";
            flpManoJugador.Size = new Size(1230, 276);
            flpManoJugador.TabIndex = 0;
            flpManoJugador.AutoScroll = true;
            flpManoJugador.WrapContents = true;
            flpManoJugador.FlowDirection = FlowDirection.LeftToRight;
            flpManoJugador.Padding = new Padding(5);
            flpManoJugador.Margin = new Padding(5);
            // 
            // Partida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1254, 641);
            Controls.Add(flpManoJugador);
            Name = "Partida";
            Text = "Jugar";
            Load += Partida_Load;
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flpManoJugador;
    }
}