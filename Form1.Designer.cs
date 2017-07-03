namespace WinFormsBoletos
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
            this.btnSalvarHTML = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSalvarHTML
            // 
            this.btnSalvarHTML.Location = new System.Drawing.Point(160, 48);
            this.btnSalvarHTML.Name = "btnSalvarHTML";
            this.btnSalvarHTML.Size = new System.Drawing.Size(199, 64);
            this.btnSalvarHTML.TabIndex = 0;
            this.btnSalvarHTML.Text = "Gerar Boleto BB - HTML";
            this.btnSalvarHTML.UseVisualStyleBackColor = true;
            this.btnSalvarHTML.Click += new System.EventHandler(this.btnHTML_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(157, 136);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(201, 68);
            this.btnPDF.TabIndex = 1;
            this.btnPDF.Text = "Gerar Boleto BB - PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 261);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.btnSalvarHTML);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSalvarHTML;
        private System.Windows.Forms.Button btnPDF;
    }
}

