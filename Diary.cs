using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HomeWork_07_SKP
{
    
    
    struct Diary
    {
        /// <summary>
        /// Приватное поле для хранения коллекции заметок
        /// </summary>
        private List<Note> _notes;

        /// <summary>
        /// Свойство доступа к полю "Коллекция заметок"
        /// </summary>
        public List<Note> Notes
        {
            get {
                if (_notes is null) _notes = new List<Note>();
                return _notes;
            }
            private set { _notes = value; }
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
        /// Перечисление полей заметки
        /// </summary>
        
        //private DiaryRequest Request = new DiaryRequest();



        /// <summary>
        /// Метод выбора поля заметки
        /// </summary>
        /// <returns>Возвращает выбранное пользователем поле</returns>
        public static diaryColumns ChooseDiaryColumns()
        {
            Console.WriteLine();
            
            while (true)
            {
                
                Console.WriteLine(
                    "Пожалуйста выберите поле заметки:\n1 - Номер заметки\n2 - Дата заметки\n3 - Автор заметки\n4 - Категория заметки");
                var chooseUser = Console.ReadKey();

                switch (chooseUser.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Вы выбрали поле \"Номер заметки\"");
                        Console.ReadKey();
                        return diaryColumns.number;
                    case ConsoleKey.D2:
                        Console.WriteLine("Вы выбрали поле \"Дата заметки\"");
                        Console.ReadKey();
                        return diaryColumns.date;
                    case ConsoleKey.D3:
                        Console.WriteLine("Вы выбрали поле \"Автор заметки\"");
                        Console.ReadKey();
                        return diaryColumns.author;
                    case ConsoleKey.D4:
                        Console.WriteLine("Вы выбрали поле \"Категория заметки\"");
                        Console.ReadKey();
                        return diaryColumns.type;
                    default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

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
            try
            {
                Notes.Add(newNote);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }
        }


        #region Load/Upload Methods
        
        /// <summary>
        /// Метод загрузки объектов "Заметка" из файла
        /// </summary>
        public void LoadNotes()
        {
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
            using (StreamWriter writeToFile = new StreamWriter(Path, false, Encoding.UTF8))
            {
                foreach (var note in Notes)
                {
                    writeToFile.Write(note.Number + ";" + note.Date + ";" + note.Author + ";" + note.Content + ";" + note.Type);
                    writeToFile.WriteLine();
                }
            }
        }

        #endregion


        #region show

        
        public void ChooseShowMode()
        {
            Console.Clear();
            bool methodWork = true;
            while(methodWork)
            {
                Console.WriteLine("Выберите режим вывода заметок на экран:\n1 - Обычный (по порядковым номерам)\n2 - Пользовательский(сортировка по выбранному полю)\n3 - Пользовательский(фильтрация по выбранному полю)");
                var chooseUser = Console.ReadKey();
                switch (chooseUser.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Вы выбрали обычный режим представления заметок");
                        Console.ReadKey();
                        ShowNotes(Notes);
                        methodWork = false;
                        break;
                    case '2':
                        Console.WriteLine("Вы выбрали пользовательский режим представления заметок (сортировка)");
                        Console.ReadKey();
                        ShowNotes(CreateUserShowModeWithSort(SetSortMode(),ChooseDiaryColumns()));
                        methodWork = false;
                        break;
                    case '3':
                        Console.WriteLine("Вы выбрали пользовательский режим представления заметок (фильтрация)");
                        Console.ReadKey();
                        ShowNotes(CreateUserShowModeWithFilter(ChooseDiaryColumns()));
                        methodWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        /// <summary>
        /// Метод вывода всех заметок на экран консоли
        /// </summary>
        public void ShowNotes(List<Note> Notes)
        {
            Console.Clear();

            string[] titles = { "№ п/п", "Дата заметки", "Автор заметки", "Содержимое заметки", "Категория заметки" };  //объявляем массив с наименованием столбцов

            Console.WriteLine($"{titles[0],5} {titles[1],12} {titles[2],13} {titles[3],-40} {titles[4],17}");

            foreach (var note in Notes)
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


        #endregion

        /*public void ChangeSortMode(displayColumns column)
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Укажите тип сортировки:\n1-По возрастанию\n2-По убыванию");
                var chooseUser = Console.ReadKey();

                SortType? sortBy = chooseUser.KeyChar switch
                {
                    '1' => SortType.ASC;
                    '2' => SortType.DESC;
                    _ => null;
                }

                if (sortBy == null) {
                    Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine($"Вы выбрали сортировку {sortBy}");

                request.Sort = new DiarySort { Column = column, SortBy = sortBy.Value };

                switch (chooseUser.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Вы выбрали сортировку по возрастанию");
                        Console.ReadKey();
                        sortBy == SortType.ASC;
                        break;
                    case '2':
                        Console.WriteLine("Вы выбрали сортировку по убыванию");
                        Console.ReadKey();
                        sortBy == SortType.DESC;
                        break;
                    default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }


                return sortMode;
            }
        }*/

        #region sort&filter monsters

        /// <summary>
        /// Выбор режима сортировки
        /// </summary>
        /// <returns>Возвращает булевое значение 1 - прямая сортировка, 2 - обратная</returns>
        public bool SetSortMode()
        {
            Console.Clear();
            bool methodWork = true;
            bool sortMode = true;
            while (true)
            {
                Console.WriteLine("Укажите тип сортировки:\n1-По возрастанию\n2-По убыванию");
                var chooseUser = Console.ReadKey();
                switch (chooseUser.KeyChar)
                {
                    case '1':
                        Console.WriteLine("Вы выбрали сортировку по возрастанию");
                        Console.ReadKey();
                        sortMode = true;
                        methodWork = false;
                        break;
                    case '2':
                        Console.WriteLine("Вы выбрали сортировку по убыванию");
                        Console.ReadKey();
                        sortMode = false;
                        methodWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
                return sortMode;
            }
        }

        /// <summary>
        /// Метод для сортировки коллекции заметок по заданным пользователем параметрам
        /// </summary>
        /// <param name="sortMode">Прямая или обратная сотировка</param>
        /// <param name="fieldNote">Поле класса "Заметка" по которому будет происходить сортировка</param>
        /// <returns>Отсортированная коллекция</returns>
        public List<Note> CreateUserShowModeWithSort(bool sortMode, diaryColumns fieldNote)
        {
            if (fieldNote == diaryColumns.number)
            {
                if (sortMode) return Notes.OrderBy(n => n.Number).ToList();
                else return Notes.OrderByDescending(n => n.Number).ToList();
            }

            if (fieldNote == diaryColumns.date)
            {
                if (sortMode) return Notes.OrderBy(n => n.Date).ToList();
                else return Notes.OrderByDescending(n => n.Date).ToList();
            }

            if (fieldNote == diaryColumns.author)
            {
                if (sortMode) return Notes.OrderBy(n => n.Author).ToList();
                else return Notes.OrderByDescending(n => n.Author).ToList();
            }

            if (fieldNote == diaryColumns.type)
            {
                if (sortMode) return Notes.OrderBy(n => n.Type).ToList();
                else return Notes.OrderByDescending(n => n.Type).ToList();
            }
            return Notes;
        }

        /// <summary>
        /// Метод фильтрации коллекции заметок по заданным пользователем параметрам
        /// </summary>
        /// <param name="fieldNote">Поле класса "Заметка" по которому будет происходить фильтрация</param>
        /// <returns>Отфильтрованная коллекция</returns>
        public List<Note> CreateUserShowModeWithFilter(diaryColumns fieldNote)
        {
            if (fieldNote == diaryColumns.number)
            {
                bool inputCorrect = false;
                int number = 0;
                while (!inputCorrect)
                {
                    Console.Write("Введите номер заметки:");
                    string inputByUser = Console.ReadLine();
                    inputCorrect = Int32.TryParse(inputByUser, out number);
                    if (!inputCorrect) Console.WriteLine("Некорректный ввод. Необходимо ввести целое число. Попробуйте снова.");
                    Console.ReadKey();
                }

                Console.WriteLine($"Введен номер - {number}. На экран консоли будут выведена заметка с указанным номером...");
                Console.ReadKey();
                return Notes.Where(n => n.Number == number).ToList();
            }

            if (fieldNote == diaryColumns.date)
            {
                bool inputCorrect = false;
                DateTime date = DateTime.MinValue;
                while (!inputCorrect)
                {
                    Console.Write("Введите дату заметки:");
                    string inputByUser = Console.ReadLine();
                    inputCorrect = DateTime.TryParse(inputByUser, out date);
                    if (!inputCorrect) Console.WriteLine("Некорректный ввод. Необходимо ввести дату в формате - ДД.ММ.ГГГГ. Попробуйте снова.");
                    Console.ReadKey();
                }

                Console.WriteLine($"Введена дата - {date}. На экран консоли будут выведены все заметка с указанной датой...");
                Console.ReadKey();
                return Notes.Where(n => n.Date == date).ToList();
            }

            if (fieldNote == diaryColumns.author)
            {
                Console.Write("Введите автора заметки:");
                string author = Console.ReadLine();

                Console.WriteLine($"Введен автор - {author}. На экран консоли будут выведены все заметки указанного автора...");
                Console.ReadKey();
                return Notes.Where(n => n.Author == author).ToList();
            }

            if (fieldNote == diaryColumns.type)
            {
                Console.Write("Введите категорию заметки:");
                string type = Console.ReadLine();

                Console.WriteLine($"Введена категория - {type}. На экран консоли будут выведены все заметки с указанной категорией...");
                Console.ReadKey();
                return Notes.Where(n => n.Type == type).ToList();
            }

            return Notes;

        }
        

        #endregion




        #region del methods

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

        #endregion



    }
}
