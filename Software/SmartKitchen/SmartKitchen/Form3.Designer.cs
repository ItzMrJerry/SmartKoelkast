namespace SmartKitchen
{
    partial class Form3
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
            lb_ingredienten = new ListBox();
            lbl_filter = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 28.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(151, 9);
            label1.Name = "label1";
            label1.Size = new Size(481, 100);
            label1.TabIndex = 2;
            label1.Text = "SmartKitchen";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lb_ingredienten
            // 
            lb_ingredienten.FormattingEnabled = true;
            lb_ingredienten.Location = new Point(44, 112);
            lb_ingredienten.Name = "lb_ingredienten";
            lb_ingredienten.Size = new Size(725, 484);
            lb_ingredienten.TabIndex = 3;
            // 
            // lbl_filter
            // 
            lbl_filter.AutoSize = true;
            lbl_filter.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_filter.Location = new Point(44, 599);
            lbl_filter.Name = "lbl_filter";
            lbl_filter.Size = new Size(145, 45);
            lbl_filter.TabIndex = 4;
            lbl_filter.Text = "Filter op:";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(44, 665);
            button1.Name = "button1";
            button1.Size = new Size(350, 140);
            button1.TabIndex = 5;
            button1.Text = "Terug";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 16.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.Location = new Point(419, 665);
            button2.Name = "button2";
            button2.Size = new Size(350, 140);
            button2.TabIndex = 6;
            button2.Text = "Genereer recept";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(824, 829);
            ControlBox = false;
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lbl_filter);
            Controls.Add(lb_ingredienten);
            Controls.Add(label1);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox lb_ingredienten;
        private Label lbl_filter;
        private Button button1;
        private Button button2;
    }
}