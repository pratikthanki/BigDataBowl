﻿using System;

namespace NFL.BigDataBowl
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DataReader dataReader = new DataReader();
            dataReader.ParseGames();
        }
    }
}