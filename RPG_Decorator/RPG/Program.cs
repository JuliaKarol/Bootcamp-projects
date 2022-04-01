using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public interface IAtak
    {
        int AddSiła();
    }
    public interface IObrona
    {
        int Redukcja(int Siła);
    }
    class Program
    {
        public class BrońBiała : IAtak
        {
            public int AddSiła()
            {
                return 5;
            }
        }

        class BrońDystansowa :IAtak
        {
            public int AddSiła()
            {
                return new Random().Next(1, 7);
            }
        }
        public class Dekorator : IObrona
        {
            IObrona  Komponent; 
            public Dekorator(IObrona k)
            {
                this.Komponent = k;
            }
            
           public virtual int Redukcja(int Siła)
            {
                return Komponent.Redukcja(Siła);
            }
             
        }
        abstract class Unik : Dekorator//klasa dekoratora
        {
            public Unik(IObrona k):base(k)
            {

            }
           public override int Redukcja(int Siła)
            {
                int Siła2 = new Random().Next(1, Siła+1);
                return base.Redukcja(Siła2);
            }
        }
        class Tarcza :Dekorator //klasa dekoratora
        {
            public Tarcza(IObrona k):base(k)
            {

            }
            public override int Redukcja(int Siła)
            {
            int Siła2 =(Siła / 2);
                return base.Redukcja(Siła2);
            }
        }
        class ZbrojaPlytowa : IObrona
        {
            public int Redukcja(int Siła)
            {
                return Math.Max(0, Siła - 2);    
            }
        }
        class ZbrojaKolczuga : IObrona
        {
            public int Redukcja(int Siła)
            {
                if (Siła<5)
                {
                    return Math.Max(0, Siła - 1);
                }
                else
                {
                    return Math.Max(0, Siła - 2);
                }
            }
        }
        class Rycerz
        {
            IAtak Broń;
            IObrona Obrona;
            public int HP=100;
            //zrobic pole prwatne zdrowie plus get/set
            public void JakaBroń(IAtak b)
            {
                Broń = b;
            }
            public void JakaObrona(IObrona o)
            {
                Obrona = o;
            }
            void BrońSię(int Siła)
            {
                HP -= Obrona.Redukcja(Siła);
            }
            public void Atak(Rycerz cel)
            {
                cel.BrońSię(Broń.AddSiła());
            }
            //public Rycerz(IAtak Broń, IObrona Obrona, int HP)
            //{
            //   Broń = this.Broń;
            //   Obrona = this.Obrona;
            //   HP = this.HP;
            //}
        }
        static void Main(string[] args)
        {
            Rycerz r = new Rycerz();
            r.JakaBroń(new BrońBiała());

            Rycerz q = new Rycerz();
            q.JakaBroń(new BrońDystansowa());
            IObrona def = new Tarcza(new ZbrojaKolczuga());

            r.Atak(q);
            Console.WriteLine(q.HP);
        }
    }
}
