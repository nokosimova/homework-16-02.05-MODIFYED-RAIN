using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; 

namespace MRain
{
    class Program
    {
        static int height = 14;
        static char[] SingleColumn = new char[height];
        static int InitRow;
        static int InitColumn;
        static object locker = new object(), locker1 = new object();
        static int currectpos = 0;
        static void Main(string[] args)
        {
            Random randomheight = new Random();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            CleanColumn();
            InitColumn = Console.CursorLeft;
            InitRow = Console.CursorTop;
            Task[] tasks = new Task[10];
            for (int j = 0; j < tasks.Length; j++)
            {
                object k = 2*j;
                 int letterstreamheight = randomheight.Next(5,8); //длина потока букв
                tasks[j] = Task.Factory.StartNew(() =>{ 
                    while (true)
                    {   
                        WriteStream(k,letterstreamheight,0,(int)height / 2-2);
                        WriteStreamAsync(k,letterstreamheight,(int)height / 2-2, height);

                    }
                });
            }
            Console.ReadLine();
        }
        public static void CleanColumn()
        {
            for (int i = 0; i < SingleColumn.Length; i++)
                SingleColumn[i] = ' ';
        }

        //вывод символа:
        public static void WriteAt(char letter, object p)
        {
            Point point = p as Point;
                Console.SetCursorPosition(InitColumn + point.col, InitRow + point.row);
                Console.Write(letter);

           
        }
        //вывод столбца
        public static void WriteList(int column, int prelast, int last)
        {
            for (int i = 0; i < SingleColumn.Length; i++)
            {
                if (i == last) Console.ForegroundColor = ConsoleColor.Blue;
                else if (i == prelast) Console.ForegroundColor = ConsoleColor.Green;
                else Console.ForegroundColor = ConsoleColor.DarkGreen;
                Point point = new Point(column,i);
                WriteAt(SingleColumn[i], point);
            }
            Thread.Sleep(50);   
        }
        //вывод цепочки({переписывать лист + показывать лист} - н раз)
        public static void WriteStream(object column, int letterstreamheight, int start, int finish)
        {
            Random randomletter = new Random();
            Random randomheight = new Random();
            Point point = new Point(0,0);
            int i, beginpos, endpos;
            point.col = (int)column;
      
            for (i = start; i < finish; i++)
            {
                currectpos = i;
                int last = 0, prelast = 0;
                lock(locker) {
                beginpos = (i <= letterstreamheight) ? start : i - letterstreamheight;
                endpos = (i <= finish)? i : finish;
                    for (int letterpos = beginpos; letterpos < endpos; letterpos++)
                    {       
                        char letter = Convert.ToChar(randomletter.Next(65,90));
                        SingleColumn[point.row + letterpos] = letter; 
                    }
                prelast = point.row + endpos - 2;
                last = point.row + endpos - 1;
                WriteList((int) column, prelast, last);
                CleanColumn();
                }
            }
        }
        //ассинхронный метод для вывода оставшейся цепочки
         public static async void WriteStreamAsync(object column, int letterstreamheight, int start, int finish)
        {
            await Task.Run(() => 
            {
                Random randomletter = new Random();
            Random randomheight = new Random();
            Point point = new Point(0,0);
            int i, beginpos, endpos;
            point.col = (int)column;
      
            for (i = start; i <= finish + letterstreamheight; i++)
            {
                currectpos = i;
                int last = 0, prelast = 0;
                lock(locker) {
                beginpos = (i <= letterstreamheight) ? start : i - letterstreamheight;
                endpos = (i <= finish)? i : finish;
                    for (int letterpos = beginpos; letterpos < endpos; letterpos++)
                    {       
                        char letter = Convert.ToChar(randomletter.Next(65,90));
                        SingleColumn[point.row + letterpos] = letter; 
                    }
                prelast = point.row + endpos - 2;
                last = point.row + endpos - 1;
                WriteList((int) column, prelast, last);
                CleanColumn();
                }
            }
            });
        }
    }
}

