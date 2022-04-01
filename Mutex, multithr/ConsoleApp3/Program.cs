using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace ConsoleApp3
{
    class Program
    { 
        //static Mutex zamek = new Mutex();
        //static char[] Bufor = new char[7];
        //static int indeks = 0;
        //static void Pisarz(char znak, int pauza)
        //{
        //    while(true)
        //    {
        //        zamek.WaitOne();
        //            if (indeks < Bufor.Length)
        //            {
        //                Bufor[indeks++] = znak;
        //            }    
        //        zamek.ReleaseMutex();
        //        Thread.Sleep(pauza);
        //    }
        //}
        //static void Czytelnik()
        //{
        //    while (true)
        //    {
        //        zamek.WaitOne();
        //        if (indeks > 0)
        //        {
        //            Console.Write(Bufor[--indeks]);
        //        }
        //        zamek.ReleaseMutex();
        //    }   
        //}
        static void Main(string[] args)
        {
            using (Mutex zamek = new Mutex(false, "Global\\CA.Test.727"))
            {
                if (!zamek.WaitOne(8000))
                {
                    Console.WriteLine("Ej");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine("Robię");
                    for (int i = 0; i < 5; i++)
                    {
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                    }
                zamek.ReleaseMutex();
                
            }
            //int t = int.Parse(Console.ReadLine());
            //Thread c1 = new Thread(Czytelnik);
            //Thread p1 = new Thread(() => Pisarz('A', t));
            //Thread p2 = new Thread(() => Pisarz('B', t));
            //c1.Start();
            //p1.Start();
            //p2.Start();
        }

    }
}

