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
            flpManoJugador.AutoScroll = true;
            flpManoJugador.BackColor = SystemColors.Control;
            flpManoJugador.ForeColor = SystemColors.ControlText;
            flpManoJugador.Location = new Point(14, 414);
            flpManoJugador.Margin = new Padding(5);
            flpManoJugador.Name = "flpManoJugador";
            flpManoJugador.Padding = new Padding(5);
            flpManoJugador.Size = new Size(1184, 233);
            flpManoJugador.TabIndex = 0;
            flpManoJugador.AllowDrop = true;
            flpManoJugador.DragEnter += FlpManoJugador_DragEnter;
            flpManoJugador.DragOver += FlpManoJugador_DragOver;
            flpManoJugador.DragDrop += FlpManoJugador_DragDrop;
            // 
            // Partida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
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