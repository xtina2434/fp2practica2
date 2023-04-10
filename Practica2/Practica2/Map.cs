using System;
using System.Globalization;
using System.IO;
using Listas;

namespace Practica2
{
    class Map
    {
        struct Item // información de cada ítem
        {
            public string name, description; // como aparecen en el archivo CrowtherItems
            public int initialRoom;          // índice de la habitación donde está al principio del juego
        }
        Room[] rooms;                        // array de habitaciones indexadas con la numeración de CrowtherRooms
        int nRooms;                          // número de habitaciones = índice a la primera posición libre en rooms
        Item[] items;                        // array de items en el juego indexados por orden de aparición en el archivo
        int nItems;                          // número de ítems = índice la primera posición libre en items
        int maxRoutes;                       // número máximo de rutas por habitación
        public Map(int maxRooms = 100, int maxRts = 10, int maxItems = 20)
        {
            rooms = new Room[maxRooms];
            nRooms = 0;//con 0 habitaciones?
            items = new Item[maxItems];
            nItems = 0;//0 ítems?
            maxRoutes = maxRts;
        }
        public void AddItem(string name, string description, int iniRoom)
        {
            items[nItems].name = name;
            items[nItems].description = description;
            items[nItems].initialRoom = iniRoom;
            nItems++;
        }
        private int GetItemIndex(string name)
        {
            bool enc = false;
            int i = 0;
            while(i<items.Length && !enc)
            {
                if (items[i].name == name) enc = true;
                else i++;
            }
            if (enc) return i;
            else return -1;
        }
         public void AddRoom(int nRoom, string name, string description)//rooms[nRoom] o rooms[nRooms]??
        {
             nRooms++; 
             rooms[nRoom] = new Room(name, description,maxRoutes);
         }
        public void AddRouteRoom(int nRoom, string dir, int destRoom, string condItem)
        {
            int item = GetItemIndex(condItem);
            rooms[nRoom].AddRoute(dir, destRoom, item);
        }
        public void AddItemRoom(int nRoom, int itemId)
        {
            rooms[nRoom].AddItem(itemId);
            nItems++;
        }
        public string GetInfoRoom(int nRoom)
        {
            string info = rooms[nRoom].GetInfo();
            return info;
        }
        /*public string GetItemsRoom(int nRoom)
        {
            
        }*/
        public void SetItemsRooms()
        {
            for(int i = 0; i < items.Length; i++)
            {
                int pos = GetItemIndex(items[i].name);//?
                AddItemRoom(items[i].initialRoom, pos);
            }
        }
        public void WriteMap()//
        {
            for(int i=1; i<rooms.Length; i++)
            {
               string infoRoom = GetInfoRoom(i);
               Console.WriteLine(infoRoom);
                //descripcion direc
                //GetItemsRoom
            }
        }
        /*public bool TakeItemRoom(int nRoom, string itemName, List inventory)
        {

        }
        public bool DropItemRoom(int nRoom, string itemName, List inventory)
        {

        }
        public List Move(int nRoom, string dir, List inventory)
        {

        }
        public string GetItemsInfo(List inventory)
        {

        }*/
    }
}
