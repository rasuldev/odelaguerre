namespace OdeWithLaguerre
{
    partial class Form1
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
            this.nupCoeffsCount = new System.Windows.Forms.NumericUpDown();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nupCoeffsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.nupCoeffsCount);
            this.panelBottom.Controls.SetChildIndex(this.nupCoeffsCount, 0);
            this.panelBottom.Controls.SetChildIndex(this.seriesListBox, 0);
            // 
            // nupCoeffsCount
            // 
            this.nupCoeffsCount.Location = new System.Drawing.Point(417, 19);
            this.nupCoeffsCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nupCoeffsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCoeffsCount.Name = "nupCoeffsCount";
            this.nupCoeffsCount.Size = new System.Drawing.Size(120, 20);
            this.nupCoeffsCount.TabIndex = 2;
            this.nupCoeffsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nupCoeffsCount.ValueChanged += new System.EventHandler(this.nupCoeffsCount_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 534);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nupCoeffsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nupCoeffsCount;
    }
}

