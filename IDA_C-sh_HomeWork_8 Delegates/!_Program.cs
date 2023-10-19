// HomeWork template 1.4 // date: 17.10.2023

using Service;
using System;
using System.Linq.Expressions;
using System.Text;
using nmspRaceGame;
using nmspCardGame;

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
            Console.WriteLine(work_name);         

            // Запускаем игру по старинке - через .ехе ))
            RaceGame.exe();

        }
        public static void Task_2(string work_name)
        /* Задание 2. Программа «Карточная игра!»
        Создать модель карточной игры.
        Требования:
        1. Класс Game формирует и обеспечивает:
        1.1.1. Список игроков (минимум 2);
        1.1.2. Колоду карт (36 карт);
        1.1.3. Перетасовку карт (случайным образом);
        1.1.4. Раздачу карт игрокам (равными частями каждому
        игроку);
        1.1.5. Игровой процесс. Принцип: Игроки кладут по
        одной карте. У кого карта больше, то тот игрок
        забирает все карты и кладет их в конец своей
        колоды. Упрощение: при совпадении карт заби-
        рает первый игрок, шестерка не забирает туза.
        Выигрывает игрок, который забрал все карты.
        2. Класс Player (набор имеющихся карт, вывод име-
        ющихся карт).
        3. Класс Karta (масть и тип карты (6–10, валет, дама,
        король, туз). */
        {
            Console.WriteLine(work_name);
            
            CardGame.exe();       
            
        }


    }// class Program
}// namespace