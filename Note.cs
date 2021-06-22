using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_SKP
{
    struct Note
    {
       
        /// <summary>
        /// Номер заметки
        /// </summary>
        public int NumberOfNote { get; private set; }

        /// <summary>
        /// Дата заметки
        /// </summary>
        public DateTime DateOfNote { get; private set; }

        /// <summary>
        /// Автор заметки
        /// </summary>
        public string AuthorOfNote { get; set; }

        /// <summary>
        /// Содержимое заметки
        /// </summary>
        public string ContentOfNote { get; set; }
        
        /// <summary>
        /// Категория заметки
        /// </summary>
        public string TypeOfNote { get; set; }

        /// <summary>
        /// Конструктор для структуры
        /// </summary>
        /// <param name="numberOfNote">Номер заметки</param>
        /// <param name="dateOfNote">Дата заметки</param>
        /// <param name="authorOfNote">Автор заметки</param>
        /// <param name="contentOfNote">Содержимое заметки</param>
        /// <param name="typeOfNote">Категория заметки</param>
        public Note(int numberOfNote, DateTime dateOfNote, string authorOfNote, string contentOfNote, string typeOfNote)
        {
            NumberOfNote = numberOfNote;

            DateOfNote = dateOfNote;

            AuthorOfNote = authorOfNote;

            ContentOfNote = contentOfNote;

            TypeOfNote = typeOfNote;
        }

    }

    
}
