namespace Chinchon {
    partial class MainMenu {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            titulo = new Label();
            play = new Button();
            tutorial = new Button();
            exit = new Button();
            SuspendLayout();
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Font = new Font("Andalus", 30F, FontStyle.Bold, GraphicsUnit.Point, 178);
            titulo.Location = new Point(127, 25);
            titulo.Name = "titulo";
            titulo.Size = new Size(236, 61);
            titulo.TabIndex = 0;
            titulo.Text = "CHINCHÓN";
            // 
            // play
            // 
            play.Font = new Font("Andalus", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            play.Location = new Point(134, 180);
            play.Name = "play";
            play.Size = new Size(229, 82);
            play.TabIndex = 1;
            play.Text = "Jugar";
            play.UseVisualStyleBackColor = true;
            play.Click += Play_Click;
            // 
            // tutorial
            // 
            tutorial.Font = new Font("Andalus", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            tutorial.Location = new Point(134, 289);
            tutorial.Name = "tutorial";
            tutorial.Size = new Size(229, 82);
            tutorial.TabIndex = 2;
            tutorial.Text = "Tutorial";
            tutorial.UseVisualStyleBackColor = true;
            tutorial.Click += Tutorial_Click;
            // 
            // exit
            // 
            exit.Font = new Font("Andalus", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            exit.Location = new Point(134, 404);
            exit.Name = "exit";
            exit.Size = new Size(229, 82);
            exit.TabIndex = 3;
            exit.Text = "Salir";
            exit.UseVisualStyleBackColor = true;
            exit.Click += Exit_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 531);
            Controls.Add(exit);
            Controls.Add(tutorial);
            Controls.Add(play);
            Controls.Add(titulo);
            Name = "MainMenu";
            Text = "Chinchón - Menú";
            Load += MainMenu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo;
        private Button play;
        private Button tutorial;
        private Button exit;
    }
}