using System;
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
                        
            Content = content;

            Type = type;
        }

        /// <summary>
        /// Добавление новой заметки
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Note Add(int count)
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

        /// <summary>
        /// Метод преобразования строки из файла в объект структуры "Заметка"
        /// </summary>
        /// <param name="rowFromFile">Строка из файла хранящего заметки</param>
        /// <param name="notesCount">текущий номер добавляемой заметки</param>
        /// <returns></returns>
        public static Note Parse(string rowFromFile, int notesCount)
        {
            string[] fieldsOfClass = rowFromFile.Split(';');  //объявлением массива c полями объекта заметка

            DateTime parsedDate = DateTime.Parse(fieldsOfClass[1]);   //преобразование строки в формат даты

            var newNote = new Note(notesCount, parsedDate, fieldsOfClass[2], fieldsOfClass[3], fieldsOfClass[4]);

            return newNote;
        }

        /// <summary>
        /// Изменение номера заметки после удаления предшествующей заметки в общем списке
        /// </summary>
        public void ChangeNumber()
        {
            Number--;   //уменьшение номера заметки на единицу
        }

    }


}
