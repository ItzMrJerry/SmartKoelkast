namespace SmartKitchen
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            groupBox1 = new GroupBox();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            Producten = new ListBox();
            button1 = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(155, 9);
            label1.Name = "label1";
            label1.Size = new Size(481, 100);
            label1.TabIndex = 3;
            label1.Text = "SmartKitchen";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.Location = new Point(25, 112);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 76);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(156, 25);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(93, 36);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Fruit";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(0, 25);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(131, 36);
            radioButton1.TabIndex = 0;
            radioButton1.TabStop = true;
            radioButton1.Text = "Groente";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // Producten
            // 
            Producten.FormattingEnabled = true;
            Producten.Location = new Point(25, 194);
            Producten.Name = "Producten";
            Producten.Size = new Size(776, 452);
            Producten.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(226, 677);
            button1.Name = "button1";
            button1.Size = new Size(350, 140);
            button1.TabIndex = 6;
            button1.Text = "Terug";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 829);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(Producten);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Name = "Form4";
            Text = "Form4";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private ListBox Producten;
        private Button button1;
    }
}