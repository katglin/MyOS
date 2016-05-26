namespace MyOS
{
    partial class Packet
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
            this.openPacketDialog = new System.Windows.Forms.OpenFileDialog();
            this.ChoosePacket = new System.Windows.Forms.Button();
            this.textResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openPacketDialog
            // 
            this.openPacketDialog.FileName = "openFileDialog1";
            this.openPacketDialog.Filter = "\"Текстовые файлы|*.txt\"\"";
            this.openPacketDialog.Title = "Выберите файл пакета";
            // 
            // ChoosePacket
            // 
            this.ChoosePacket.Location = new System.Drawing.Point(91, 12);
            this.ChoosePacket.Name = "ChoosePacket";
            this.ChoosePacket.Size = new System.Drawing.Size(124, 27);
            this.ChoosePacket.TabIndex = 0;
            this.ChoosePacket.Text = "Загрузить пакет";
            this.ChoosePacket.UseVisualStyleBackColor = true;
            this.ChoosePacket.Click += new System.EventHandler(this.ChoosePacket_Click);
            // 
            // textResult
            // 
            this.textResult.Location = new System.Drawing.Point(46, 57);
            this.textResult.Multiline = true;
            this.textResult.Name = "textResult";
            this.textResult.Size = new System.Drawing.Size(206, 75);
            this.textResult.TabIndex = 2;
            this.textResult.Visible = false;
            // 
            // Packet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 144);
            this.Controls.Add(this.textResult);
            this.Controls.Add(this.ChoosePacket);
            this.Name = "Packet";
            this.Text = "Packet";
            this.Load += new System.EventHandler(this.Packet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openPacketDialog;
        private System.Windows.Forms.Button ChoosePacket;
        private System.Windows.Forms.TextBox textResult;
    }
}