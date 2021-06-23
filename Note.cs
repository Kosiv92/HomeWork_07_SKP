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
        public int NoteNumber { get; private set; }

        /// <summary>
        /// Дата заметки
        /// </summary>
        public DateTime NoteDate { get; private set; }

        /// <summary>
        /// Автор заметки
        /// </summary>
        public string NoteAuthor { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        public string NoteContent { get; set; }
        
        /// <summary>
        /// Категория заметки
        /// </summary>
        public string NoteType { get; set; }

        /// <summary>
        /// Конструктор для структуры Заметка
        /// </summary>
        /// <param name="numberOfNote">Номер заметки</param>
        /// <param name="dateOfNote">Дата заметки</param>
        /// <param name="authorOfNote">Автор заметки</param>
        /// <param name="contentOfNote">Содержимое заметки</param>
        /// <param name="typeOfNote">Категория заметки</param>
        public Note(int noteNumber, DateTime noteDate, string noteAuthor, string noteContent, string noteType)
        {
            NoteAuthor = noteAuthor;
            
            NoteNumber = noteNumber;

            NoteDate = noteDate;

            NoteAuthor = noteAuthor;

            NoteContent = noteContent;

            NoteType = noteType;
        }

    }

    
}
