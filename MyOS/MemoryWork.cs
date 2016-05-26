using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyOS
{
    class MemoryWork
    {
        public const int PAGE_SIZE = 512;
        public const int PAGE_COUNT = 100;

        public static List<MemoryPage> mem = new List<MemoryPage>(); // memory
        Thread myThread;

        public void startProc(object PageIDOb) // запускаем процессы и выделяем им выделенную страницу № PageID
        {
            int PageID = Convert.ToInt32(PageIDOb);
            for (int i = 1; i <= MemoryTest.pr_count; i++)
            {
                myThread = new Thread(new ParameterizedThreadStart(mem[PageID - 1].write));
                myThread.Name = "Поток " + i.ToString();
                myThread.Start(myThread.Name.ToString()+" работает со страницей "+PageID.ToString());
                MainProgram.memt.tbl[1, i - 1].Value = "Ожидает доступ к странице " + PageID;
                Thread.Sleep(500);
            }
        }

        public MemoryWork() // create pages 
        {
            for (int i = 0; i < PAGE_COUNT; i++)
            {
                MemoryPage p = new MemoryPage(i+1); 
                mem.Add(p);
            }
        }
    }
}
