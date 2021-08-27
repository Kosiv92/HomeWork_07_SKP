using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
             
        public Note this[int index]
        {
            get{ return Notes[index]; }
            set{ Notes[index] = value; }
            }

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
        /// Метод выбора номера заявки
        /// </summary>
        /// <returns>Возвращает целое число</returns>
        public static int ChooseNumberOfNote()
        {
            bool methodWork = false;

            int numberOfNote = 0;

            do{
                Console.WriteLine();
                Console.WriteLine("Укажите номер заметки для редактирования:");
                var inputByUser = Console.ReadLine();
                methodWork = Int32.TryParse(inputByUser, out numberOfNote);
                methodWork = numberOfNote > 0;
                if(!methodWork) Console.WriteLine("Введено некорректное значение. Номер должен быть больше нуля. Попробуйте снова...");

                }while(!methodWork);
            
            return numberOfNote;
            
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

        public void ChangeNumber()
        {
            for(int i = 0; i < Notes.Count; i++)
            {
                Notes[i].ChangeNumberOfNote(Notes[i], i);
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

        public void LoadNotesByDates()
        {
            string[] lines = File.ReadAllLines(Path);   //объявление массива всех строк файла
            
            DatesPeriod dateLoad = new DatesPeriod();

            int notesCount = 1;   //счетчик количества загруженных заметок

            if (lines.Length != 0)
            {
                using (StreamReader diaryReader = new StreamReader(Path))
                {
                    while (!diaryReader.EndOfStream)
                    {
                        string rowFromFile = diaryReader.ReadLine();   //объявление переменной для хранения данных строки из файла 
                                                
                        Note newNote = Note.Parse(rowFromFile, notesCount);
                        
                        if(newNote.Date >= dateLoad.StartDate && newNote.Date <= dateLoad.EndDate) 
                        {
                            AddNote(newNote);
                            notesCount++;
                        }                                     
                                               
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
                Console.WriteLine("Выберите режим вывода заметок на экран:\n1 - Обычный (по порядковым номерам)\n2 - Пользовательский");
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
                        
                        DiaryRequest requestByUser = new DiaryRequest();
                        
                        requestByUser.Filter.turnFilter();

                        ShowNotes(GetNotes(requestByUser));
                        
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

        #region sort&filter monsters

        /// <summary>
        /// Метод сортировки и фильтрации коллекции заметок
        /// </summary>
        /// <param name="request">Объект класса с указанием настроек фильтрации и сортировки</param>
        /// <returns>коллекция заметок после преобразования</returns>
        public List<Note> GetNotes(DiaryRequest request)
        {
            List<Note> displayNotes = new List<Note>();
            
            if(request.Filter.FilterEnabled)
            {
                switch(request.Filter.Column)
                {
                    case diaryColumns.number:
                    int digit = (int)request.Filter.Value;
                    displayNotes = Notes.Where(n => n.Number == digit).ToList();
                    break;
                    case diaryColumns.date:
                    DateTime date = (DateTime)request.Filter.Value;
                    displayNotes = Notes.Where(n => n.Date == date).ToList();
                    break;
                    case diaryColumns.author:
                    string author = (string)request.Filter.Value;
                    displayNotes = Notes.Where(n => n.Author == author).ToList();   
                        break;
                        case diaryColumns.type:
                    string type = (string)request.Filter.Value;
                    displayNotes = Notes.Where(n => n.Type == type).ToList();   
                        break;
                default:
                    break;
                }                  
            }else displayNotes = Notes;

            if(request.Sort.SortBy == DiarySort.SortType.ASC) displayNotes = displayNotes.OrderBy(n => request.Sort.SortBy).ToList();
            else displayNotes = displayNotes.OrderByDescending(n => request.Sort.SortBy).ToList();

            return displayNotes;
        }
          

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
        /// Метод ввода значения поля пользователем
        /// </summary>
        /// <returns>Возвращает значение одного из полей заметки</returns>
        static public object GetValue(diaryColumns Column)
        {            
            bool inputCorrect = false;
            object result;

            do
            {
                Console.WriteLine();
                Console.Write("Введите значение: ");

                string inputByUser = Console.ReadLine();

                Console.WriteLine();

                switch (Column)
                {
                    case diaryColumns.number:                        
                        result = ChooseNumberOfNote();
                        break;
                        
                    case diaryColumns.date:
                        inputCorrect = DateTime.TryParse(inputByUser, out DateTime date);
                        if (!inputCorrect)
                        {
                            Console.WriteLine(
                                "Некорректный ввод. Необходимо ввести дату в формате - ДД.ММ.ГГГГ. Попробуйте снова.");                                                       
                        }
                        result = date;;
                        break;

                    default:
                        inputCorrect = true;
                        result = inputByUser;
                        break;
                    }
            } while (!inputCorrect);
            return result;
        }

        #endregion

        #region add/del methods
               
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

        /// <summary>
        /// Метод удаления заметок по пользовательским настройкам
        /// </summary>
        /// <param name="diary"></param>
        /// <returns>Возвращает список заметок оставшихся после удаления</returns>
        public void DeleteNotes(DiaryTerminator diaryTerminator)
            {
            
            List<Note> changedNotes = new List<Note>();

            switch(diaryTerminator.Column)
                {
                case diaryColumns.number:
                    int digit = (int)diaryTerminator.Value;
                    changedNotes = Notes.Where(n => n.Number != digit).ToList();
                    break;
                case diaryColumns.date:
                    DateTime date = (DateTime)diaryTerminator.Value;
                    changedNotes = Notes.Where(n => n.Date != date).ToList();
                    break;
                case diaryColumns.author:
                    string author = (string)diaryTerminator.Value;
                    changedNotes = Notes.Where(n => n.Author != author).ToList();
                    break;
                    case diaryColumns.type:
                    string type = (string)diaryTerminator.Value;
                    changedNotes = Notes.Where(n => n.Type != type).ToList(); 
                    break;
                }

            Notes = changedNotes;

            ChangeNumber();
        }
        

        #endregion



    }
}
