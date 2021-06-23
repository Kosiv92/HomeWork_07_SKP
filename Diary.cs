using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_SKP
{
    /// <summary>
    /// Структура описывающая сущность "Дневник"
    /// </summary>
    struct Diary
    {
        /// <summary>
        /// Список заметок составляющих дневник
        /// </summary>
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Путь к файлу с дневником
        /// </summary>
        public string DiaryPath
        {
            set
            {
                Console.WriteLine(
                    "Перед началом работы укажите директорию, где находится или где необходимо создать файл с  ежедневникоv\nФайл должен называться - \"Diary.csv\"\n" +
                    "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он он будет создан\n");

                while (true)
                {
                    DiaryPath = Console.ReadLine();
                    try
                    {
                        DirectoryInfo
                            infoAboutDirectoryOfDiary = new DirectoryInfo(DiaryPath); //проверка на пустую строку
                        DiaryPath += "\\DiaryList.csv";
                        FileInfo infoAboutDiary = new FileInfo(DiaryPath);
                        if (infoAboutDiary.Exists)
                        {
                            Console.WriteLine(
                                $"Обнаружен существующий файл-ежедневник для хранения заметок - {DiaryPath}");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            File.Create(DiaryPath);
                            Console.WriteLine($"Создан новый файл-ежедневник для хранения заметок - {DiaryPath}");
                            Console.ReadKey();
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine(
                            "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                    }
                }
            }
            get;
        }

        /// <summary>
        /// Название дневника
        /// </summary>
        public string DiaryName { get; set; }

        /// <summary>
        /// Владелец дневника
        /// </summary>
        public string DiaryOwner { get; set; }

        /// <summary>
        /// Конструктор для структуры Дневник
        /// </summary>
        public Diary(string path, string diaryName, string diaryOwner)
        {
            DiaryPath = path;
            
            Notes = new List<Note>();

            DiaryName = diaryName;

            DiaryOwner = diaryOwner;
        }

        /// <summary>
        /// Добавление новой заметки
        /// </summary>
        /// <param name="notes">Список в который добавляется новая заметка</param>
        public void AddNote(List<Note> notes)
        {
            int numberOfNote = notes.Count + 1; //номер вносимой заметки

            DateTime dateOfNote = DateTime.Today;   //дата вносимой заметки

            Console.Write("\nВведите автора заметки:");

            string authorOfNote = Console.ReadLine();

            Console.Write("\nВведите содержимое заметки:");

            string contentOfNote = Console.ReadLine();

            Console.Write("\nВведите категорию заметки заметки:");

            string typeOfNote = Console.ReadLine();

            Note currentNote = new Note(numberOfNote, dateOfNote, authorOfNote, contentOfNote, typeOfNote);

            notes.Add(currentNote);

            Console.WriteLine("\nЗаметка добавлена");
            Console.ReadKey();
        }

        /// <summary>
        /// Указание пути к файлу
        /// </summary>
        /// <returns></returns>
        public string GetDiaryListPath(bool diary); 
        {
            
            Console.WriteLine("Перед началом работы укажите директорию, где находится или где необходимо создать файл со списком ежедневников\nФайл должен называться - \"DiaryList.csv\"\n" +
                              "Вам необходимо вписать путь до директории с файлом в формате - \"D:\\MyFiles\" В случае если файл отсутствует по указанному пути он он будет создан\n");
            
            string diaryListPath;

            while (true)
            {
                diaryListPath = Console.ReadLine();
                try
                {
                    DirectoryInfo infoAboutDirectoryOfDiary = new DirectoryInfo(diaryListPath);   //проверка на пустую строку
                    diaryListPath += "\\DiaryList.csv";
                    FileInfo infoAboutDiary = new FileInfo(diaryListPath);
                    if (infoAboutDiary.Exists)
                    {
                        Console.WriteLine(
                            $"Обнаружен существующий файл-ежедневник для хранения заметок - {diaryListPath}");
                        Console.ReadKey();
                        return diaryListPath;
                    }
                    else
                    {
                        File.Create(diaryListPath);
                        Console.WriteLine($"Создан новый файл-ежедневник для хранения заметок - {diaryListPath}");
                        Console.ReadKey();
                        return diaryListPath;
                    }
                }
                catch
                {
                    Console.WriteLine(
                        "Вы указали неверный путь. Проверьте правильности формата (пример - \"D:\\MyFiles\") и попробуйте снова");
                }
            }
        }
    }
}
