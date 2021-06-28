using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace HomeWork_07_SKP
{
    class Program
    {
        static void Main(string[] args)
        {
            var notes = new List<Note>(); //объявление списка объектов (дневников)
            
            string pathForDiary = GetDiaryPath(notes); //получение пути к файлу-ежедневнику от пользователя
            
            ShowMenu(notes, pathForDiary);    //Переход в меню

        }

        /// <summary>
        /// Метод загрузки объектов структуры (заметок) из файла
        /// </summary>
        /// <param name="notes">Коллекция объектов в которую будет происходить загрузка</param>
        /// <param name="pathForDiary">Путь к файлу</param>
        static void LoadFromFile(List<Note> notes, string pathForDiary)
        {
            int count = 0; //счетчик количества заметок

            string[] lines = File.ReadAllLines(pathForDiary);

            if (lines.Length != 0)
            {
                using (StreamReader readFromFile = new StreamReader(pathForDiary))
                {

                    while (!readFromFile.EndOfStream)
                    {
                        string[] rowFromFile = readFromFile.ReadLine().Split(';');

                        count++;
                        
                        //var allowedFormatsDate = new[] { "MM-dd-yyyy", "dd-MM-yyyy hh-mm-ss" };

                        DateTime parsedDate = DateTime.ParseExact(rowFromFile[1], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

                        notes.Add(new Note(count, parsedDate, rowFromFile[2], rowFromFile[3], rowFromFile[4]));
                    }
                }
            }
        }

        /// <summary>
        /// Метод загрузки структуры в файл
        /// </summary>
        /// <param name="notes">Список объектов структуры</param>
        /// <param name="pathForDiary">Путь к файлу для записи</param>
        static void UploadToFile(List<Note> notes, string pathForDiary)
        {
            
            using (StreamWriter writeToFile = new StreamWriter(pathForDiary, false, Encoding.UTF32))
            {
                FileInfo infoDiary = new FileInfo(pathForDiary);
                if(!infoDiary.Exists) File.Create(pathForDiary);
                    foreach (var note in notes)
                {
                    //string tempStringForWrite = String.Format("{0},{1},{2},{3},{4}",
                    //    note.Number, note.Date, note.Author, note.Content, note.Type);

                    writeToFile.Write(note.Number + ";"  + note.Date + ";" + note.Author + ";" + note.Content + ";" + note.Type);
                    writeToFile.WriteLine();
                }
                
            }
        }
        
        /// <summary>
        /// Указание пути к файлу со списокм ежедневников
        /// </summary>
        /// <returns></returns>
        static string GetDiaryPath(List<Note> notes)
        {
            Console.WriteLine(
                "Перед началом работы укажите директорию, где находится или где необходимо создать файл с ежедневником\nФайл должен называться - \"Diary.csv\"\n" +
                "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он будет создан\n");
            string diaryPath;

            while (true)
            {
                diaryPath = Console.ReadLine();
                try
                {
                    DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(diaryPath);   //проверка на пустую строку
                    diaryPath += "\\Diary.csv";
                    FileInfo infoAboutDiary = new FileInfo(diaryPath);
                    if (infoAboutDiary.Exists)
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {diaryPath}");
                        LoadFromFile(notes, diaryPath);
                        Console.ReadKey();
                        return diaryPath;
                    }
                    else
                    {
                        //File.Create(diaryPath);
                        Console.WriteLine($"Файл не обнаружен. Будет создан новый файл-ежедневник для хранения заметок - {diaryPath}");
                        Console.ReadKey();
                        return diaryPath;
                    }
                }
                catch
                {
                    Console.WriteLine(
                        "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                }
            }
        }

        static void ShowMenu(List<Note> notes, string pathForDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите необходимое действие:\n1 - Добавить заметку\n2 - Вывести все заметки на экран\n3 - Закрыть приложение");
                ConsoleKeyInfo buttonPressed; //нажимаемая пользователем клавиша
                buttonPressed = Console.ReadKey();
                switch (buttonPressed.KeyChar)
                {
                    case '1':
                        notes.Add(Note.Add(notes.Count));
                        break;
                    case '2':
                        ShowNotes(notes);
                        break;
                    case '3':
                        UploadToFile(notes, pathForDiary);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ShowNotes(List<Note> notes)
        {
            Console.Clear();
            string[] titles =
                {"№ п/п", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки"};
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
