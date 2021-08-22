using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Laba_4
{
    class MainClass
    {
        static void Main(string[] args)
        {
            List<Price> price = new List<Price>();
            Console.Write("Оберіть дію:" +
                 "\n1 - Записати нові дані у файли." +
                 "\n2 - Вивести збережені дані з файлів." +
                 "\nВведіть ваш вибір: ");
            int ReadChar = int.Parse(Console.ReadLine());

            if (ReadChar == 1)
            {
                Console.WriteLine("Введіть дані за зразком\n" +
                    "Кола, АТБ, 1950");
                price = Input();
                SaveData(price);
                Console.WriteLine("\nДані забережено");
            }
            else if (ReadChar == 2)
            {
                Console.Write("Оберіть тип файлу:" +
                "\n1 - TXT." +
                "\n2 - XML." +
                "\nВведіть ваш вибір: ");
                int TXTorXML = int.Parse(Console.ReadLine());

                if (TXTorXML == 1)
                {
                    price = ReadTXT();
                }
                else if (TXTorXML == 2)
                {
                    price = ReadXML();
                }

                SaveData(price);
            }
            price.Sort();
            Console.WriteLine("Введіть назву продукту який вас цікавить");
            string name = Console.ReadLine();

            for (int i = 0; i < price.Count; i++)
                if (name == price[i].nameOfProduct)
                {
                    OutputData(price[i]);
                }
        }
        public static List<Price> Input()
        {
            List<Price> data = new List<Price>();
            string input = "";


            while (true)
            {
                Console.Write("Введіть дані: ");
                input = Console.ReadLine();
                if (input == "")
                    break;
                data.Add(new Price(input));
            }

            return data;
        }
        public static void SaveData(List<Price> price)
        {
            using (StreamWriter save = new StreamWriter("Price_TXT.txt"))
            {
                foreach (Price data in price)
                    save.WriteLine(data);
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Price>));

            using FileStream fs = new FileStream("Price_XML.xml", FileMode.Create);
            formatter.Serialize(fs, price);
        }
        public static List<Price> ReadTXT()
        {
            List<Price> price = new List<Price>();
            string[] dataFromFile = File.ReadAllLines("Price_TXT.txt");
            for (int i = 0; i < dataFromFile.Length; i++)
                price.Add(new Price(dataFromFile[i]));

            return price;
        }

        public static List<Price> ReadXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Price>));
            List<Price> price = new List<Price>();

            using (FileStream fs = new FileStream("Price_XML.xml", FileMode.OpenOrCreate))
            {
                price = (List<Price>)formatter.Deserialize(fs);
            }

            return price;
        }
        public static string MoneyFormatter(int Amount)
        {
            int cop = Amount % 100;
            int hrn = (Amount - cop) / 100;
            string HrnCop = hrn + " грн. " + cop + " коп.";
            return HrnCop;
        }
        public static void OutputData(Price price)
        {
            Console.WriteLine("Назва товару {0}\n" +
                "Назва магазину {1}\n" +
                "Вартість товару {2}\n",
                 price.nameOfProduct, price.nameOfShop, MoneyFormatter(price.Amount));
        }
    }
}


