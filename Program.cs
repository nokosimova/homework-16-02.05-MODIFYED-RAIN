using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic; 

namespace MRain
{
    class Program
    {
        static int height = 15;
        static char[] SingleColumn = new char[height];
        static int InitRow;
        static int InitColumn;
        static object locker = new object(), locker1 = new object();
      //  static bool currentstatus = true;
        static void Main(string[] args)
        {
            Random randomheight = new Random();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            CleanColumn();
            InitColumn = Console.CursorLeft;
            InitRow = Console.CursorTop;
            Task[] tasks = new Task[2];

            for (int j = 0; j < tasks.Length; j++)
            {
                object k = 2*j;
                int letterstreamheight = randomheight.Next(4, 7); //длина потока букв
                tasks[j] = Task.Factory.StartNew(() =>{ 
                    while (true)
                    { 
                        TopStream(k,5,0,(int)height/2-1);
                       // while (currentstatus)  
                       // TopStreamAsync(k,letterstreamheight,0,(int)height / 2);
                       // currentstatus = true;
                       // TopStreamAsyncAsync(k,letterstreamheight,(int)height / 2, height);

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
        public static void CleanColumn(int b, int e)
        {
            for (int i = b; i <= e; i++)
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
            Thread.Sleep(1000);   
        }
        //вывод цепочки({переписывать лист + показывать лист} - н раз)
        public static void TopStream(object column, int letterstreamheight, int beginpos,int endpos)
        {
            Random randomletter = new Random();
            Random randomheight = new Random();
            Point point = new Point(0,0);
            int i, firstletterpos = 0, lastletterpos = 0;
            point.col = (int)column;
            
            for (i = beginpos; i <= endpos; i++)
            {
                int last = 0, prelast = 0;
                lock(locker) {
                    firstletterpos = (i <= letterstreamheight) ? beginpos : i - letterstreamheight;
                    lastletterpos = (i <= endpos)? i : endpos;
                    for (int letterpos = firstletterpos; letterpos < lastletterpos; letterpos++)
                    {         
                        char letter = Convert.ToChar(randomletter.Next(65,90));
                        SingleColumn[point.row + letterpos] = letter; 
                    }
                prelast = point.row + lastletterpos - 2;
                last = point.row + lastletterpos - 1;
                WriteList((int) column, prelast, last);
                CleanColumn(beginpos, endpos);
                }
            }
        }
      /*  public static async void TopStreamAsync(object column, int letterstreamheight, int start, int finish)
        {
            await Task.Run(() => 
            {
            Random randomletter = new Random();
            Random randomheight = new Random();
            Point point = new Point(0,0);
            int i, firstletterpos, lastletterpos;
            point.col = (int)column;
      

            for (i = start; i <= finish+letterstreamheight; i++)
            {
               int last = 0, prelast = 0;
                lock(locker) {
                firstletterpos = (i <= letterstreamheight) ? start : i - letterstreamheight;
                lastletterpos = (i <= finish)? i : finish;
                    for (int letterpos = firstletterpos; letterpos < lastletterpos; letterpos++)
    
                    {       
    
                        char letter = Convert.ToChar(randomletter.Next(65,90));
                        SingleColumn[point.row + letterpos] = letter; 
                    }
                prelast = point.row + lastletterpos - 2;
                last = point.row + lastletterpos - 1;
                WriteList(
                    (int) column, prelast, last);
                if (i =
                = finish) DownStreamAsync(column, letterstreamheight, finish, height);
                CleanColumn(start, finish);
                }
            } });
        }
        //ассинхронный метод для вывода оставшейся цепочки
         public static async void DownStreamAsync(object column, int letterstreamheight, int start, int finish)
        {
            await Task.Run(() => 
            {
            Random randomletter = new Random();
            Random randomheight = new Random();
            Point point = new Point(0,0);
            int i, firstletterpos, lastletterpos;
            point.col = (int)column;
      

            for (i = start; i <= finish + letterstreamheight; i++)
            {
                int last = 0, prelast = 0;
                lock(locker) {
                firstletterpos = (i <= letterstreamheight) ? start : i - letterstreamheight;
                lastletterpos = (i <= finish)? i : finish;
                    for (int letterpos = firstletterpos; letterpos < lastletterpos; letterpos++)
    
                    {       
    
                        char letter = Convert.ToChar(randomletter.Next(65,90));
                        SingleColumn[point.row + letterpos] = letter; 
                    }
                prelast = point.row + lastletterpos - 2;
                last = point.row + lastletterpos - 1;
                WriteList(
                    (int) column, prelast, last);
                CleanCo
                lumn(start, finish);
                }
            }
            });
        }*/    
    }
}

