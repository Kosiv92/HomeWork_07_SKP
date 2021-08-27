using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_SKP
{

    public enum diaryColumns : byte
    {
        number,
        date,
        author,
        contentNote,
        type
    }

    public class DiaryFilter
    {
        public diaryColumns Column { get; set; }
        public object Value { get; set; }

        public bool FilterEnabled { private set; get; }
        
        /// <summary>
        /// Метод выбора настроек фильтрации пользователем
        /// </summary>
        public void turnFilter()
        {
                Console.Clear();
                bool filterEnabled = false;
                bool methodWork = true;
                do
                {
                Console.WriteLine("Включить фильтр?:\n1-Да\n2-Нет");
                    var chooseUser = Console.ReadKey();
                    switch (chooseUser.Key)
                    {
                        case ConsoleKey.D1:
                            Console.WriteLine("Вы включили фильтрацию заметок");
                            Console.ReadKey();
                            filterEnabled = true;
                            methodWork = false;
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("Вы выключили фильтрацию заметок");
                            Console.ReadKey();
                            filterEnabled = false;
                            methodWork = false;
                            break;
                        default:
                            Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }

                }while (methodWork);
            
            FilterEnabled = filterEnabled;

        }
                
    }

    public class DiarySort
    {

        public enum SortType
        {
            [Description("По возрастанию")]
            ASC,
            [Description("По убыванию")]
            DESC,
        }
        public diaryColumns Column { get; set; }
        public SortType SortBy { get; set; }
    }

    public class DiaryRequest : IPaginationRequest
    {
        public DiaryFilter Filter { get; set; }
        public DiarySort Sort { get; set; }
        public int CountByPage { get; set; }
        public int CurentPage { get; set; }

        public DiaryRequest()
        {
            Sort = new DiarySort();

            Filter = new DiaryFilter();
        }

        /// <summary>
        /// Изменение значения сортировки
        /// </summary>
        public void ChangeSort()
        {
            Console.Clear();
            bool methodWork = true;
            do
            {
                Console.WriteLine("Укажите тип сортировки:\n1-По возрастанию\n2-По убыванию");
                var chooseUser = Console.ReadKey();
                switch (chooseUser.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Вы выбрали сортировку по возрастанию");
                        Console.ReadKey();
                        Sort.SortBy = DiarySort.SortType.ASC;
                        methodWork = false;
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine("Вы выбрали сортировку по убыванию");
                        Console.ReadKey();
                        Sort.SortBy = DiarySort.SortType.DESC;
                        methodWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (methodWork);

            Sort.Column = Diary.ChooseDiaryColumns();
        }

        public void ChangeFilter()
        {
            
            if (Filter.FilterEnabled)
            {
                Filter.Column = Diary.ChooseDiaryColumns();
                Filter.Value = Diary.GetValue(Filter.Column);
            }
            
        }

    }

    public class DatesPeriod
        {
        public DateTime StartDate{ get; private set; }

        public DateTime EndDate{ get; private set; }

        /// <summary>
        /// Конструктор объекта с установкой значений свойств пользователем
        /// </summary>
        public DatesPeriod()
        { 
            DateTime startDate;
            DateTime endDate;
            
            Console.WriteLine("Введите период создания заметок");
            bool correctInput = false;

            do
            {
                do
                {
                Console.WriteLine("Введите начальную дату в формате - ДД.ММ.ГГГГ: ");
                string inputByUser = Console.ReadLine();
                correctInput = DateTime.TryParse(inputByUser, out startDate);
                if(!correctInput)
                    {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова...");
                    Console.ReadKey();
                    continue;
                    }

                } while (!correctInput);

            correctInput = false;
            do
                {
            Console.WriteLine("Введите конечную дату в формате - ДД.ММ.ГГГГ: ");
                string inputByUser = Console.ReadLine();
                correctInput = DateTime.TryParse(inputByUser, out endDate);
                if(!correctInput)
                    {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова...");
                    Console.ReadKey();
                    continue;
                    }
                } while (!correctInput);

                if(endDate < startDate)
                { 
                    correctInput = false;
                    Console.WriteLine("Конечная дата не может быть раньше начальной. Повторите ввод начальной и конечной даты");
                    Console.ReadKey();                    
                    }


            }while(!correctInput);
            
            StartDate = startDate;

            EndDate = endDate;

        }

        }

    class DiaryTerminator
    {
        public diaryColumns Column { get; set; }
        public object Value { get; set; }

        public DiaryTerminator(Diary diary)
            {
            Column = Diary.ChooseDiaryColumns();

            Value = Diary.GetValue(Column);       
                        
            }

        
    }

    public interface IPaginationRequest
    {
        /// <summary>
        /// Кол-во элементов на странице
        /// </summary>
        int CountByPage { get; set; }
        /// <summary>
        /// Текущая страница
        /// </summary>
        int CurentPage { get; set; }
    }

}
