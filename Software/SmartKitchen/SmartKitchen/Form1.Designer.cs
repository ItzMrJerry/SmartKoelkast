namespace SmartKitchen
{
    partial class HomePage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lb_producten = new ListBox();
            button1 = new Button();
            button2 = new Button();
            btn_exit = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(166, 9);
            label1.Name = "label1";
            label1.Size = new Size(481, 100);
            label1.TabIndex = 0;
            label1.Text = "SmartKitchen";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_producten
            // 
            lb_producten.FormattingEnabled = true;
            lb_producten.Location = new Point(42, 140);
            lb_producten.Name = "lb_producten";
            lb_producten.Size = new Size(725, 484);
            lb_producten.TabIndex = 1;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(42, 679);
            button1.Name = "button1";
            button1.Size = new Size(350, 140);
            button1.TabIndex = 2;
            button1.Text = "Niet scanbaar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(417, 679);
            button2.Name = "button2";
            button2.Size = new Size(350, 140);
            button2.TabIndex = 3;
            button2.Text = "Genereer recept";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btn_exit
            // 
            btn_exit.Location = new Point(693, 37);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new Size(74, 45);
            btn_exit.TabIndex = 4;
            btn_exit.Text = "Exit";
            btn_exit.UseVisualStyleBackColor = true;
            btn_exit.Click += btn_exit_Click;
            // 
            // HomePage
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 829);
            ControlBox = false;
            Controls.Add(btn_exit);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lb_producten);
            Controls.Add(label1);
            Name = "HomePage";
            Load += HomePage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lb_producten;
        private Button button1;
        private Button button2;
        private Button btn_exit;
    }
}
