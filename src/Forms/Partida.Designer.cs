using System.Windows.Forms.Design;

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
            flpPilaDescarte = new FlowLayoutPanel();
            mazoRobar = new PictureBox();
            lblEstado = new Label();
            ((System.ComponentModel.ISupportInitialize)mazoRobar).BeginInit();
            SuspendLayout();
            // 
            // flpManoJugador
            // 
            flpManoJugador.AllowDrop = true;
            flpManoJugador.AutoScroll = true;
            flpManoJugador.BackColor = SystemColors.Control;
            flpManoJugador.ForeColor = SystemColors.ControlText;
            flpManoJugador.Location = new Point(14, 392);
            flpManoJugador.Margin = new Padding(5);
            flpManoJugador.Name = "flpManoJugador";
            flpManoJugador.Padding = new Padding(5);
            flpManoJugador.Size = new Size(1184, 255);
            flpManoJugador.TabIndex = 0;
            flpManoJugador.DragDrop += FlpManoJugador_DragDrop;
            flpManoJugador.DragEnter += FlpManoJugador_DragEnter;
            flpManoJugador.DragOver += FlpManoJugador_DragOver;
            flpManoJugador.Anchor = (AnchorStyles.Bottom);
            // 
            // flpPilaDescarte
            // 
            flpPilaDescarte.AllowDrop = true;
            flpPilaDescarte.AutoScroll = true;
            flpPilaDescarte.BackgroundImage = Properties.Resources.DESCARTE;
            flpPilaDescarte.BackgroundImageLayout = ImageLayout.Stretch;
            flpPilaDescarte.Location = new Point(608, 123);
            flpPilaDescarte.Name = "flpPilaDescarte";
            flpPilaDescarte.Size = new Size(200, 250);
            flpPilaDescarte.TabIndex = 1;
            flpPilaDescarte.WrapContents = false;
            flpPilaDescarte.DragDrop += FlpPilaDescarte_DragDrop;
            flpPilaDescarte.DragEnter += FlpPilaDescarte_DragEnter;
            // 
            // mazoRobar
            // 
            mazoRobar.BackgroundImage = Properties.Resources.MAZO;
            mazoRobar.BackgroundImageLayout = ImageLayout.Stretch;
            mazoRobar.Location = new Point(344, 123);
            mazoRobar.Name = "mazoRobar";
            mazoRobar.Size = new Size(200, 250);
            mazoRobar.TabIndex = 2;
            mazoRobar.TabStop = false;
            mazoRobar.Click += MazoRobar_Click;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Andalus", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 178);
            lblEstado.Location = new Point(415, 55);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(129, 33);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Roba una carta del mazo o la pila de descarte";
            lblEstado.Anchor = (AnchorStyles.Top);
            // 
            // Partida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(lblEstado);
            Controls.Add(mazoRobar);
            Controls.Add(flpPilaDescarte);
            Controls.Add(flpManoJugador);
            Name = "Partida";
            Text = "Jugar";
            Load += Partida_Load;
            ((System.ComponentModel.ISupportInitialize)mazoRobar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpManoJugador;
        private FlowLayoutPanel flpPilaDescarte;
        private ImageList imageList1;
        private PictureBox mazoRobar;
        private Label lblEstado;
    }
}