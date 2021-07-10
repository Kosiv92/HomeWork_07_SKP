﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_SKP
{
    /// <summary>
    /// Структура описывающая сущность "заметка"
    /// </summary>
    struct Note
    {
       
        /// <summary>
        /// Номер заметки
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Дата заметки
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Автор заметки
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        public string Content { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// Категория заметки
        /// </summary>et; set; }

        /// <summary>
        /// Конструктор для структуры Заметка
        /// </summary>
        /// <param name="numberOfNote">Номер заметки</param>
        /// <param name="dateOfNote">Дата заметки</param>
        /// <param name="authorOfNote">Автор заметки</param>
        /// <param name="contentOfNote">Содержимое заметки</param>
        /// <param name="typeOfNote">Категория заметки</param>
        public Note(int number, DateTime date, string author, string content, string type)
        {
            Author = author;
            
            Number = number;

            Date = date;

            Author = author;

            Content = content;

            Type = type;
        }

        /// <summary>
        /// Добавление новой заметки
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Note Add(int count)
        {
            
            int numberOfNote = count + 1;

            DateTime dateOfNote = DateTime.Today;

            Console.Write("\nВведите автора заметки:");

            string authorOfNote = Console.ReadLine();

            Console.Write("\nВведите содержимое заметки:");

            string contentOfNote = Console.ReadLine();

            Console.Write("\nВведите категорию заметки заметки:");

            string typeOfNote = Console.ReadLine();

            Note currentNote = new Note(numberOfNote, dateOfNote, authorOfNote, contentOfNote, typeOfNote);
            
            Console.WriteLine("\nЗаметка добавлена");
            Console.ReadKey();

            return currentNote;
        }

    }


}
