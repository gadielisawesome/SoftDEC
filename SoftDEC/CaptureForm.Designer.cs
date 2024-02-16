﻿namespace SoftDEC
{
    partial class CaptureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CaptureForm));
            this.Spacer1 = new System.Windows.Forms.Panel();
            this.TitleHolder = new System.Windows.Forms.Panel();
            this.TitleText = new System.Windows.Forms.Label();
            this.DescriptionHolder = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Spacer4 = new System.Windows.Forms.Panel();
            this.DescriptionText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TitleHolder.SuspendLayout();
            this.DescriptionHolder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Spacer1
            // 
            this.Spacer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Spacer1.Location = new System.Drawing.Point(0, 0);
            this.Spacer1.Name = "Spacer1";
            this.Spacer1.Size = new System.Drawing.Size(1280, 50);
            this.Spacer1.TabIndex = 0;
            // 
            // TitleHolder
            // 
            this.TitleHolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.TitleHolder.Controls.Add(this.TitleText);
            this.TitleHolder.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleHolder.Font = new System.Drawing.Font("Montserrat", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleHolder.ForeColor = System.Drawing.Color.Yellow;
            this.TitleHolder.Location = new System.Drawing.Point(0, 50);
            this.TitleHolder.Name = "TitleHolder";
            this.TitleHolder.Size = new System.Drawing.Size(1280, 120);
            this.TitleHolder.TabIndex = 1;
            // 
            // TitleText
            // 
            this.TitleText.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleText.Font = new System.Drawing.Font("Montserrat", 60F, System.Drawing.FontStyle.Bold);
            this.TitleText.Location = new System.Drawing.Point(0, 0);
            this.TitleText.Name = "TitleText";
            this.TitleText.Size = new System.Drawing.Size(1280, 100);
            this.TitleText.TabIndex = 0;
            this.TitleText.Text = "OOPS... SOMETHING BROKE";
            this.TitleText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TitleText.UseMnemonic = false;
            // 
            // DescriptionHolder
            // 
            this.DescriptionHolder.BackColor = System.Drawing.Color.Black;
            this.DescriptionHolder.Controls.Add(this.DescriptionText);
            this.DescriptionHolder.Controls.Add(this.pictureBox1);
            this.DescriptionHolder.Controls.Add(this.panel1);
            this.DescriptionHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionHolder.Font = new System.Drawing.Font("Montserrat", 25F, System.Drawing.FontStyle.Bold);
            this.DescriptionHolder.ForeColor = System.Drawing.Color.White;
            this.DescriptionHolder.Location = new System.Drawing.Point(0, 170);
            this.DescriptionHolder.Name = "DescriptionHolder";
            this.DescriptionHolder.Size = new System.Drawing.Size(1280, 500);
            this.DescriptionHolder.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.ErrorImage = global::SoftDEC.Properties.Resources.spinner;
            this.pictureBox1.Image = global::SoftDEC.Properties.Resources.spinner;
            this.pictureBox1.InitialImage = global::SoftDEC.Properties.Resources.spinner;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1280, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // Spacer4
            // 
            this.Spacer4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Spacer4.Location = new System.Drawing.Point(0, 670);
            this.Spacer4.Name = "Spacer4";
            this.Spacer4.Size = new System.Drawing.Size(1280, 50);
            this.Spacer4.TabIndex = 4;
            // 
            // DescriptionText
            // 
            this.DescriptionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DescriptionText.Font = new System.Drawing.Font("Montserrat", 25F, System.Drawing.FontStyle.Bold);
            this.DescriptionText.Location = new System.Drawing.Point(0, 150);
            this.DescriptionText.Name = "DescriptionText";
            this.DescriptionText.Size = new System.Drawing.Size(1280, 350);
            this.DescriptionText.TabIndex = 2;
            this.DescriptionText.Text = resources.GetString("DescriptionText.Text");
            this.DescriptionText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DescriptionText.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1280, 25);
            this.panel1.TabIndex = 3;
            // 
            // CaptureForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.DescriptionHolder);
            this.Controls.Add(this.Spacer4);
            this.Controls.Add(this.TitleHolder);
            this.Controls.Add(this.Spacer1);
            this.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CaptureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SoftDEC Temporary Capture Window";
            this.Load += new System.EventHandler(this.CaptureForm_Load);
            this.TitleHolder.ResumeLayout(false);
            this.DescriptionHolder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Spacer1;
        private System.Windows.Forms.Panel TitleHolder;
        private System.Windows.Forms.Label TitleText;
        private System.Windows.Forms.Panel DescriptionHolder;
        private System.Windows.Forms.Label DescriptionText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Spacer4;
    }
}

