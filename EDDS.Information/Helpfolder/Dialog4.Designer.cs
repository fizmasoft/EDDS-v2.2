namespace EDDS.Information.Helpfolder
{
    partial class Dialog4
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
            this.label2 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioBtn_VChPB = new System.Windows.Forms.RadioButton();
            this.radioBtn_PChPB = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Имя:";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(48, 81);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Добавить";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(157, 81);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Отменить";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(48, 46);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 20);
            this.textBox1.TabIndex = 1;
            // 
            // radioBtn_VChPB
            // 
            this.radioBtn_VChPB.AutoSize = true;
            this.radioBtn_VChPB.Checked = true;
            this.radioBtn_VChPB.Location = new System.Drawing.Point(48, 12);
            this.radioBtn_VChPB.Name = "radioBtn_VChPB";
            this.radioBtn_VChPB.Size = new System.Drawing.Size(55, 17);
            this.radioBtn_VChPB.TabIndex = 9;
            this.radioBtn_VChPB.TabStop = true;
            this.radioBtn_VChPB.Text = "ВЧПБ";
            this.radioBtn_VChPB.UseVisualStyleBackColor = true;
            // 
            // radioBtn_PChPB
            // 
            this.radioBtn_PChPB.AutoSize = true;
            this.radioBtn_PChPB.Location = new System.Drawing.Point(176, 12);
            this.radioBtn_PChPB.Name = "radioBtn_PChPB";
            this.radioBtn_PChPB.Size = new System.Drawing.Size(56, 17);
            this.radioBtn_PChPB.TabIndex = 9;
            this.radioBtn_PChPB.Text = "ПЧПБ";
            this.radioBtn_PChPB.UseVisualStyleBackColor = true;
            // 
            // Dialog4
            // 
            this.AcceptButton = this.btn_add;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(279, 126);
            this.Controls.Add(this.radioBtn_PChPB);
            this.Controls.Add(this.radioBtn_VChPB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dialog4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton radioBtn_VChPB;
        private System.Windows.Forms.RadioButton radioBtn_PChPB;
    }
}