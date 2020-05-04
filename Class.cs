using System;
using System.Threading;
using System.Threading.Tasks;
namespace MRain
{
    public  class Point
    {
        public  int col{get; set;}
        public  int row{get; set;}
        public Point(int c, int r)
        {
            col = c;
            row = r; 
        }
    }
}