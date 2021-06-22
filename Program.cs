using System;
using System.Collections.Generic;
using System.IO;

namespace HomeWork_07_SKP
{
    class Program
    {
        static void Main(string[] args)
        {
            var notes = new List<Note>();   //объявление списка объектов (заметок в ежедневнике)
            string pathForDiary = GetPathToDiary();
            ShowMenu(ref notes, pathForDiary);    //Переход в меню

        }

        /// <summary>
        /// Указание пути к файлу-ежедневнику
        /// </summary>
        /// <returns></returns>
        static string GetPathToDiary()
        {
            Console.WriteLine(
                "Перед началом работы укажите директорию, где находится или где необходимо создать ежедневник\nФайл должен называться - \"Diary.csv\"\n" +
                "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он он будет создан\n");
            string pathToDiary;

            while (true)
            {
                pathToDiary = Console.ReadLine();
                try
                {
                    DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(pathToDiary);
                    pathToDiary += "\\Diary.csv";
                    FileInfo infoAboutDiary = new FileInfo(pathToDiary);
                    if (infoAboutDiary.Exists)
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {pathToDiary}");
                        return pathToDiary;
                    }
                    else
                    {
                        File.Create(pathToDiary);
                        Console.WriteLine($"Создан новый файл-ежедневник для хранения заметок - {pathToDiary}");
                        return pathToDiary;
                    }
                }
                catch
                {
                    Console.WriteLine(
                        "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                }
            }
        }

        static void ShowMenu(ref List<Note> notes, string pathForDiary)
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
                        AddNote(ref notes);
                        break;
                    case '2':
                        ShowNotes(ref notes);
                        break;
                    case '3':
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ShowNotes(ref List<Note> notes)
        {
            Console.Clear();
            string[] titles =
                {"Номер заметки", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки"};
            Console.WriteLine($"{titles[0],3} {titles[1],9} {titles[2],9} {titles[3],20} {titles[4],10}");
            foreach (var note in notes)
            {
                Console.Write($"{note.NumberOfNote,3} ");
                Console.Write($"{note.DateOfNote,9} ");
                Console.Write($"{note.AuthorOfNote,9} ");
                Console.Write($"{note.ContentOfNote,20} ");
                Console.Write($"{note.TypeOfNote,10} ");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Добавление новой заметки
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        static void AddNote(ref List<Note> notes)
        {
            int numberOfNote = notes.Count + 1;

            DateTime dateOfNote = DateTime.Today;
            
            Console.Write("Введите автора заметки:");
            
            string authorOfNote = Console.ReadLine();

            Console.Write("\nВведите содержимое заметки:");

            string contentOfNote = Console.ReadLine();

            Console.WriteLine("\nВведите категорию заметки заметки:");

            string typeOfNote = Console.ReadLine();

            Note currentNote = new Note(numberOfNote, dateOfNote, authorOfNote, contentOfNote, typeOfNote);

            notes.Add(currentNote);

            Console.WriteLine("Заметка добавлена");
            Console.ReadKey();
        }
    }
}
