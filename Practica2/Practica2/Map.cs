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
            nRooms = 0;
            items = new Item[maxItems];
            nItems = 0;
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
        public void AddRoom(int nRoom, string name, string description)
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
        }
        public string GetInfoRoom(int nRoom)
        {
            string info = rooms[nRoom].GetInfo();
            return info;
        }
        public string GetItemsRoom(int nRoom)
        {
            int[] arrAux = rooms[nRoom].GetArrayItems();
            string info="-";
            for(int i = 0; i < arrAux.Length; i++)
            {
                info += items[arrAux[i]].name + ": " + items[arrAux[i]].description + "\n";
            }
            return info;
        }
        public void SetItemsRooms()
        {
            int i = 0;
            while (i < nItems)
            {
                AddItemRoom(items[i].initialRoom, i);
                i++;
            }
        }
        public void WriteMap()//
        {
            int i;
            for( i=1; i<nRooms+1; i++)
            {
               string infoRoom = GetInfoRoom(i);
               string infoItem = GetItemsRoom(i);
               Console.WriteLine(infoRoom + "\n" + infoItem);

                //las direcciones con su información correspondiente
            }
        }
        public bool TakeItemRoom(int nRoom, string itemName, List inventory)//
        {
            int[] aux = rooms[nRoom].GetArrayItems();
            bool enc=false;
            int i = 0;
            while(i < aux.Length && !enc)
            {
                if (items[i].name == itemName)//??
                {
                    enc = true;
                    rooms[nRoom].RemoveItem(i);// i??
                    inventory.InsertaFinal(i);// i??
                }
                else i++; 
            }
            return enc;
        }
        public bool DropItemRoom(int nRoom, string itemName, List inventory)//
        {
            int[] aux = rooms[nRoom].GetArrayItems();
            bool enc = false;
            int i = 0;
            while (i < aux.Length && !enc)
            {
                if (items[i].name == itemName && inventory.BuscaDato(i))//??
                {
                    enc = true;
                    rooms[nRoom].AddItem(i);// i??
                    inventory.EliminaElto(i);// i??
                }
                else i++;
            }
            return enc;
        }
        public List Move(int nRoom, string dir, List inventory)//
        {
            List roomsVisited = new List();
            roomsVisited.InsertaFinal(nRoom);
            int destRoom = rooms[nRoom].Move(dir, inventory);
            if(destRoom != -1) roomsVisited.InsertaFinal(destRoom);

            while (rooms[destRoom].ForcedMove())
            {
                destRoom = rooms[destRoom].Move(dir, inventory);//
                roomsVisited.InsertaFinal(destRoom);
            }
            return roomsVisited;
        }
        public string GetItemsInfo(List inventory)//
        {
            string itemsInfo="";
            int[] arrItemsInfo = inventory.ToArray();
            for(int i=0; i<arrItemsInfo.Length; i++)
            {
                itemsInfo += items[arrItemsInfo[i]].name + ": " + items[arrItemsInfo[i]].description + "\n";
            }
            return itemsInfo;
        }
        
    }
}
