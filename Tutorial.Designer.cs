namespace Chinchon {
    partial class Tutorial {
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
            titulo = new Label();
            instructions = new Label();
            goBack = new Button();
            SuspendLayout();
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Font = new Font("Andalus", 30F, FontStyle.Bold, GraphicsUnit.Point, 178);
            titulo.Location = new Point(281, 9);
            titulo.Name = "titulo";
            titulo.Size = new Size(167, 61);
            titulo.TabIndex = 1;
            titulo.Text = "Tutorial";
            // 
            // instructions
            // 
            instructions.AutoSize = true;
            instructions.Location = new Point(36, 96);
            instructions.Name = "instructions";
            instructions.Size = new Size(224, 15);
            instructions.TabIndex = 2;
            instructions.Text = "Cómo jugar al chinchón. Próximamente";
            // 
            // goBack
            // 
            goBack.Font = new Font("Andalus", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            goBack.Location = new Point(673, 371);
            goBack.Name = "goBack";
            goBack.Size = new Size(115, 58);
            goBack.TabIndex = 4;
            goBack.Text = "Volver";
            goBack.UseVisualStyleBackColor = true;
            goBack.Click += this.goBack_Click;
            // 
            // Tutorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 441);
            Controls.Add(goBack);
            Controls.Add(instructions);
            Controls.Add(titulo);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "Tutorial";
            Text = "TUTORIAL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo;
        private Label instructions;
        private Button goBack;
    }
}