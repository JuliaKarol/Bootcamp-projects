using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace xml
{
    class Program
    {
        static void Main(string[] args)
        {
            XElement ksiazka =
                  new XElement("Ksiazka",
                  new XElement("Wpis",
                      new XElement("Nazwa", "Kowalski"),
                      new XElement("Miasto", "Katowice"),
                      new XElement("Telefony",
                          new XElement("Telefon", "465-789-123", new XAttribute("Typ", "dom")),
                          new XElement("Telefon", "231-782-983", new XAttribute("Typ", "praca"))
                      )
                  ),
                  new XElement(new XElement("Ksiazka",
                  new XElement("Wpis",
                      new XElement("Nazwa", "Nowak"),
                      new XElement("Miasto", "Kraków"),
                      new XElement("Telefony",
                          new XElement("Telefon", "753-736-649", new XAttribute("Typ", "dom")),
                          new XElement("Telefon", "532-305-901", new XAttribute("Typ", "praca")),
                          new XElement("Telefon", "601-491-572", new XAttribute("Typ", "praca")))
                          )
                      )
                  ));
                          
            ksiazka.Save("ksiazka.xml");

            //var temp = from x in ksiazka.Elements("Wpis") select ksiazka.Element("Nazwa").Value;
           // var temp1 = ksiazka.Elements("Wpis").Select(x=>ksiazka.Element("Nazwa").Value);
            //var temp2 = ksiazka.Elements("Wpis").First(x => ksiazka.Element("Nazwa").Value == "Nowak").Element("Telefony").Elements("Telefon").Select(y => ksiazka.Attribute("Typ").Value);
            var temp3 = ksiazka.Elements("Wpis").SelectMany(x => ksiazka.Element("Telefony").Elements("Telefon").Select(x=> ksiazka.Element("Miasto").Value));
            var temp4 = from a in ksiazka.Elements("Wpis")
                        from b in ksiazka.Element("Telefony").Elements("Telefon")
                        select new
                        {
                            Nazwa = b.Element("Nazwa")
                        };
            var temp5 = from z in ksiazka.Elements("Wpis").SelectMany(x => ksiazka.Element("Telefony").Elements("Telefon"))
                        select new
                        {
                           Nazwa = z.Element("Nazwa").Value
                        };
            XElement kowalski = ksiazka.Elements("Wpis").First(x => x.Element("Nazwa").Value == "Kowalski");
            kowalski.Element("Masto").Value = "Warszawa";
            var kowalskiDomowy = kowalski.Element("Telefony").Elements("Telefon").Where(x=> x.Attribute("Typ").Value =="dom");
            foreach (var item in kowalskiDomowy)
            {
                item.Remove();
            }
            kowalski.Element("Telefony").Add(new XElement("Telefon", "903403-2429", new XAttribute("Typ", "praca")));

            //  foreach (var x in temp3)
            //      Console.WriteLine(x);
            Console.ReadLine();
        }
    }
}
