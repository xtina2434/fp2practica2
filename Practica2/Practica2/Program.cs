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
           // map.WriteMap();
            List inventory = new List();

            //bucle, El jugador comenzará en la habitación 1 con el inventario vacío y terminará cuando alcance la habitación 0
            Console.Write(">");
            string input = Console.ReadLine();
            ProcessCommand(map, input,1, inventory);//

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
        static void ProcessCommand(Map map, string input, int playerRoom, List inventory)
        {
            string[] words = input.Trim().ToUpper().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (words[0] == "HELP")
            {
                Console.Write("Comandos: \n" +
                    "-INVENTORY\n" +
                    "-LOOK\n" +
                    "-TAKE <item>\n" +
                    "-DROP <item>\n" +
                    "Items: KEYS, LAMP, ROD, BIRD, NUGGET, DIAMOND, COINS, EMERALD, EGGS, WATER, PLANT, CHEST.\n"
                    + "Directions: IN, OUT, NORTH, SOUTH, WEST, EAST, UP, DOWN, XYZZY, PLUGH, WAVE, SWIM");
                
            } 
            else if (words[0] == "INVENTORY") map.GetItemsInfo(inventory);
            else if(words[0] == "LOOK") map.GetInfoRoom(playerRoom);
            else if(words[0] == "TAKE" && words[1]=="<item>")//revisar words[1]=="<item>"
            {
                if(!map.TakeItemRoom(playerRoom, words[1] , inventory)) 
                {
                    Console.WriteLine("You can´t do this action.");
                } 
                else map.TakeItemRoom(playerRoom, words[1], inventory); 
            }
            else if(words[0] == "DROP" && words[1] == "<item>") //revisar words[1]=="<item>"
            {
                if (!map.DropItemRoom(playerRoom, words[1], inventory))
                {
                    Console.WriteLine("You can´t do this action.");
                }
                else map.DropItemRoom(playerRoom, words[1], inventory);
            }
            else
            {
                map.Move(playerRoom, words[0], inventory);
            }
            
        }
    }
}