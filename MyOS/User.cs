using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace MyOS
{
    public class User
    {
        private string login;
        public bool isAdmin;
        private string password;

        public User(string slogin, bool sisAdmin, string spassword)
        {
            login = slogin;
            isAdmin = sisAdmin;
            password = spassword;
        }
        public User() { }
        ~User() { }

        public string getLogin() { return login; }
        public string getPass() { return password; }

        public void setLogin(string log) { login = log; }
        public void setPass(string pass) { password = pass; }

        public static string getHash(string s)
        {  
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);  
  
            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =  new MD5CryptoServiceProvider();  
          
            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);  
  
            string hash = string.Empty;  
  
            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)  
                hash += string.Format("{0:x2}", b);  
            return hash;  
        }

        public static void deleteUser(string login) // command 'delete login'
        {
            if (login == "admin")
            {
                MessageBox.Show("Невозможно удалить администратора");
            }
            else if (MainProgram.currentuser.getLogin() == login || MainProgram.currentuser.getLogin() == "admin")
            {
                bool isFound = false;
                int i = 0;
                while (!isFound && i < MainProgram.users.Count)
                    if (MainProgram.users[i].getLogin() == login) isFound = true;
                    else i++;
                if (isFound) 
                {
                    MainProgram.users.Remove(MainProgram.users[i]);
                    MessageBox.Show("Пользователь '"+login+"' был успешно удален");
                    if (MainProgram.currentuser.getLogin() == login) MainProgram.resetCurrentuser();
                }
                else MessageBox.Show("Удаление невозможно: такого пользователя не существует");
            }
            else MessageBox.Show("У вас не достаточно прав");
        }

    }
}
