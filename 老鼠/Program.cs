using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 老鼠
{
    class Program
    {
        static void Main(string[] args)
        {
            //现有1000瓶药，其中有一瓶毒药，喝了之后1小时后才产生效果，
            //现在你有10只老鼠和1个小时的时间，请问怎么找出毒药？

            int[] mouse =  {1,2,3,4,5,6,7,8,9,10 };
            int[] drug = new int[1000] ;
            for (int i = 0; i < mouse.Length; i++)
            {
                mouse[i] = i+1;
                
            }
            Console.WriteLine(mouse[999]);
            //1 1-500
            //3 1-125
            //5 126-250
            //7 251-375
            //9 376-500

            //2 501 - 1000
            //4 501-625
            //6 626-750
            //8 751-875
            //10 876-1000
        }
    }
}
