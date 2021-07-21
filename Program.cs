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

            ShowMenu(myDiary);
            
            //myDiary.LoadNotes();    //загрузка заметок из файла

            //var CurrentNote = new Note();

            //CurrentNote.Add(myDiary.CountNotes);
            
            //myDiary.AddNote(CurrentNote);


            //var notes = new List<Note>(); //объявление списка объектов (дневников)

            //string pathForDiary = GetDiaryPath(notes); //получение пути к файлу-ежедневнику от пользователя

            //ShowMenu(notes, pathForDiary);    //Переход в меню

        }

        /// <summary>
        /// Ds
        /// </summary>
        /// <param name="notes"></param>
        static void ShowMenu(Diary myDiary)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите необходимое действие:\n1 - Добавить заметку\n2 - Вывести все заметки на экран\n3 - Закрыть приложение и сохранить все заметки в файл");
                ConsoleKeyInfo buttonPressed; //нажимаемая пользователем клавиша
                buttonPressed = Console.ReadKey();
                switch (buttonPressed.KeyChar)
                {
                    case '1':   //добавление заметки
                        
                        var CurrentNote = Note.Add(myDiary.CountNotes); 
                                             
                        myDiary.AddNote(CurrentNote);

                        break;
                    
                    case '2':   //Вывод всех заметок на экран консоли
                        myDiary.ShowNotes();
                        break;
                    
                    //case '3':

                    //    break;

                    case '3':   //закрытие приложения (с записью всех заметок в файл)
                        myDiary.UploadNotes();

                        Environment.Exit(0);

                        break;
                }
            }
        }

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

        /// <summary>
        /// Метод загрузки структуры в файл
        /// </summary>
        /// <param name="notes">Список объектов структуры</param>
        /// <param name="pathForDiary">Путь к файлу для записи</param>
        //static void UploadToFile(List<Note> notes, string pathForDiary)
        //{

        //    using (StreamWriter writeToFile = new StreamWriter(pathForDiary, false, Encoding.UTF32))
        //    {
        //        FileInfo infoDiary = new FileInfo(pathForDiary);
        //        if(!infoDiary.Exists) File.Create(pathForDiary);    //проверка существования файла по указанному пути
        //            foreach (var note in notes)
        //        {

        //            writeToFile.Write(note.Number + ";"  + note.Date + ";" + note.Author + ";" + note.Content + ";" + note.Type);
        //            writeToFile.WriteLine();
        //        }

        //    }
        //}

        /// <summary>
        /// Указание пути к файлу со списокм ежедневников
        /// </summary>
        /// <returns></returns>
        //static string GetDiaryPath(List<Note> notes)
        //{
        //    Console.WriteLine(
        //        "Перед началом работы укажите директорию, где находится или где необходимо создать файл с ежедневником\nФайл должен называться - \"Diary.csv\"\n" +
        //        "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он будет создан\n");
        //    string diaryPath;

        //    while (true)
        //    {
        //        diaryPath = Console.ReadLine();
        //        try
        //        {
        //            DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(diaryPath);   //проверка на пустую строку
        //            diaryPath += "\\Diary.csv";
        //            FileInfo infoAboutDiary = new FileInfo(diaryPath);
        //            if (infoAboutDiary.Exists)
        //            {
        //                Console.WriteLine(
        //                    $"Обнаружен существующий файл-ежедневник для хранения заметок - {diaryPath}");
        //                LoadFromFile(notes, diaryPath);
        //                Console.ReadKey();
        //                return diaryPath;
        //            }
        //            else
        //            {
        //                //File.Create(diaryPath);
        //                Console.WriteLine($"Файл не обнаружен. Будет создан новый файл-ежедневник для хранения заметок - {diaryPath}");
        //                Console.ReadKey();
        //                return diaryPath;
        //            }
        //        }
        //        catch
        //        {
        //            Console.WriteLine(
        //                "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
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

        //static int ChooseDeletedField()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Укажите по какому полю вы хотите выбрать заметки для удаления:\n1 - Номер заметки\n2 - Дата заметки\n3 - Автор заметки\n4 - Содержимое заметкиn\n5 - Категория заметки");
        //    int fieldNum = 5;   //номер поля, которое необходимо удалить
        //    ConsoleKeyInfo buttonPressed; //нажимаемая пользователем клавиша
        //    while (fieldNum == 5)
        //    {
        //        buttonPressed = Console.ReadKey();
        //        Console.WriteLine();
        //        switch (buttonPressed.KeyChar)
        //        {
        //            case '1':
        //                fieldNum = 0;
        //                break;
        //            case '2':
        //                fieldNum = 1;
        //                break;
        //            case '3':
        //                fieldNum = 2;
        //                break;
        //            case '4':
        //                fieldNum = 3;
        //                break;
        //            case '5':
        //                fieldNum = 4;
        //                break;
        //            default:
        //                Console.WriteLine("Вы нажали некорректную клавишу. Попробуйте снова");
        //                break;
        //        }

        //    }

        //    return fieldNum;
        //}


        //static void ShowNotes(List<Note> notes)
        //{
        //    Console.Clear();
        //    string[] titles =
        //        {"№ п/п", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки"};
        //    Console.WriteLine($"{titles[0],5} {titles[1],12} {titles[2],13} {titles[3],-40} {titles[4],17}");
        //    foreach (var note in notes)
        //    {
        //        Console.Write($"{note.Number,5} ");
        //        Console.Write($"{note.Date.ToString("d"),12} ");
        //        Console.Write($"{note.Author,13} ");
        //        Console.Write($"{note.Content,-40} ");
        //        Console.Write($"{note.Type,17} ");
        //        Console.WriteLine();
        //    }
        //    Console.ReadKey();
        //}


    }
}
