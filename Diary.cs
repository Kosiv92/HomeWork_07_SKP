using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeWork_07_SKP
{
    struct Diary
    {
        private string path;    //путь к файлу хранящему список заметок

        private List<Note> notes;   //список объектов структуры "Заметка"

        private int countNotes; //счетчик количества заметок
        
        /// <summary>
        /// Конструктор создания объекта структуры "Ежедневник"
        /// </summary>
        /// <param name="pathDiary">Путь к файлу храяющему списко заметок</param>
        public Diary(string pathDiary): this()
        {
            Path = pathDiary;

            notes = new List<Note>();   //объявление списка объектов структуры "Заметка"

            countNotes = 0; //начальное значение счетчика количества заметок
        }

        /// <summary>
        /// Свойство доступа к полю "Количество заметок"
        /// </summary>
        public int CountNotes
        {
            private set { countNotes = value; }
            get { return countNotes; }
            
        }

        /// <summary>
        /// Свойство доступа к полю "Путь к файлу с заметками"
        /// </summary>
        public string Path
        {
            set
            {
                
            }
        }

        public string SetPath(string pathDiary)
        {
            while (true)
            {
            

                try
                {
                    DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(pathDiary);   //проверка на пустую строку
                    pathDiary += "\\Diary.csv";
                    FileInfo infoAboutDiary = new FileInfo(pathDiary);
                    if (infoAboutDiary.Exists)
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {pathDiary}");
                        LoadNotes();
                        Console.ReadKey();
                        return pathDiary;
                    }
                    else
                    {
                        File.Create(pathDiary);
                        Console.WriteLine($"Файл не обнаружен. Будет создан новый файл-ежедневник для хранения заметок - {pathDiary}");
                        Console.ReadKey();
                        return pathDiary;
                    }
                }
                catch
                {
                    Console.WriteLine(
                        "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                }
            }
        }


        /// <summary>
        /// Метод добавления нового объекта "Заметка" в список объектов структуры заметка
        /// </summary>
        /// <param name="newNote">Добавляемый объект структуры "Заметка"</param>
        public void AddNote(Note newNote)
        {
            notes[countNotes] = newNote;    
            countNotes++;   //увеличение счетчика числа заметок
        }

        /// <summary>
        /// Метод загрузки объектов "Заметка" из файла
        /// </summary>
        public void LoadNotes()
        {
            string[] lines = File.ReadAllLines(path);   //объявление массива всех строк файла 

            int countOfNotes = 0;   //счетчик количества загруженных заметок
            
            if (lines.Length != 0)
            {
                using (StreamReader diaryReader = new StreamReader(path))
                {
                    while (!diaryReader.EndOfStream)
                    {
                        string[] rowFromFile = diaryReader.ReadLine().Split(';');   //объявлением массива 

                        countNotes++;    //увеличение счетчика числа заметок

                        DateTime parsedDate = DateTime.Parse(rowFromFile[1]);   //преобразование строки в формат даты

                        var newNote = new Note(countNotes, parsedDate, rowFromFile[2], rowFromFile[3], rowFromFile[4]);

                        AddNote(newNote);   //добавление нового объекта в списко объектов "Заметка"

                        countOfNotes++;
                    }
                }
            }

            Console.WriteLine($"Загружено {countOfNotes} заметок");

        }

        /// <summary>
        /// Метод записи объектов "Заметка" в файл
        /// </summary>
        public void UploadNotes()
        {
            using (StreamWriter writeToFile = new StreamWriter(path, false, Encoding.UTF32))
            {
                foreach (var note in notes)
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
            
            string[] titles = {"№ п/п", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки"};  //объявляем массив с наименованием столбцов

            Console.WriteLine($"{titles[0],5} {titles[1],12} {titles[2],13} {titles[3],-40} {titles[4],17}");

            foreach (var note in notes)
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
    }
}
