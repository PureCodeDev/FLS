using System;
using System.Collections;
using System.Collections.Generic;
namespace LAB1
{
    class Program
    {
        static void Main(string[] args)
        {
            RecordBook recordBook = new RecordBook();
            recordBook.DelRecord(5);
            recordBook.AddRecord(new Record("Ivan", "Ivanov", 100, "Russia"));
            recordBook.DelRecord("", "");
            recordBook.AddRecord(new Record("Petr", "Petrov", 200, "Usa"));
            recordBook.AddRecord(new Record("Sidr", "Sidorov", 300, "Ukraine"));

            recordBook.ShowAllRecords();
            recordBook.ShowRecord("Petr", "Petrov");
            Console.WriteLine();

            recordBook.GetRecord("Sidr", "Sidorov").Modify("NoName");
            try { recordBook.GetRecord("Sidr", "Sidorov").Modify("NoName"); }
            catch (NullReferenceException ex) { Console.WriteLine("     Изменяем экземпляр по пустой ссылке!"); }
            recordBook.ShowRecord("Sidr", "Sidorov");
            recordBook.ShowRecord("NoName", "");
            Console.WriteLine();

            recordBook.DelRecord(2);
            recordBook.DelRecord("Ivan", "Ivanov");
            recordBook.ShowAllRecords();

            recordBook.DelAll();
            recordBook.ShowAllRecords();
        }
    }
    public class Record
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int PhoneNumber { get; private set; }
        public string Country { get; private set; }
        public Record(string name = "", string surname = "", int number = 0, string country = "")
        {
            Name = name;
            Surname = surname;
            PhoneNumber = number;
            Country = country;
        }
        public void Modify(string name = "", string surname = "", int number = 0, string country = "")
        {
            Name = name;
            Surname = surname;
            PhoneNumber = number;
            Country = country;
        }
    }
    public class RecordBook
    {
        private List<Record> list = new List<Record>();

        public void DelRecord(string name, string surname)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Записная книжка пустая!");
                return;
            }
            int i = 0;
            while (i != list.Count && list[i].Name != name && list[i].Surname != surname)
                i++;
            if (i == list.Count)
            {
                Console.WriteLine("Такой записи в записной книжке нет!");
                return;
            }
            try { list.RemoveAt(i); }
            catch { Console.WriteLine("Ошибка! Не удалось удалить элемент!"); }
        }
        public void DelRecord(int i)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Записная книжка пустая!");
                return;
            }
            if (i > list.Count - 1)
            {
                Console.WriteLine("Такой записи в записной книжке нет!");
                return;
            }
            try { list.RemoveAt(i); }
            catch { Console.WriteLine("Ошибка! Не удалось удалить элемент!"); }
        }
        public void DelAll()
        {
            list.Clear();
        }

        public void ShowRecord(string name, string surname)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Записная книжка пустая!");
                return;
            }
            int i = 0;
            while (i != list.Count && list[i].Name != name && list[i].Surname != surname)
                i++;
            if (i == list.Count)
            {
                Console.WriteLine("Такой записи в записной книжке нет!");
                return;
            }
            Console.WriteLine($"Была найдена следующая запись:\n Имя: {list[i].Name}\n Фамилия: {list[i].Surname}\n Номер: {list[i].PhoneNumber}\n Страна: {list[i].Country}");
        }
        public void ShowAllRecords()
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Записная книжка пустая!");
                return;
            }
            int i = 1;
            foreach (var record in list)
            {
                Console.WriteLine($"{i}. Была найдена следующая запись:\n Имя: {record.Name}\n Фамилия: {record.Surname}\n Номер: {record.PhoneNumber}\n Страна: {record.Country}");
                i++;
            }
        }

        public Record GetRecord(string name, string surname)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Записная книжка пустая!");
                return null;
            }
            int i = 0;
            while (i != list.Count && list[i].Name != name && list[i].Surname != surname)
                i++;
            if (i == list.Count)
            {
                Console.WriteLine("Такой записи в записной книжке нет!");
                return null;
            }
            return list[i];
        }

        public void AddRecord(string name, string surname, int number, string country)
        {
            list.Add(new Record(name, surname, number, country));
        }
        public void AddRecord(Record record)
        {
            if (record != null)
                list.Add(record);
            else
                Console.WriteLine("Передана пустая ссылка на экземпляр!");
        }
    }
}
