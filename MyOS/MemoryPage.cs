using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyOS
{
    public class MemoryPage
    {
        int id;  // page number
        byte[] txt;  // page content
        Semaphore semaphore;  //locker

        public MemoryPage() { }
        public MemoryPage(int id)
        {
            this.id = id;
            txt = new byte[MemoryWork.PAGE_SIZE];
            semaphore = new Semaphore(1, 1);
        }

        public void write(object s)
        {
            string str = s.ToString();
            string[] strThreadNameID = Thread.CurrentThread.Name.ToString().Split(' ');
            int ID = Convert.ToInt32(strThreadNameID[1]); // ID of current thread
                System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                semaphore.WaitOne(); // блокируем доступ к странице с помощью семаформа
                if (str.Length < MemoryWork.PAGE_SIZE)
                    txt = Encoding.UTF8.GetBytes(str);
                else txt = Encoding.UTF8.GetBytes(str.Substring(0, MemoryWork.PAGE_SIZE));
                lock (MainProgram.memt)
                {
                    MainProgram.memt.txt.Text += Thread.CurrentThread.Name +
                            " получает доступ к странице " + id.ToString() + Environment.NewLine;
                    MainProgram.memt.tPages[1, this.id-1].Value = "Занята потоком "+ID;
                    
                }
                Thread.Sleep(1000);
                lock (MainProgram.memt)
                {
                    MainProgram.memt.txt.Text += Encoding.UTF8.GetString(this.txt) + Environment.NewLine;
                    MainProgram.memt.tbl[1, ID - 1].Value = "Работает со страницей " + this.id;
                }
                Thread.Sleep(1000);
                lock (MainProgram.memt)
                {
                    MainProgram.memt.txt.Text += Thread.CurrentThread.Name +
                            " завершает работу со страницей " + id.ToString() + Environment.NewLine;
                    MainProgram.memt.tbl[1, ID - 1].Value = "Завершил работу";
                    MainProgram.memt.tPages[1, this.id-1].Value = "Свободна";
                }
                Thread.Sleep(1000);
                semaphore.Release(); // освобождаем страницу
        }

    }
}
