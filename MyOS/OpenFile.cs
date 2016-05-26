using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyOS
{
    public partial class OpenFile : Form
    {
        public OpenFile()
        {
            InitializeComponent();
        }

        private void fClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenFile_Load(object sender, EventArgs e)
        {
        }

        public void showFile(int fInd)
        {
            this.fName.Text = "Файл: " + MainProgram.fs.files[fInd].Name + ".txt";
            this.fOwner.Text = "Владелец: " + MainProgram.fs.files[fInd].Owner.ToString();
            this.fInd.Text = fInd.ToString();
            this.fInd.Visible = false;

            // check rights for changing this file
            if (MainProgram.fs.files[fInd].Owner != MainProgram.currentuser.getLogin()
                && MainProgram.fs.files[fInd].Rights == "r" &&
                 MainProgram.currentuser.getLogin() != "admin")
            {
                fSave.Enabled = false;
                fileText.ReadOnly = true;
                fRights.Text = "Режим ограниченного доступа";
            }

            int length = MainProgram.fs.files[fInd].Size;
            byte[] txt = new byte[FSEmul.CLUST_SIZE];

            int cl_count = length / FSEmul.CLUST_SIZE + 1; // number of clusters of file
            int iCl = MainProgram.fs.files[fInd].First_clust; // index of current cluster
            BinaryReader br = new BinaryReader(File.Open("MyFS.dat", FileMode.Open), Encoding.UTF8);
            this.Show();
            while (iCl != 500)
            {   
                br.BaseStream.Seek((iCl - 1) * FSEmul.CLUST_SIZE, SeekOrigin.Begin);
                if (length >= FSEmul.CLUST_SIZE) { txt = br.ReadBytes(FSEmul.CLUST_SIZE); length -= txt.Length; }
                else if (length > 0) { txt = br.ReadBytes(length); length -= txt.Length; } 
                fileText.Text += Encoding.UTF8.GetString(txt);
                
                iCl = MainProgram.fs.listCl[iCl];
            }
            br.Close();
        }

        private void fSave_Click(object sender, EventArgs e)
        {
            int fInd = Convert.ToInt32(this.fInd.Text);
            int iCl = MainProgram.fs.files[fInd].First_clust;

            char[] allText = new char[fileText.Text.Length];
            allText = fileText.Text.ToCharArray();
            byte[] txt = System.Text.Encoding.UTF8.GetBytes(allText);
            int length = txt.Length; // fileText.Text.Length;

            // check if FS has enough clusters for text...
            int needCl = length / FSEmul.CLUST_SIZE;
            if (FSEmul.getFreeClustCount() < needCl)
            {
                MessageBox.Show("Файл слишком велик, не хватает кластеров. Сократите файл перед сохранением");
                return;
            }

            fileText.Text += "  ";
            clearFile(fInd); // clear blocks of this file in fs

            BinaryWriter bw = new BinaryWriter(File.Open("MyFS.dat", FileMode.Open), Encoding.UTF8);
            MainProgram.fs.files[fInd].Size = length+1; // text.Length;

            int cl_count = length / FSEmul.CLUST_SIZE + 1; // number of clusters for file

            for (int i = 0; i < cl_count; i++)
            {
                bw.BaseStream.Seek((iCl - 1) * FSEmul.CLUST_SIZE, SeekOrigin.Begin);

                int j = 0;
                int countBytes = 0;
                int countChars = 0;
                while (j < FSEmul.CLUST_SIZE && countBytes+1 < FSEmul.CLUST_SIZE)
                {
                    if (j < allText.Length)
                        bw.Write(allText[j]);
                    else break; 
                    countBytes += System.Text.Encoding.UTF8.GetBytes(allText[j].ToString()).Length;
                    countChars++;
                    j++;
                }
                
                // cut byte-array        
                if (System.Text.Encoding.UTF8.GetBytes(allText).Length > FSEmul.CLUST_SIZE)
                {
                    int num = allText.Length - countChars;
                    char[] TempallText = new char[num];
                    int k = 0;
                    while (k < num)
                    {
                        TempallText[k] = allText[k + countChars];
                        k++;
                    }
                    allText = new char[num];
                    allText = TempallText;
                }
                                   
                if (i+1 < cl_count) // not enough clusters for changeed file
                {
                   // get another free cluster
                   ushort newiCl = FSEmul.getFreeClust(false);
                   MainProgram.fs.listCl[iCl] = newiCl;
                   MainProgram.fs.listCl[newiCl] = 500;  
                   iCl = newiCl;
                }
            }
            bw.Close();
        }

        // clean file system file-clusters and fs.listCl (except 1-st)
        public void clearFile(int Ind)
        {
            ushort iCl = MainProgram.fs.files[Ind].First_clust;
            ushort oldiCl;
            byte[] clBytes = new byte[FSEmul.CLUST_SIZE];
            for (int i = 0; i < clBytes.Length; i++)
                clBytes[i] = Convert.ToByte(' ');
            string clB = Encoding.UTF8.GetString(clBytes);

            BinaryWriter bw = new BinaryWriter(File.Open("MyFS.dat", FileMode.Open), Encoding.UTF8);
            while (iCl != 500)
            {
                bw.BaseStream.Seek((iCl - 1) * FSEmul.CLUST_SIZE, SeekOrigin.Begin);
              
                bw.Write(clBytes);
                oldiCl = iCl;
                iCl = MainProgram.fs.listCl[iCl];
                MainProgram.fs.listCl[oldiCl] = 0; 
            }
            MainProgram.fs.listCl[MainProgram.fs.files[Ind].First_clust] = 500;   
            bw.Close();
        }
    }
}
