// Cristina Muñoz Muñoz
// Aroa Yubero Sevilla
using System;
using System.IO;
using Listas;
namespace Practica2
{
    class Program
    {
        static void Main()
        {
            Map map = new Map();
            ReadInventory("CrowtherItems.txt",map);
            ReadRooms("CrowtherRooms.txt",map);
            map.SetItemsRooms();
            map.WriteMap();
        }
        static void ReadInventory(string file, Map map)// los almacena en map con AddItemRoom dice el enunciado, pero tiene mas sentido AddItem
        {
            StreamReader streamReader = new StreamReader(file);
            while (!streamReader.EndOfStream)
            {
               string item = streamReader.ReadLine();
               string descr = streamReader.ReadLine();
               int room = int.Parse(streamReader.ReadLine());
               streamReader.ReadLine();
               map.AddItem(item,descr,room); //item es string y pide int
            }
            streamReader.Close();
        }
        static void ReadRooms(string file,Map map)
        {
            StreamReader f = new StreamReader(file);
            while (!f.EndOfStream)
            {
                 int numRoom = int.Parse(f.ReadLine());
                 ReadRoom(f, numRoom, map);
            }
            f.Close();
        }
        static void ReadRoom (StreamReader f, int n,Map map)
        {
            string name = f.ReadLine();
            string descr = f.ReadLine();
            map.AddRoom(n, name, descr);
            f.ReadLine();
            string s = f.ReadLine();
            while (s != "" && !f.EndOfStream)//espacio en blanco
            {
                string[] v = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string direction = v[0];
                string[] aux = v[1].Split('/');
                string nextRoom = aux[0];
                string item = "";
                if (aux.Length>1)
                {
                    item = aux[1];
                }
                s = f.ReadLine();
                int destRoom = int.Parse(nextRoom);
                map.AddRouteRoom(n, direction, destRoom, item);
            }
        }
    }
}