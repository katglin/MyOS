using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace MyOS
{
    static class MainProgram
    {
        public static volatile Queue<Process> qPr; //очередь процессов 
        public static volatile AddProcess addPr = new AddProcess(); //форма
        public static volatile MemoryTest memt = new MemoryTest(); //форма
        public static Thread planir;
        public static List<User> users = new List<User>();
        public static User currentuser = new User("UnknownUser",false,"");

        public static FS fs; // file system
           [STAThread]
        public  static void Main()
        {
            Initial(); // считывание всех нужных данных перед началом работы    
            MainProgram.qPr = new Queue<Process>();
            planir = new Thread((new ProcessControll()).run);
            planir.Start();

            Application.Run(addPr);    
        }

        public static void getUsers() // считываем список существующих пользователей из файла
        {
            if (File.Exists("Users.sys"))  // файл с пользователями уже создан
            {
                StreamReader file = new StreamReader("Users.sys");
                int count = Convert.ToInt16(file.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    users.Add(new User());
                    users[i].setLogin(file.ReadLine());
                    users[i].isAdmin = Convert.ToBoolean(file.ReadLine());
                    users[i].setPass(file.ReadLine());
                }
                file.Close();
            }
            else // файл с пользователями не создан 
            {
                User adminUser = new User("admin", true, "19a2854144b63a8f7617a6f225019b12");
                  users.Add(adminUser);
            }
            
        }

        public static void Initial()
        {
            getUsers(); // считать список пользователей из файла Users.sys
            //...  FSEmul.FS_Start();
        }

        public static void Save()
        {
            StreamWriter file = new StreamWriter("Users.sys");
            file.WriteLine((users.Count).ToString());
            for (int i = 0; i < users.Count; i++)
            {
                file.WriteLine(users[i].getLogin());
                file.WriteLine(Convert.ToString(users[i].isAdmin)); 
                file.WriteLine(users[i].getPass()); 
            }
            file.Close();
        }

        public static void resetCurrentuser()
        {
            currentuser = new User("UnknownUser", false, "");
            addPr.UserStatus.Text = "Текущий пользователь:  " +
                                   MainProgram.currentuser.getLogin();
        }
    }
}
