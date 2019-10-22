namespace EDDS
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.panel = new System.Windows.Forms.Panel();
            this.sectionControl1 = new EDDS.Monitoring.SectionControl();
            this.SectionDataInterval = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel.Location = new System.Drawing.Point(12, 78);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1163, 381);
            this.panel.TabIndex = 0;
            // 
            // sectionControl1
            // 
            this.sectionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sectionControl1.Location = new System.Drawing.Point(12, 455);
            this.sectionControl1.Name = "sectionControl1";
            this.sectionControl1.Size = new System.Drawing.Size(1163, 274);
            this.sectionControl1.TabIndex = 1;
            // 
            // SectionDataInterval
            // 
            this.SectionDataInterval.Enabled = true;
            this.SectionDataInterval.Interval = 1000;
            this.SectionDataInterval.Tick += new System.EventHandler(this.SectionDataInterval_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1095, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "TestButton";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1187, 741);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sectionControl1);
            this.Controls.Add(this.panel);
            this.Name = "MainForm";
            this.Text = "EDDS v2.2";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private Monitoring.SectionControl sectionControl1;
        private System.Windows.Forms.Timer SectionDataInterval;
        private System.Windows.Forms.Button button1;
    }
}