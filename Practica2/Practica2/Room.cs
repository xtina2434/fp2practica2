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
            nRoutes = 0;//con 0 rutas?
            items = new List(); //lista vacia de items ??
        }
        public void AddRoute(string dir, int desR, int condIt)
        {
            routes[nRoutes].direction = dir;
            routes[nRoutes].destRoom = desR;
            routes[nRoutes].conditionalItem = condIt;
            nRoutes++;//??
        }
        public void AddItem(int it)//??
        {
            items.InsertaFinal(it);
        }
        public string GetInfo()///revisar
        {
            string info = name + description;
            return info;
        }
        /*public int[] GetArrayItems()
        {

        }*/
       /* public int Move(string dir, List inventory)
        {

        }*/
        public bool ForcedMove()
        {
            int i = 0;
            bool enc = false;
            while(i < routes.Length && !enc)
            {
                if (routes[i].direction == "FORCED") enc = true;
                else i++;
            }
            if (enc) return true;
            else return false;
        }
        /*public bool RemoveItem(int it)
        {

        }*/
    }
}
