﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HomeWork_07_SKP
{
    struct Diary : IDisposable
    {
        private List<Note> _notes;
        public List<Note> Notes
        {
            get
            {
                if (_notes is null)
                    _notes = new List<Note>();
                return _notes;
            }
        }

        /// <summary>
        /// Свойство доступа к полю "Количество заметок"
        /// </summary>
        public int CountNotes => Notes.Count();

        /// <summary>
        /// Свойство доступа к полю "Путь к файлу с заметками"
        /// </summary>
        public string Path { get; private set; }


        /// <summary>
        /// Метод установки пути к файлу с дневником
        /// </summary>
        /// <param name="pathDiary"></param>
        /// <returns></returns>
        public string SetPath(string pathDiary)
        {
            while (true)
            {
                try
                {
                    //DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(pathDiary);   //проверка на пустую строку

                    Path = pathDiary + @"\Diary.csv";
                    if (File.Exists(Path))
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {Path}");
                        LoadNotes();
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        using (File.Create(Path))                       
                        Console.WriteLine($"Файл не обнаружен. Будет создан новый файл-ежедневник для хранения заметок - {Path}");
                        Console.ReadKey();
                        break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ошибка установки пути. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                    Console.WriteLine($"Ошибка:\n{ex}");                    
                }

                Console.WriteLine();
                Console.Write("Введите путь к файлу снова: ");
                pathDiary = Console.ReadLine();

            }

            return Path;
        }


        /// <summary>
        /// Метод добавления нового объекта "Заметка" в список объектов структуры заметка
        /// </summary>
        /// <param name="newNote">Добавляемый объект структуры "Заметка"</param>
        public void AddNote(Note newNote)
        {
            Notes.Add(newNote);
        }

        /// <summary>
        /// Метод загрузки объектов "Заметка" из файла
        /// </summary>
        public void LoadNotes()
        {
            // 1. Нах 2 раза с файла считать хочешь?
            // 2. В структуре Note объявляешь статический метод <Note Parse(string)> или <bool TryParse(string, out Note note)>и там парсишь

            string[] lines = File.ReadAllLines(Path);   //объявление массива всех строк файла 

            int notesCount = 1;   //счетчик количества загруженных заметок

            if (lines.Length != 0)
            {
                using (StreamReader diaryReader = new StreamReader(Path))
                {
                    while (!diaryReader.EndOfStream)
                    {
                        string rowFromFile = diaryReader.ReadLine();   //объявление переменной для хранения данных строки из файла 
                                                
                        AddNote(Note.Parse(rowFromFile, notesCount));   //добавление нового объекта в списко объектов "Заметка"
                            
                        notesCount++;
                        
                    }
                }
            }

            Console.WriteLine($"Загружено {notesCount - 1} заметок");

        }

        /// <summary>
        /// Метод записи объектов "Заметка" в файл
        /// </summary>
        public void UploadNotes()
        {            
            using (StreamWriter writeToFile = new StreamWriter(Path, false, Encoding.UTF32))
            {
                foreach (var note in Notes)
                {
                    writeToFile.Write(note.Number + ";" + note.Date + ";" + note.Author + ";" + note.Content + ";" + note.Type);
                    writeToFile.WriteLine();
                }
            }
        }

        /// <summary>
        /// Метод вывода всех заметок на экран консоли
        /// </summary>
        public void ShowNotes()
        {
            Console.Clear();

            string[] titles = { "№ п/п", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки" };  //объявляем массив с наименованием столбцов

            Console.WriteLine($"{titles[0],5} {titles[1],12} {titles[2],13} {titles[3],-40} {titles[4],17}");

            foreach (var note in _notes)
            {
                Console.Write($"{note.Number,5} ");
                Console.Write($"{note.Date.ToString("d"),12} ");
                Console.Write($"{note.Author,13} ");
                Console.Write($"{note.Content,-40} ");
                Console.Write($"{note.Type,17} ");
                Console.WriteLine();
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Проверка не превышает ли указанный номер заметки счетчик количества заметок
        /// </summary>
        /// <param name="numberNote">Номер заметки</param>
        /// <returns></returns>
        public bool CheckNumberNote(int numberNote)
        {
            if (numberNote <= CountNotes) return true;
            else return false;
        }

        /// <summary>
        /// Удаление заметки по номеру
        /// </summary>
        /// <param name="numberNote">номер заметки</param>
        public void DeleteNoteByNumber(int numberNote)
        {
            if (!CheckNumberNote(numberNote))
            {
                Console.WriteLine($"Заявки с номером {numberNote} не существует. Пожалуйста попробуйте снова...");

                Console.ReadKey();

                return;
            }

            Notes.RemoveAt(numberNote - 1); //Удаление заметки из списка заметок

            for (int i = numberNote - 1; i < CountNotes; i++)   //цикл изменения номеров заметок находящихся после удаленной
            {
                Notes[i].ChangeNumber();
            }

            Console.WriteLine("Заметка успешно удалена. Обратите внимание, что нумерация заметок изменилась!");

            Console.ReadKey();
        }


        public void Dispose()
        {
            _notes = null;
            Path = null;
        }
    }
}
