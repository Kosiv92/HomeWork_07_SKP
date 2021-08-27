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
            Console.WriteLine(
                "Перед началом работы укажите директорию, где находится или где необходимо создать файл с ежедневником\nФайл должен называться - \"Diary.csv\"\n" +
                "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он будет создан\n");
            string diaryPath = Console.ReadLine();

            Diary myDiary = new Diary();

            myDiary.SetPath(diaryPath);

            ChooseKindOfLoad(myDiary);

            ShowMenu(myDiary);                       

        }
             

        /// <summary>
        /// Основное меню программы
        /// </summary>
        /// <param name="notes"></param>
        static void ShowMenu(Diary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите необходимое действие:\n1 - Добавить заметку\n2 - Вывести все заметки на экран\n3 - Редактировать заметку\n4 - Удалить заметку\n5 - Закрыть приложение и сохранить все заметки в файл");
                ConsoleKeyInfo buttonPressed = Console.ReadKey();; 
                switch (buttonPressed.KeyChar)
                {
                    case '1':   //добавление заметки
                        
                        var CurrentNote = Note.Add(myDiary.CountNotes); 
                                             
                        myDiary.AddNote(CurrentNote);

                        break;
                    
                    case '2':   //Вывод заметок на экран консоли
                        myDiary.ChooseShowMode();
                        break;

                        case '3':   //редактирование по номеру



                            break;

                    case '4':   // удаление заметки по номеру

                       DiaryTerminator diaryTerminator = new DiaryTerminator(myDiary);

                        myDiary.DeleteNotes(diaryTerminator);

                        break;

                    case '5':   //закрытие приложения (с записью всех заметок в файл)
                        myDiary.UploadNotes();

                        Environment.Exit(0);

                        break;
                }
            }
        }

        /// <summary>
        /// Выбор режима загрузки заметок из файла
        /// </summary>
        /// <param name="myDiary"></param>
        static void ChooseKindOfLoad(Diary myDiary)
        {
            bool methodWork = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Выберите режим загрузки заметок из файла:\n1 - Обычный\n2 - Загрузка по диапозону дат");
                ConsoleKeyInfo buttonPressed = Console.ReadKey();; 
                switch (buttonPressed.Key)
                {
                    case ConsoleKey.D1:
                        myDiary.LoadNotes();
                        methodWork = false;
                        break;
                        case ConsoleKey.D2:
                        myDiary.LoadNotesByDates();
                        methodWork = false;
                        break;
                        default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();                        
                        break;
                }
            }while(methodWork);

        }

        //static void ChooseNotesForDelete();


        /// <summary>
        /// Метод загрузки объектов структуры (заметок) из файла
        /// </summary>
        /// <param name="notes">Коллекция объектов в которую будет происходить загрузка</param>
        /// <param name="pathForDiary">Путь к файлу</param>
        //static void LoadFromFile(List<Note> notes, string pathForDiary)
        //{
        //    int count = 0; //счетчик количества заметок

        //    string[] lines = File.ReadAllLines(pathForDiary);

        //    if (lines.Length != 0)
        //    {
        //        using (StreamReader readFromFile = new StreamReader(pathForDiary))
        //        {

        //            while (!readFromFile.EndOfStream)
        //            {
        //                string[] rowFromFile = readFromFile.ReadLine().Split(';');

        //                count++;

        //                DateTime parsedDate = DateTime.Parse(rowFromFile[1]);

        //                notes.Add(new Note(count, parsedDate, rowFromFile[2], rowFromFile[3], rowFromFile[4]));
        //            }
        //        }
        //    }
        //}
                

        //static void DeleteNotes(List<Note> notes)
        //{
        //    if (notes.Count == 0)
        //    {
        //        Console.WriteLine("Список заметок пуст. Удаление невозможно"); return;
        //    }

        //    int filterFieldNumber = ChooseDeletedField();

        //    switch (filterFieldNumber)
        //    {
        //        case 0:
        //            break;
        //    }

        //    Console.Write("Укажите ключевое слово по которому будет происходить удаление заметок:");

        //    string filterFieldString = Console.ReadLine();

        //    for (int i = 0; i < notes.Count; i++)
        //    {

        //    }

        //    int deletedField = ChooseDeletedField();    //определение поля по которому будет происходить удаление объектов


        //}
        
    }
}
