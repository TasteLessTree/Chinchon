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
            centrarElementos = new TableLayoutPanel();
            cerrar = new Button();
            puntuaciones = new Label();
            ((System.ComponentModel.ISupportInitialize)mazoRobar).BeginInit();
            centrarElementos.SuspendLayout();
            SuspendLayout();
            // 
            // flpManoJugador
            // 
            flpManoJugador.AllowDrop = true;
            flpManoJugador.Anchor = AnchorStyles.Bottom;
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
            // 
            // flpPilaDescarte
            // 
            flpPilaDescarte.AllowDrop = true;
            flpPilaDescarte.AutoScroll = true;
            flpPilaDescarte.BackgroundImage = Properties.Resources.DESCARTE;
            flpPilaDescarte.BackgroundImageLayout = ImageLayout.Stretch;
            flpPilaDescarte.Dock = DockStyle.Fill;
            flpPilaDescarte.Location = new Point(481, 5);
            flpPilaDescarte.Margin = new Padding(5);
            flpPilaDescarte.Name = "flpPilaDescarte";
            flpPilaDescarte.Size = new Size(194, 294);
            flpPilaDescarte.TabIndex = 1;
            flpPilaDescarte.DragDrop += FlpPilaDescarte_DragDrop;
            flpPilaDescarte.DragEnter += FlpPilaDescarte_DragEnter;
            // 
            // mazoRobar
            // 
            mazoRobar.BackgroundImage = Properties.Resources.MAZO;
            mazoRobar.BackgroundImageLayout = ImageLayout.Stretch;
            mazoRobar.Dock = DockStyle.Fill;
            mazoRobar.Location = new Point(277, 5);
            mazoRobar.Margin = new Padding(5);
            mazoRobar.Name = "mazoRobar";
            mazoRobar.Size = new Size(194, 294);
            mazoRobar.SizeMode = PictureBoxSizeMode.StretchImage;
            mazoRobar.TabIndex = 2;
            mazoRobar.TabStop = false;
            mazoRobar.Click += MazoRobar_Click;
            // 
            // lblEstado
            // 
            lblEstado.Anchor = AnchorStyles.Top;
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Andalus", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 178);
            lblEstado.Location = new Point(383, 24);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(445, 33);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Roba una carta del mazo o la pila de descarte";
            // 
            // centrarElementos
            // 
            centrarElementos.Anchor = AnchorStyles.None;
            centrarElementos.AutoSize = true;
            centrarElementos.ColumnCount = 4;
            centrarElementos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            centrarElementos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            centrarElementos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            centrarElementos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            centrarElementos.Controls.Add(flpPilaDescarte, 2, 0);
            centrarElementos.Controls.Add(mazoRobar, 1, 0);
            centrarElementos.Controls.Add(cerrar, 3, 0);
            centrarElementos.Location = new Point(153, 80);
            centrarElementos.Name = "centrarElementos";
            centrarElementos.RowCount = 1;
            centrarElementos.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            centrarElementos.Size = new Size(816, 304);
            centrarElementos.TabIndex = 4;
            // 
            // cerrar
            // 
            cerrar.Anchor = AnchorStyles.Bottom;
            cerrar.Font = new Font("Andalus", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 178);
            cerrar.Location = new Point(699, 256);
            cerrar.Margin = new Padding(5);
            cerrar.Name = "cerrar";
            cerrar.Size = new Size(97, 43);
            cerrar.TabIndex = 3;
            cerrar.Text = "Cerrar";
            cerrar.UseVisualStyleBackColor = true;
            cerrar.Click += Cerrar_Click;
            // 
            // puntuaciones
            // 
            puntuaciones.Anchor = AnchorStyles.Top;
            puntuaciones.AutoSize = true;
            puntuaciones.Font = new Font("Andalus", 16F, FontStyle.Regular, GraphicsUnit.Point, 178);
            puntuaciones.Location = new Point(533, 57);
            puntuaciones.Name = "puntuaciones";
            puntuaciones.Size = new Size(135, 34);
            puntuaciones.TabIndex = 5;
            puntuaciones.Text = "Puntuaciones";
            // 
            // Partida
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1214, 661);
            Controls.Add(centrarElementos);
            Controls.Add(lblEstado);
            Controls.Add(flpManoJugador);
            Controls.Add(puntuaciones);
            Name = "Partida";
            Text = "Jugar";
            FormClosing += Partida_FormClosing;
            Load += Partida_Load;
            ((System.ComponentModel.ISupportInitialize)mazoRobar).EndInit();
            centrarElementos.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flpManoJugador;
        private FlowLayoutPanel flpPilaDescarte;
        private PictureBox mazoRobar;
        private Label lblEstado;
        private TableLayoutPanel centrarElementos;
        private Button cerrar;
        private Label puntuaciones;
    }
}