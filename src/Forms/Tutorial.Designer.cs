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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tutorial));
            titulo = new Label();
            instructions = new Label();
            goBack = new Button();
            rules = new Label();
            SuspendLayout();
            // 
            // titulo
            // 
            titulo.AutoSize = true;
            titulo.Font = new Font("Andalus", 30F, FontStyle.Bold, GraphicsUnit.Point, 178);
            titulo.Location = new Point(334, 9);
            titulo.Name = "titulo";
            titulo.Size = new Size(167, 61);
            titulo.TabIndex = 1;
            titulo.Text = "Tutorial";
            // 
            // instructions
            // 
            instructions.AutoSize = true;
            instructions.Font = new Font("Andalus", 15F, FontStyle.Bold, GraphicsUnit.Point, 178);
            instructions.Location = new Point(12, 70);
            instructions.Name = "instructions";
            instructions.Size = new Size(800, 93);
            instructions.TabIndex = 2;
            instructions.Text = resources.GetString("instructions.Text");
            // 
            // goBack
            // 
            goBack.Font = new Font("Andalus", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            goBack.Location = new Point(717, 851);
            goBack.Name = "goBack";
            goBack.Size = new Size(115, 58);
            goBack.TabIndex = 4;
            goBack.Text = "Volver";
            goBack.UseVisualStyleBackColor = true;
            goBack.Click += goBack_Click;
            // 
            // rules
            // 
            rules.AutoSize = true;
            rules.Font = new Font("Andalus", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 178);
            rules.Location = new Point(12, 173);
            rules.Name = "rules";
            rules.Size = new Size(651, 720);
            rules.TabIndex = 5;
            rules.Text = resources.GetString("rules.Text");
            // 
            // Tutorial
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 921);
            Controls.Add(rules);
            Controls.Add(goBack);
            Controls.Add(instructions);
            Controls.Add(titulo);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "Tutorial";
            Text = "Tutorial";
            Load += Tutorial_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label titulo;
        private Label instructions;
        private Button goBack;
        private Label rules;
    }
}