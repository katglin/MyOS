using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MyOS
{
    public class ProcessControll
    {
        public static volatile List<Process> listProc = new List<Process>(); // для вывода в таблицу

        public void run()
        {
            Process curPr = new Process();
            const int KWANT_PIECE = 300; 
            int kwant;
            int newTime;
            Thread.Sleep(1000);
            while (true)
            {
              showControll();
              Thread.Sleep(500);    // Thread.Sleep(kwant*20);
              for (int j = 0; j < listProc.Count; j++)
              {
                  if (MainProgram.qPr.Count > 0)  //Process.prCount > 0
                  {
                      curPr = MainProgram.qPr.Peek();
                      if (!curPr.pause)
                      {
                          if (curPr.getPrior() < 1) curPr.setPrior(1); // --
                          kwant = KWANT_PIECE / curPr.getPrior();
                          newTime = curPr.getTime() - kwant;
                          curPr.setTime(newTime);
                          listProc[findProcess(curPr.getName())].setTime(newTime); // обновляем выводимые данные

                          if (curPr.getTime() <= 0)
                          {
                              MainProgram.qPr.Dequeue();
                              listProc.Remove(listProc[findProcess(curPr.getName())]);
                          }
                          else
                          {
                              while (curPr.getPrior() > 1 &&
                                     curPr.getTime() - 2 * KWANT_PIECE / curPr.getPrior() <= 0 &&
                                     curPr.getTime() >= KWANT_PIECE / curPr.getPrior())
                                  curPr.setPrior(curPr.getPrior() - 1); //динамичкски изменяем приоритет
                              listProc[findProcess(curPr.getName())].setPrior(curPr.getPrior());  // обновляем выводимые данные
                              MainProgram.qPr.Dequeue();
                              MainProgram.qPr.Enqueue(curPr);
                          }
                      }
                      else
                      {
                          MainProgram.qPr.Dequeue();
                          MainProgram.qPr.Enqueue(curPr);
                      }
                  }
              }
            }
        }

        public static int findProcess(string name) // поиск процесса в listProc
        {
            int i = 0;
            bool pr = false;
            while (!pr && i < listProc.Count)
                if (listProc[i].getName() == name) return i;
                else i++;
            return -1; // процесс не запущен
        }

        public static void showControll()
        {
      //      Thread.Sleep(100);
            MainProgram.addPr.Invoke(MainProgram.addPr.delegClear);
            Process tempPr = new Process();
            for (int i = 0; i < listProc.Count; i++)
            {
                tempPr = listProc[i];
                MainProgram.addPr.AllPr[0,i].Value = tempPr.getName();
                MainProgram.addPr.AllPr[1, i].Value = tempPr.getPrior().ToString();
                MainProgram.addPr.AllPr[2, i].Value = tempPr.getTime().ToString();
                MainProgram.addPr.AllPr[3, i].Value = tempPr.getUserID();
            }
        }
    }
}
