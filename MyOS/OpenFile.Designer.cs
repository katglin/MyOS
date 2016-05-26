namespace MyOS
{
    partial class OpenFile
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
            this.fileText = new System.Windows.Forms.TextBox();
            this.fSave = new System.Windows.Forms.Button();
            this.fClose = new System.Windows.Forms.Button();
            this.fInd = new System.Windows.Forms.Label();
            this.fName = new System.Windows.Forms.Label();
            this.fOwner = new System.Windows.Forms.Label();
            this.fRights = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileText
            // 
            this.fileText.Location = new System.Drawing.Point(33, 55);
            this.fileText.Multiline = true;
            this.fileText.Name = "fileText";
            this.fileText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.fileText.Size = new System.Drawing.Size(406, 191);
            this.fileText.TabIndex = 0;
            // 
            // fSave
            // 
            this.fSave.Location = new System.Drawing.Point(33, 266);
            this.fSave.Name = "fSave";
            this.fSave.Size = new System.Drawing.Size(75, 23);
            this.fSave.TabIndex = 3;
            this.fSave.Text = "Сохранить";
            this.fSave.UseVisualStyleBackColor = true;
            this.fSave.Click += new System.EventHandler(this.fSave_Click);
            // 
            // fClose
            // 
            this.fClose.Location = new System.Drawing.Point(364, 266);
            this.fClose.Name = "fClose";
            this.fClose.Size = new System.Drawing.Size(75, 23);
            this.fClose.TabIndex = 4;
            this.fClose.Text = "Закрыть";
            this.fClose.UseVisualStyleBackColor = true;
            this.fClose.Click += new System.EventHandler(this.fClose_Click);
            // 
            // fInd
            // 
            this.fInd.AutoSize = true;
            this.fInd.Location = new System.Drawing.Point(206, 13);
            this.fInd.Name = "fInd";
            this.fInd.Size = new System.Drawing.Size(0, 13);
            this.fInd.TabIndex = 5;
            // 
            // fName
            // 
            this.fName.AutoSize = true;
            this.fName.Location = new System.Drawing.Point(30, 13);
            this.fName.Name = "fName";
            this.fName.Size = new System.Drawing.Size(0, 13);
            this.fName.TabIndex = 6;
            // 
            // fOwner
            // 
            this.fOwner.AutoSize = true;
            this.fOwner.Location = new System.Drawing.Point(30, 29);
            this.fOwner.Name = "fOwner";
            this.fOwner.Size = new System.Drawing.Size(0, 13);
            this.fOwner.TabIndex = 7;
            // 
            // fRights
            // 
            this.fRights.AutoSize = true;
            this.fRights.Location = new System.Drawing.Point(255, 39);
            this.fRights.Name = "fRights";
            this.fRights.Size = new System.Drawing.Size(0, 13);
            this.fRights.TabIndex = 8;
            // 
            // OpenFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 301);
            this.Controls.Add(this.fRights);
            this.Controls.Add(this.fOwner);
            this.Controls.Add(this.fName);
            this.Controls.Add(this.fInd);
            this.Controls.Add(this.fClose);
            this.Controls.Add(this.fSave);
            this.Controls.Add(this.fileText);
            this.Name = "OpenFile";
            this.Text = "OpenFile";
            this.Load += new System.EventHandler(this.OpenFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileText;
        private System.Windows.Forms.Button fSave;
        private System.Windows.Forms.Button fClose;
        private System.Windows.Forms.Label fInd;
        private System.Windows.Forms.Label fName;
        private System.Windows.Forms.Label fOwner;
        private System.Windows.Forms.Label fRights;
    }
}