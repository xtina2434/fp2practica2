using System;
using System.IO;
using Listas;

namespace Practica2
{
    
    class Room
    {
        struct Route //tipo para las rutas
        {
            public string direction;
            public int destRoom,        //habitación destino de la ruta
                       conditionalItem; //indice del item condicional (al array de items de Map)
        }                               // -1 si no hay ítem condicional

        string name, description; // nombre y descripción de la habitación leídos de CrowtherRooms
        Route[] routes;           // array de rutas de la habitación
        int nRoutes;              // número de rutas = índice a la primera ruta libre
        List items;            // lista de índices de ítems (al array de ítems de Map)

        public Room(string nam, string des, int maxRts)
        {
            name = nam;
            description = des;
            routes = new Route[maxRts];
            nRoutes = 0;
            items = new List(); 
        }
        public void AddRoute(string dir, int desR, int condIt)
        {
            routes[nRoutes].direction = dir;
            routes[nRoutes].destRoom = desR;
            routes[nRoutes].conditionalItem = condIt;
            nRoutes++;
        }
        public void AddItem(int it)
        {
            items.InsertaFinal(it);
        }
        public string GetInfo()
        {
            string info = name + "\n" + description;
            /*string dirInfo="";
            for(int i=0; i<nRoutes; i++)
            {
                dirInfo += routes[nRoutes].direction + routes[nRoutes].destRoom + routes[nRoutes].conditionalItem + "\n";
               // dirInfo += routes[nRoutes].direction + "\n"; //+ routes[nRoutes].destRoom + routes[nRoutes].conditionalItem
            }*/
          // string infoTotal = info + "\n" + dirInfo;
            return info;
        }
        public int[] GetArrayItems()
        {
            int[] arrItems= items.ToArray();
            return arrItems;
        }
        public int Move(string dir, List inventory)//revisar
        {
            int i = 0;
            bool enc = false;
            while(i<nRoutes && !enc)
            {
               // if ((routes[i].direction == dir && inventory.BuscaDato(routes[i].conditionalItem)) || routes[i].direction == dir) enc = true;
                if(routes[i].direction == dir)
                {
                    if (routes[i].conditionalItem != -1 && inventory.BuscaDato(routes[i].conditionalItem)) enc = true;
                    else enc = true;
                }
                else i++;
            }
            if (enc) return routes[i].destRoom;
            else return -1;
        }
        public bool ForcedMove()
        {
            int i = 0;
            bool enc = false;
            while(i < routes.Length && !enc)
            {
                if (routes[i].direction == "FORCED") enc = true;
                else i++;
            }
            return enc;
        }
        public bool RemoveItem(int it)//
        {
            return items.EliminaElto(it);
        }
    }
}
