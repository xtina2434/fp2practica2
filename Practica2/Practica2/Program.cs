﻿// Cristina Muñoz Muñoz
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
            ReadInventory("CrowtherItems.txt");
            ReadRooms("CrowtherRooms.txt");
        }
        static void ReadInventory(string file)
        {
            StreamReader streamReader = new StreamReader(file);

            while (!streamReader.EndOfStream)
            {
               string item = streamReader.ReadLine();
               string descr = streamReader.ReadLine();
               int room = int.Parse(streamReader.ReadLine());
               streamReader.ReadLine();
               Console.Write("Item name: " + item + "  Descr: " + descr + "  InitRoom: " + room);
               Console.WriteLine();
            }
            streamReader.Close();
        }
        static void ReadRooms(string file)
        {
            StreamReader f = new StreamReader(file);
            while (!f.EndOfStream)
            {
                //string room = f.ReadLine();
                 int numRoom = int.Parse(f.ReadLine());
                 ReadRoom(f, numRoom);
            }
            f.Close();
        }
        static void ReadRoom (StreamReader f, int n)
        {
            string name = f.ReadLine();
            string descr = f.ReadLine();
            Console.Write("Room: " + n + "  Name: " + name + "  Descr: " + descr);
            Console.WriteLine();
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
                Console.Write("Route from room: " + n + " to room " + nextRoom + ", direction " + direction + ". CondItem: " + item);
                Console.WriteLine();
                s = f.ReadLine();
            }
          
        }
    }
}