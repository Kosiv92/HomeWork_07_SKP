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
        public string Value { get; set; }

        public bool filterOn
        {
            get { return filterOn;}
            set
            {
                Console.Clear();
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
                            value = true;
                            methodWork = false;
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine("Вы выключили фильтрацию заметок");
                            Console.ReadKey();
                            value = false;
                            methodWork = false;
                            break;
                        default:
                            Console.WriteLine("Вы нажали неизвестную кнопку. Попробуйте снова...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                    }

                }while (methodWork);
            }
        }

        public string GetValue()
        {
            bool inputCorrect = false;
            
            switch (Column)
            {
                case diaryColumns.number:
                    while (!inputCorrect)
                    {
                        int digit = 0;
                        Console.Write("Введите номер заметки:");
                        string inputByUser = Console.ReadLine();
                        inputCorrect = Int32.TryParse(inputByUser, out digit);
                        if (!inputCorrect)
                        {
                            Console.WriteLine("Некорректный ввод. Необходимо ввести целое число. Попробуйте снова.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine($"Введен номер - {digit}. На экран консоли будут выведена заметка с указанным номером...");
                            Console.ReadKey();
                            return inputByUser;
                        }
                    }
                    break;

                case diaryColumns.date:
                    DateTime date = DateTime.MinValue;
                    while (!inputCorrect)
                    {
                        Console.Write("Введите дату заметки:");
                        string inputByUser = Console.ReadLine();
                        inputCorrect = DateTime.TryParse(inputByUser, out date);
                        if (!inputCorrect) 
                        {
                            Console.WriteLine("Некорректный ввод. Необходимо ввести дату в формате - ДД.ММ.ГГГГ. Попробуйте снова.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine($"Введена дата - {date}. На экран консоли будут выведены все заметка с указанной датой...");
                            Console.ReadKey();
                            return inputByUser;
                        }
                    }
                    break;

                case diaryColumns.author:
                    Console.Write("Введите автора заметки:");
                    string inputAuthor = Console.ReadLine();
                    Console.WriteLine($"Введен автор - {inputAuthor}. На экран консоли будут выведены все заметки указанного автора...");
                    Console.ReadKey();
                    return inputAuthor;
                    break;

                case diaryColumns.type:
                    Console.Write("Введите категорию заметки:");
                    string inputType = Console.ReadLine();
                    Console.WriteLine($"Введена категория - {inputType}. На экран консоли будут выведены все заметки с указанной категорией...");
                    Console.ReadKey();
                    return inputType;
                    break;
            }

            return null;
        }

        public void DoMagic() { }
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
        }

        public void ChangeFilter()
        {
            
            if (Filter.filterOn)
            {
                Filter.Column = Diary.ChooseDiaryColumns();
                Filter.Value = Filter.GetValue();
            }
            
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
