﻿// HomeWork template 1.4 // date: 17.10.2023

using Service;
using System;
using System.Linq.Expressions;
using System.Text;

/// QUESTIONS ///
/// 1. 

// HomeWork 08 : [{Delegates}] --------------------------------

namespace IDA_C_sh_HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();

            do
            {
                Console.Clear();
                mainMenu.Show_menu();
                if (mainMenu.User_Choice_Handle() == 0) break;
                Console.ReadKey();
            } while (true);
        }

        public static void Task_1(string work_name)
        /*  Задание 1. Игра «Автомобильные гонки»
        Разработать игру "Автомобильные гонки" с использова-
        нием делегатов.
        1. В игре использовать несколько типов автомобилей:
        спортивные, легковые, грузовые и автобусы.
        2. Реализовать игру «Гонки». Принцип игры: Автомо-
        били двигаются от старта к финишу со скоростями,
        которые изменяются в установленных пределах
        случайным образом. Победителем считается ав-
        томобиль, пришедший к финишу первым.*/
        { 
            Console.WriteLine("\n***\t{0}\n\n", work_name); 
        
            int car_quantity = default(int);


            _ = RaceGame.exe;

           // RaceGame raceGame_obj = new RaceGame();






        }
        public static void Task_2(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }
        public static void Task_3(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }
        public static void Task_4(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }
        public static void Task_5(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }
        public static void Task_6(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }
        public static void Task_7(string work_name)
        /* Задание */
        { Console.WriteLine("\n***\t{0}\n\n", work_name); }

    }// class Program
}// namespace