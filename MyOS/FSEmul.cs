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
    public partial class FSEmul : Form
    {
        #region params
        public static int curDir = 0;
        public static  ushort CLUST_SIZE = 512; // size of 1 cluster, 512 bytes
        public static  ushort FILES_CLUST_COUNT = 512; // count of clusters in file block, 512 clusters
        public static ushort FILE_STRUCT_SIZE = 55; // 63; // size of the file description 

        public static  ushort FAT_FIRST_CLUST = 2;  // first cluster of fat-table (fat-table - list of clusters)
        public static  ushort ROOT_FIRST_CLUST = 4; // first cluster of root directory, list of files
        public static  ushort FILES_FIRST_CLUST = 41; // first cluster of files block (files block - content of files) // 36 cl for ROOT
   
        public static  int MaxCountOfFiles = 300; // CLUST_SIZE * (FILES_CLUST_COUNT - FILES_FIRST_CLUST + 1) / (CLUST_SIZE + FILE_STRUCT_SIZE); // = 418 files can be created

        string path = "MyFS:\\";
        int IdOfCutFile = -1; // no cutting file
        #endregion

        public FSEmul()
        {
            InitializeComponent();
        }

        private void back_Click_1(object sender, EventArgs e)
        {
            FSEmul.FS_Save();
            this.Close();
        }

        private void FSEmul_Load(object sender, EventArgs e)
        {
            output.ReadOnly = true;
         //   output.Enabled = false;
            output.Text = path + ">";
            user.Text = MainProgram.currentuser.getLogin();
         //   FS_Start();

        }

        public static void FS_Start()
        {
            SuperBlock sb;
            ushort[] listCl;
            List<myFile> listFiles;

            curDir = 0;
       //     int countOfFiles = CLUST_SIZE * (FILES_FIRST_CLUST - ROOT_FIRST_CLUST + 1) / FILE_STRUCT_SIZE; // 487 files can be created

            if (!File.Exists("MyFS.dat"))
            {
                sb = new SuperBlock("MyFS", CLUST_SIZE, FAT_FIRST_CLUST, ROOT_FIRST_CLUST, FILES_FIRST_CLUST, FILES_CLUST_COUNT);
             //   listCl = new ushort[CLUST_SIZE * (FILES_CLUST_COUNT - FILES_FIRST_CLUST + 1) / (CLUST_SIZE + FILE_STRUCT_SIZE)];
             //   listCl = new ushort[FILES_CLUST_COUNT - FILES_FIRST_CLUST+1];
                listCl = new ushort[FILES_CLUST_COUNT];
                listFiles = new List<myFile>();
            }
            else 
            {
                BinaryReader br = new BinaryReader(File.Open("MyFS.dat", FileMode.Open), Encoding.UTF8);

                sb = new SuperBlock(br.ReadString(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16());
              //  listCl = new ushort[CLUST_SIZE * (FILES_CLUST_COUNT - FILES_FIRST_CLUST + 1) / (CLUST_SIZE + FILE_STRUCT_SIZE)];
                listCl = new ushort[FILES_CLUST_COUNT - FILES_FIRST_CLUST + 1];
                br.BaseStream.Seek((FAT_FIRST_CLUST-1)*CLUST_SIZE, SeekOrigin.Begin);
                int iCl = 0;  // number of cluster
                while (iCl<listCl.Length && br.BaseStream.Position < (ROOT_FIRST_CLUST - 1) * CLUST_SIZE)
                { //br.PeekChar() != -1 &&
                    listCl[iCl] = br.ReadUInt16(); // value for cluster iCl
                    iCl++;
                }
                listFiles = new List<myFile>();
                br.BaseStream.Seek((ROOT_FIRST_CLUST - 1) * CLUST_SIZE, SeekOrigin.Begin);
                int iFile = 0;  // number of file
                while (br.PeekChar() != -1 &&  ((FILES_FIRST_CLUST - 1) * CLUST_SIZE) >= br.BaseStream.Position + FILE_STRUCT_SIZE)
                {
                    myFile temp = new myFile();
                    listFiles.Add(temp);
                    listFiles[iFile].id = br.ReadInt32();
                    if (listFiles[iFile].id == -1)
                    {
                        listFiles.Remove(temp);
                        break;
                    }
                    listFiles[iFile].Name = br.ReadString();
                  /*  if (listFiles[iFile].Name == "")
                    {
                        listFiles.Remove(temp);
                        break;
                    } */
                    listFiles[iFile].Extend = br.ReadString();
                    listFiles[iFile].Date_time = br.ReadString();
                    listFiles[iFile].First_clust = br.ReadUInt16();
                    listFiles[iFile].Size = br.ReadInt32();
                    listFiles[iFile].Rights = br.ReadString();
                    listFiles[iFile].Parent_dir = br.ReadInt32();
                    listFiles[iFile].Owner = br.ReadString(); // ...
                    iFile++;
                }
                br.Close();
            }
            MainProgram.fs = new FS(sb, listCl, listFiles);
            if (MainProgram.fs.files.Count == 0) { createRootDir(); FS_Save(); }
        }

        public static void FS_Save()
        {
            BinaryWriter bw = new BinaryWriter(File.Open("MyFS.dat", FileMode.OpenOrCreate), Encoding.UTF8);

            bw.Write(MainProgram.fs.superblock.Name);
            bw.Write(MainProgram.fs.superblock.Cl_size);
            bw.Write(MainProgram.fs.superblock.Fat_f_cl);
            bw.Write(MainProgram.fs.superblock.Root_f_cl);
            bw.Write(MainProgram.fs.superblock.Files_f_cl);
            bw.Write(MainProgram.fs.superblock.Files_cl_c);

            bw.BaseStream.Seek((FAT_FIRST_CLUST-1)*CLUST_SIZE, SeekOrigin.Begin);
            for (int i = 0; i < MainProgram.fs.listCl.Length; i++)
            {
                bw.Write(MainProgram.fs.listCl[i]);
            }
            bw.BaseStream.Seek((ROOT_FIRST_CLUST - 1) * CLUST_SIZE, SeekOrigin.Begin);
            for (int i = 0; i < MainProgram.fs.files.Count; i++)
            {
                bw.Write(MainProgram.fs.files[i].id);
                bw.Write(MainProgram.fs.files[i].Name);
                bw.Write(MainProgram.fs.files[i].Extend);
                bw.Write(MainProgram.fs.files[i].Date_time);
                bw.Write(MainProgram.fs.files[i].First_clust);
                bw.Write(MainProgram.fs.files[i].Size);
                bw.Write(MainProgram.fs.files[i].Rights);
                bw.Write(MainProgram.fs.files[i].Parent_dir);
                bw.Write(MainProgram.fs.files[i].Owner);
            }
            if (MainProgram.fs.files.Count<FSEmul.MaxCountOfFiles)  bw.Write(-1); // not read deleted files

            bw.Close();
        }

        private void FSEmul_FormClosing(object sender, FormClosingEventArgs e)
        {
            FS_Save();
        }

        private void run_Click(object sender, EventArgs e)
        {
            string[] str = command.Text.Split(' ');
            output.Text += command.Text + Environment.NewLine; 
            command.Text = "";
            if (str.Length == 0) { output.Text += "Некорректная команда" + Environment.NewLine + path + ">"; }
            #region cd
            else if (str.Length == 1 && str[0].ToLower() == "cd")
            {
                command.Text = "";
                if (curDir != 0)
                {
                    int ind = path.LastIndexOf( MainProgram.fs.files[getIndOfCurFile()].Name +"\\");
                    path = path.Remove(ind);
                    // set parent dir of current dir as current
                    curDir = cdDir();
                    output.Text += path+ ">"; 
                    
                }
                else { output.Text += "Текущая папка - корневая" + Environment.NewLine + path + ">"; }
            }
            #endregion       
            #region dir
            else if (str.Length == 1 && str[0] == "dir")
            {
                showDirContent();
                output.Text += Environment.NewLine + Environment.NewLine + path + ">";
            }
            #endregion
            #region open dir
            else if (str.Length == 3 && str[0].ToLower() == "open" && str[1].ToLower() == "d" && str[2].Length > 0)
            {
                int ind = getIndOfDir(str[2]);
                if (ind == -1 || MainProgram.fs.files[ind].Parent_dir != curDir)
                { output.Text += "Папка не найдена" + Environment.NewLine + path; }
                else  //  dir exists
                {
                    if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                        || MainProgram.fs.files[ind].Rights != "n" ||
                         MainProgram.currentuser.getLogin() == "admin") // check rights
                    {
                        curDir = openDir(str[2]);
                        path += MainProgram.fs.files[getIndOfCurFile()].Name + "\\";
                        output.Text += path + ">";
                    }
                    else // no rights
                    {
                        output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">";
                    }
                }
            }
            #endregion
            #region open file
            else if (str.Length == 3 && str[0].ToLower() == "open" && str[1].ToLower() == "f" && str[2].Length > 0) // open file 
                {
                    int ind = getIndOfFile(str[2]);
                    if (ind == -1 || MainProgram.fs.files[ind].Parent_dir != curDir)
                    { output.Text += "Файл не найден" + Environment.NewLine + path; }
                    else  //  file exists
                    {
                        if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                            || MainProgram.fs.files[ind].Rights != "n" ||
                             MainProgram.currentuser.getLogin() == "admin") // check rights
                        {
                            // Open file fo reading / or(and) writing
                            (new OpenFile()).showFile(ind);
                            output.Text += path + ">";
                        }
                        else // no rights
                        {
                            output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">";
                        }
                    }
                }
            #endregion
            #region create dir
            else if (str.Length == 4 && str[0].ToLower() == "create" && str[1].ToLower() == "d" && str[2].Length > 0 && str[3].Length > 0)
            {
                if (MainProgram.fs.files.Count >= MaxCountOfFiles)
                { output.Text += "Количество созданных файлов достигло предела.Освободите место для создания новых" + Environment.NewLine + path + ">"; }
                else if (str[2].Length > 10) { output.Text += "Имя папки не должно содержать более 10 символов" + Environment.NewLine + path + ">"; }
                // check rights for writing in current dir
                else if (MainProgram.fs.files[getIndOfCurFile()].Owner == MainProgram.currentuser.getLogin()
                            || MainProgram.fs.files[getIndOfCurFile()].Rights == "rw" ||
                             MainProgram.currentuser.getLogin() == "admin")
                {
                    // check if name is origin
                    if (checkUnicName(str[2], false))
                    {
                        if (str[3] == "n" || str[3] == "r" || str[3] == "rw")
                        {
                            createFolder(str[2], str[3]);
                        }
                        else
                        {
                            output.Text += "Некорректная команда" + Environment.NewLine + path + ">";
                        }
                    }
                    else output.Text += "Это имя уже используется" + Environment.NewLine + path + ">";
                }
                else output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">";
            }
            #endregion
            #region create file
            else if (str.Length == 4 && str[0].ToLower() == "create" && str[1].ToLower() == "f" && str[2].Length > 0 && str[3].Length > 0)
            {
                if (MainProgram.fs.files.Count >= MaxCountOfFiles)
                { output.Text += "Количество созданных файлов достигло предела.Освободите место для создания новых" + Environment.NewLine + path + ">"; }
                else 
                if (str[2].Length > 10) { output.Text += "Имя файла не должно содержать более 10 символов" + Environment.NewLine + path + ">"; }
                // check rights for writing in current dir
                else if (MainProgram.fs.files[getIndOfCurFile()].Owner == MainProgram.currentuser.getLogin()
                            || MainProgram.fs.files[getIndOfCurFile()].Rights == "rw" ||
                             MainProgram.currentuser.getLogin() == "admin") // check rights
                {
                    if (checkUnicName(str[2], true))
                    {
                        if (str[3] == "n" || str[3] == "r" || str[3] == "rw")
                        {
                            createFile(str[2], str[3]);
                        }
                        else
                        {
                            output.Text += "Некорректная команда" + Environment.NewLine + path + ">";
                        }
                    }
                    else output.Text += "Это имя уже используется" + Environment.NewLine + path + ">";
                }
                else output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">";
            }
            #endregion
            #region delete file
            else if (str.Length == 3 && str[0].ToLower() == "del" && str[1].ToLower() == "f" && str[2].Length > 0)
            {
                int ind = getIndOfFile(str[2]);
                if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                        || MainProgram.fs.files[ind].Rights == "rw" ||
                         MainProgram.currentuser.getLogin() == "admin")// check rights
                {
                    //int ind = getIndOfFile(str[2]);
                    if (ind == -1 || MainProgram.fs.files[ind].Parent_dir != curDir)
                    { 
                        output.Text += "Файл не найден" + Environment.NewLine + path + ">";
                    }
                    else // file exists
                    {
                        deleteFile(ind);
                        output.Text += "Файл был удален" + Environment.NewLine + path + ">";
                    }
                }
                else
                { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
            }
            #endregion
            #region delete dir
            else if (str.Length == 3 && str[0].ToLower() == "del" && str[1].ToLower() == "d" && str[2].Length > 0)
            {
                int ind = getIndOfDir(str[2]);
                if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                        || MainProgram.fs.files[ind].Rights == "rw" ||
                         MainProgram.currentuser.getLogin() == "admin")  // check rights
                {
                  //  int ind = getIndOfDir(str[2]);
                    if (ind == -1 || MainProgram.fs.files[ind].Parent_dir != curDir)
                    {
                        output.Text += "Папка не найдена" + Environment.NewLine + path + ">";
                    }
                    else // dir exists
                    {
                        deleteDir(MainProgram.fs.files[ind].id);
                        output.Text += "Папка была удалена" + Environment.NewLine + path + ">";
                    }
                }
                else
                { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
            }
            #endregion
            #region rename file
            else if (str.Length == 4 && str[0].ToLower() == "rn" && str[1].ToLower() == "f" && str[2].Length > 0 && str[3].Length > 0)
            {
                if (str[3].Length > 10) { output.Text += "Имя файла не должно содержать более 10 символов" + Environment.NewLine + path + ">"; }
                else if (!checkUnicName(str[2], true))  // file exist
                {
                    if (checkUnicName(str[3], false))
                    {
                        int ind = getIndOfFile(str[2]);
                        if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                                   || MainProgram.fs.files[ind].Rights == "rw" ||
                                    MainProgram.currentuser.getLogin() == "admin") // check rights
                        {
                            changeName(ind, str[3]);
                            output.Text += "Имя файла было изменено" + Environment.NewLine + path + ">";
                        }
                        else { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
                    }
                    else { output.Text += "Переименование невозможно: в текущей директории уже есть файл с таким именем" + Environment.NewLine + path + ">"; }
                }
                else { output.Text += "Файл с таким именем не найден в текущем каталоге" + Environment.NewLine + path + ">"; }
            }
            #endregion
            #region rename dir
            else if (str.Length == 4 && str[0].ToLower() == "rn" && str[1].ToLower() == "d" && str[2].Length > 0 && str[3].Length > 0)
            {
                if (str[3].Length > 10) { output.Text += "Имя папки не должно содержать более 10 символов" + Environment.NewLine + path + ">"; }
                else if (!checkUnicName(str[2], false))  // dir exist
                {
                    if (checkUnicName(str[3], false))
                    {
                        int ind = getIndOfDir(str[2]);
                        if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                                   || MainProgram.fs.files[ind].Rights == "rw" ||
                                    MainProgram.currentuser.getLogin() == "admin") // check rights
                        {
                            changeName(ind, str[3]);
                            output.Text += "Имя папки было изменено" + Environment.NewLine + path + ">";
                        }
                        else { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
                    }
                    else { output.Text += "Переименование невозможно: в текущей директории уже есть папка с таким именем" + Environment.NewLine + path + ">"; }
                }
                else { output.Text += "Папка с таким именем не найдена в текущем каталоге" + Environment.NewLine + path + ">"; }
            }
            #endregion
            #region cut file
            else if (str.Length == 3 && str[0].ToLower() == "cut" &&
                (str[1].ToLower() == "f" || str[1].ToLower() == "d") && str[2].Length > 0)
            {
                if ((str[1].ToLower() == "f" && !checkUnicName(str[2], true)) || 
                    (str[1].ToLower() == "d" && !checkUnicName(str[2], false)))  // file exist
                {
                    int ind;
                    if (str[1].ToLower() == "f") ind = getIndOfFile(str[2]);
                    else ind = getIndOfDir(str[2]);
                    if (MainProgram.fs.files[ind].Owner == MainProgram.currentuser.getLogin()
                               || MainProgram.fs.files[ind].Rights == "rw" ||
                                MainProgram.currentuser.getLogin() == "admin") // check rights
                    {
                        IdOfCutFile = MainProgram.fs.files[ind].id;
                        output.Text += "Файл выбран. Укажите место назначения" + Environment.NewLine + path + ">";
                    }
                    else { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
                }
                else { output.Text += "Файл с таким именем не найден в текущем каталоге" + Environment.NewLine + path + ">"; }
            }
            #endregion
            #region insert file
            else if (str.Length == 1 && str[0].ToLower() == "ins")
            {
                if (IdOfCutFile != -1) // file is cutting
                {
                    if (checkDirIncapsul(IdOfCutFile, curDir))
                    {
                        if (MainProgram.fs.files[getIndOfCurFile()].Owner == MainProgram.currentuser.getLogin()
                            || MainProgram.fs.files[getIndOfCurFile()].Rights == "rw" ||
                             MainProgram.currentuser.getLogin() == "admin") // check rights
                        {
                            if (isUnikInNewDir(IdOfCutFile)) // check unickName if new dir
                            {
                                int ParId = MainProgram.fs.files[getIndByID(IdOfCutFile)].Parent_dir;
                                MainProgram.fs.files[getIndByID(ParId)].Size--;
                                MainProgram.fs.files[getIndByID(IdOfCutFile)].Parent_dir = curDir;
                                MainProgram.fs.files[getIndByID(curDir)].Size++;
                                IdOfCutFile = -1;
                                output.Text += "Файл успешно перемещен в текущий каталог" + Environment.NewLine + path + ">";
                            }
                            else { output.Text += "Перемещение невозможно: имена файлов/папок в одной директории не должны совпадать" + Environment.NewLine + path + ">"; }
                        }
                        else { output.Text += "У вас недостаточно прав" + Environment.NewLine + path + ">"; }
                    }
                    else { output.Text += "Папка назначения является дочерней для перемещаемой папки. Перемещение невозможно" + Environment.NewLine + path + ">"; }
                }
                else { output.Text += "Файл для перемещения не выбран" + Environment.NewLine + path + ">"; }
            }
            #endregion
            else { output.Text += "Некорректная команда" + Environment.NewLine + path + ">"; }
        }

        // return id of parent dir for current dir/file 
        public static int cdDir()
        {
            int i = getIndOfCurFile();
             return MainProgram.fs.files[i].Parent_dir;
        }
        // return id of open dir to set as current
        public static int openDir(string name)
        {
          //  int id = 0;
            int i = 0;
            bool findCurDir = false;

            while (!findCurDir && i < MainProgram.fs.files.Count)
            {
                if (MainProgram.fs.files[i].Name == name && MainProgram.fs.files[i].Parent_dir==curDir) { findCurDir = true; }
                else i++;
            }
            if (findCurDir) return MainProgram.fs.files[i].id;
            // return i;
            else return -1; // no such dir
        }

        public void showDirContent()  // fileName;  pr:file/dir;  data_time;  size;   owner;  rights;
        {
            int ind = 0, j = 0;
            int i = getIndOfCurFile();
     
            string[,] files = new string [MainProgram.fs.files[i].Size, 5];
            while (j < MainProgram.fs.files[i].Size)
            {
                if (MainProgram.fs.files[ind].Parent_dir == curDir)
                {   
                    if (MainProgram.fs.files[ind].First_clust!=0)  // file
                        files[j,0] = MainProgram.fs.files[ind].Name + "." + MainProgram.fs.files[ind].Extend;
                    else // dir
                        files[j,0] = MainProgram.fs.files[ind].Name;

                    files[j,1] = MainProgram.fs.files[ind].Date_time;
                    files[j,2] = MainProgram.fs.files[ind].Size.ToString();
                    files[j,3] = MainProgram.fs.files[ind].Owner;
                    files[j,4] = MainProgram.fs.files[ind].Rights; 
                    j++; ind++;
                }
                else ind++;
            }
            // output files[][] 
            output.Text += Environment.NewLine + "NAME \t\t" + " DATE_TIME" + "\tSIZE/COUNT " + "   RIGHTS " + "\t OWNER";
            for (int k = 0; k < MainProgram.fs.files[i].Size; k++)
            {
                output.Text += Environment.NewLine + String.Format("{0,-15}\t", files[k, 0]) +
                    String.Format("{0,-20}", files[k, 1]) + String.Format("\t{0,-6}", files[k, 2]) +
                    String.Format("\t{0,-2}", files[k, 4]) + String.Format("\t {0,-15}", files[k, 3]);
            }
        }

        public void createFolder(string name, string rights)
        {
            if (MainProgram.fs.files.Count < MaxCountOfFiles)
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                myFile f = new myFile(myFile.generateID(), name,"", 
                            dt.ToString() ,0,0, rights, curDir, MainProgram.currentuser.getLogin());
                MainProgram.fs.files.Add(f);
               MainProgram.fs.files[getIndOfCurFile()].Size++;
               output.Text += "Папка успешно создана" + Environment.NewLine + path + ">";
            }
            else { output.Text += "Превышено допустимое количество файлов/папок" + Environment.NewLine + path + ">"; }
        }

        public static int getIndOfCurFile()
        {
            int i = 0;
            bool findCurDir = false;
            while (!findCurDir)
            {
                if (MainProgram.fs.files[i].id == curDir) { findCurDir = true; }
                else i++;
            }
            return i;
        }

        public static int getIndOfFile(string name)
        {
            int i = 0;
            bool findFile = false;
            while (!findFile && i < MainProgram.fs.files.Count)
            {
                if (MainProgram.fs.files[i].Parent_dir == curDir)
                {
                    if (MainProgram.fs.files[i].Name == name && MainProgram.fs.files[i].First_clust != 0) { findFile = true; }
                    else i++;
                }
                else i++;
            }
            if (findFile) return i;
            else return -1;
        }
        public static int getIndOfDir(string name)
        {
            int i = 0;
            bool findDir = false;
            while (!findDir && i < MainProgram.fs.files.Count)
            {
                if (MainProgram.fs.files[i].Parent_dir == curDir)
                {
                    if (MainProgram.fs.files[i].Name == name && MainProgram.fs.files[i].First_clust == 0) { findDir = true; }
                    else i++;
                }
                else i++;
            }
            if (findDir) return i;
            else return -1;
        }  

        public static void createRootDir()
        {
            string dt = "01.01.2015 01:01:01";
            myFile f = new myFile(0, "ROOT", "",
                        dt.ToString(), 0, 0, "rw", -1, "SYS");
            MainProgram.fs.files.Add(f);
        }

        private void clean_Click(object sender, EventArgs e)
        {
            output.Text = path + ">";
            command.Focus(); 
        }

        public bool checkUnicName(string name, bool isFile)
        { 
            bool pr = true;
            int i = 0, j = 0;
            int col = getIndOfCurFile();
           
            while (pr && i < MainProgram.fs.files[col].Size)
            {

                if (MainProgram.fs.files[j].Parent_dir == curDir)
                {
                    if ((MainProgram.fs.files[j].First_clust != 0 && isFile) ||   // both file
                    (MainProgram.fs.files[j].First_clust == 0 && !isFile))        // both dir
                    {
                        if (MainProgram.fs.files[j].Name == name) pr = false;
                        else i++;
                    }
                    else i++;
                }
                j++;
            }
            return pr;
        }

        public bool isUnikInNewDir(int fId) // in curDir
        {
            if (MainProgram.fs.files[getIndByID(fId)].First_clust == 0)  // dir is replaced
            { return checkUnicName(MainProgram.fs.files[getIndByID(fId)].Name, false); }
            else // file is replaced
            { return checkUnicName(MainProgram.fs.files[getIndByID(fId)].Name, true); }
        }

        public void createFile(string name, string rights)
        {
            if (MainProgram.fs.files.Count < MaxCountOfFiles)
            {
                DateTime dt = new DateTime();
                dt = DateTime.Now;
                // get first cluster
                ushort f_cl_ind = getFreeClust(true);
                if (f_cl_ind > 0)
                {
                    myFile f = new myFile(myFile.generateID(), name, "txt",
                                dt.ToString(), f_cl_ind, 0, rights, curDir, MainProgram.currentuser.getLogin());
                    f.First_clust = f_cl_ind;
                    // set value to listCl
                    MainProgram.fs.listCl[f_cl_ind] = 500;
                    MainProgram.fs.files.Add(f);
                    MainProgram.fs.files[getIndOfCurFile()].Size++;
                    output.Text += "Файл успешно создан" + Environment.NewLine + path + ">";
                }
                else { output.Text += "Нет свободных кластеров" + Environment.NewLine + path + ">"; }
            }
            else { output.Text += "Превышено допустимое количество файлов/папок" + Environment.NewLine + path + ">"; }
        }

        public static ushort getFreeClust(bool f)
        {
            for (ushort i = 41; i < MainProgram.fs.listCl.Length; i++)
            {
                if (MainProgram.fs.listCl[i] == 0)
                {
                 //   if (f) clearCluster(i);
                    return i;
                }
            }
            return 0;
        }
        public static void clearCluster(int iCl)
        {
            BinaryWriter bw = new BinaryWriter(File.Open("MyFS.dat", FileMode.Open));
            bw.BaseStream.Seek((iCl - 1) * FSEmul.CLUST_SIZE, SeekOrigin.Begin);
            for (int i = 0; i < FSEmul.CLUST_SIZE; i++)
            {
                bw.Write(' ');
            }

            bw.Close();
        }

        public void deleteFile(int fInd)
        {
            int ParID = MainProgram.fs.files[fInd].Parent_dir;
            MainProgram.fs.files[getIndByID(ParID)].Size--; // if root...
            (new OpenFile()).clearFile(fInd);
            MainProgram.fs.listCl[MainProgram.fs.files[fInd].First_clust] = 0;
            MainProgram.fs.files.Remove(MainProgram.fs.files[fInd]);
        }

        public void deleteDir(int id)
        {
            int[] ch = getChildFiles(id);  // id of files/dirs in cur_dir
            for (int i = 0; i < ch.Length; i++)
            {
                if (MainProgram.fs.files[getIndByID(ch[i])].First_clust == 0)  // this is dir
                {
                    deleteDir(ch[i]);
                }
                else deleteFile(getIndByID(ch[i]));
            }
            // delete parent-dir
            int ParID = MainProgram.fs.files[getIndByID(id)].Parent_dir;
            if (ParID!=-1) MainProgram.fs.files[getIndByID(ParID)].Size--;
            MainProgram.fs.files.Remove(MainProgram.fs.files[getIndByID(id)]);
        }

        public int[] getChildFiles(int id)
        {
            int[] ch = new int[MainProgram.fs.files[getIndByID(id)].Size];
            int j = 0;
            for (int i = 0; i < MainProgram.fs.files.Count; i++)
            {
                if (MainProgram.fs.files[i].Parent_dir == id)
                { ch[j] = MainProgram.fs.files[i].id; j++; }
            }
            return ch;
        }

        public static int getIndByID(int fID)
        {
            int i = 0;
            bool findCurDir = false;
            while (!findCurDir)
            {
                if (MainProgram.fs.files[i].id == fID) { findCurDir = true; }
                else i++;
            }
            return i;
        }

        public void changeName(int fInd, string newName)
        {
            MainProgram.fs.files[fInd].Name = newName;
        }

        //check if destination-dir for inserting file/dir is not located inside resource dir
        public bool checkDirIncapsul(int srcId, int rcvId) // source-DirId, receiverDir-ID
        {
            int curID = rcvId; // MainProgram.fs.files[getIndByID(rcvId)].
            while (curID != srcId && curID!=0)
            {
                curID = MainProgram.fs.files[getIndByID(curID)].Parent_dir;
            }
            if (curID == srcId) return false;
            else return true;
        }

        public static int getFreeClustCount()
        {
            int count = 0;
            for (int i = 41; i < MainProgram.fs.listCl.Length; i++)
            {
                if (MainProgram.fs.listCl[i] == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
