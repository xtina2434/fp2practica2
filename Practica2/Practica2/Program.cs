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
            
            int actualRoom = 1;
            string infoFirstRoom = map.GetInfoRoom(actualRoom);
            Console.WriteLine(infoFirstRoom+ "\n");
            while(actualRoom != 0)
            {
                //bucle, El jugador comenzará en la habitación 1 con el inventario vacío y terminará cuando alcance la habitación 0
                Console.Write("> ");
                string input = Console.ReadLine();
                ProcessCommand(map, input, ref actualRoom, inventory);
            }
        }
        static void ReadInventory(string file, Map map)
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
        static void ProcessCommand(Map map, string input, ref int playerRoom, List inventory)
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
                    + "Directions: IN, OUT, NORTH, SOUTH, WEST, EAST, UP, DOWN, XYZZY, PLUGH, WAVE, SWIM\n");

            }
            else if (words[0] == "INVENTORY") 
            {
               string inventoryInfo =  map.GetItemsInfo(inventory);
               Console.WriteLine(inventoryInfo);
            }
            else if (words[0] == "LOOK")
            {
                string infoRoom = map.GetInfoRoom(playerRoom);
                Console.WriteLine(infoRoom+ "\n" );
            }
            else if (words[0] == "TAKE")
            {
                if (!map.TakeItemRoom(playerRoom, words[1], inventory))
                {
                    Console.WriteLine("You can´t do this action.\n");
                }
                else
                {
                    map.TakeItemRoom(playerRoom, words[1], inventory);
                    Console.WriteLine("Item " + words[1] + " taken. \n");
                }
            }
            else if (words[0] == "DROP")
            {
                if (!map.DropItemRoom(playerRoom, words[1], inventory))
                {
                    Console.WriteLine("You can´t do this action.");
                }
                else
                {
                    map.DropItemRoom(playerRoom, words[1], inventory);
                    Console.WriteLine("Item " + words[1] + " dropped");
                }
            }
            else
            {
                List roomsVisited = map.Move(playerRoom, words[0], inventory);
                string infoRoomsVisited = map.InfoRoomsVisited(roomsVisited, playerRoom, words[0], inventory);
                Console.WriteLine(infoRoomsVisited);
                int[] auxRooms = roomsVisited.ToArray();
                playerRoom = auxRooms[auxRooms.Length - 1];
            }
        }
    }
}