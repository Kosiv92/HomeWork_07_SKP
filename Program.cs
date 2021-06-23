using System;
using System.Collections.Generic;
using System.IO;

namespace HomeWork_07_SKP
{
    class Program
    {
        static void Main(string[] args)
        {
            var diares = new List<Diary>(); //объявление списка объектов (дневников)
            
            string pathForDiary = GetPathToDiary();
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

            using (StreamReader readFromFile = new StreamReader(pathForDiary))
            {

                while (!readFromFile.EndOfStream)
                {
                    string[] rowFromFile = readFromFile.ReadLine().Split('\t');

                    count++;

                    DateTime parsedDate = DateTime.Parse(rowFromFile[1]);

                    notes.Add(new Note(count, parsedDate, rowFromFile[2], rowFromFile[3], rowFromFile[4]));
                }
            }
                
            }
        


        /// <summary>
        /// Указание пути к файлу со списокм ежедневников
        /// </summary>
        /// <returns></returns>
        static string GetDiaryPath()
        {
            Console.WriteLine(
                "Перед началом работы укажите директорию, где находится или где необходимо создать файл со списком ежедневников\nФайл должен называться - \"DiaryList.csv\"\n" +
                "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он он будет создан\n");
            string diaryPath;

            while (true)
            {
                diaryPath = Console.ReadLine();
                try
                {
                    DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(diaryPath);   //проверка на пустую строку
                    diaryPath += "\\DiaryList.csv";
                    FileInfo infoAboutDiary = new FileInfo(diaryPath);
                    if (infoAboutDiary.Exists)
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {diaryPath}");
                        Console.ReadKey();
                        return diaryPath;
                    }
                    else
                    {
                        File.Create(diaryPath);
                        Console.WriteLine($"Создан новый файл-ежедневник для хранения заметок - {diaryPath}");
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
                        Diary.AddNote(notes);
                        break;
                    case '2':
                        ShowNotes(notes);
                        break;
                    case '3':
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
                Console.Write($"{note.NoteNumber,5} ");
                Console.Write($"{note.NoteDate.ToString("d"),12} ");
                Console.Write($"{note.NoteAuthor,13} ");
                Console.Write($"{note.NoteContent,-40} ");
                Console.Write($"{note.NoteType,17} ");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Добавление новой заметки
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        //static void AddNote(List<Note> notes)
        //{
        //    int numberOfNote = notes.Count + 1;

        //    DateTime dateOfNote = DateTime.Today;
            
        //    Console.Write("\nВведите автора заметки:");
            
        //    string authorOfNote = Console.ReadLine();

        //    Console.Write("\nВведите содержимое заметки:");

        //    string contentOfNote = Console.ReadLine();

        //    Console.Write("\nВведите категорию заметки заметки:");

        //    string typeOfNote = Console.ReadLine();

        //    Note currentNote = new Note(numberOfNote, dateOfNote, authorOfNote, contentOfNote, typeOfNote);

        //    notes.Add(currentNote);

        //    Console.WriteLine("\nЗаметка добавлена");
        //    Console.ReadKey();
        //}
    }
}
