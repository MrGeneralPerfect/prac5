using System;
using System.Collections.Generic;

namespace OfficeEquipment
{
    // Базовый класс для офисного оборудования
    public class OfficeEquip
    {
        public string Model { get; set; } = "Неизвестно"; // Модель оборудования
        private int price; // Цена оборудования
        private int scanSpeed; // Скорость сканирования

        // Свойство для установки и получения цены
        public int Price
        {
            get => price;
            set
            {
                if (value > 0) // Проверка на корректность цены
                    price = value;
                else
                    Console.WriteLine("Некорректная цена");
            }
        }

        // Свойство для установки и получения скорости сканирования
        public int ScanSpeed
        {
            get => scanSpeed;
            set
            {
                if (value > 0 && value <= 200) // Проверка на корректность скорости
                    scanSpeed = value;
                else
                    Console.WriteLine("Некорректная скорость сканирования");
            }
        }

        // Метод для вывода информации об оборудовании
        public virtual void Print()
        {
            Console.WriteLine($"\nМодель: {Model}\nЦена: {Price} рублей\nСкорость сканирования: {ScanSpeed} страниц в минуту");
        }
    }

    // Класс для принтера, наследующий от OfficeEquip
    public class Printer : OfficeEquip
    {
        private int storage; // Ёмкость картриджа

        // Свойство для установки и получения ёмкости
        public int Storage
        {
            get => storage;
            set
            {
                if (value > 500 && value <= 15000) // Проверка на корректность ёмкости
                    storage = value;
                else
                    Console.WriteLine("Некорректное значение ёмкости. Должно быть между 500 и 15000 страниц.");
            }
        }

        // Переопределение метода Print для вывода информации о принтере
        public override void Print()
        {
            base.Print(); // Вызов метода базового класса
            Console.WriteLine($"Ёмкость картриджа: {Storage} страниц");
        }
    }

    // Класс для факса, наследующий от OfficeEquip
    public class Fax : OfficeEquip
    {
        private int phoneLines; // Количество телефонных линий

        // Свойство для установки и получения количества линий
        public int PhoneLines
        {
            get => phoneLines;
            set
            {
                if (value > 0 && value <= 50) // Проверка на корректность количества линий
                    phoneLines = value;
                else
                    Console.WriteLine("Некорректное количество линий. Должно быть между 1 и 50.");
            }
        }

        // Переопределение метода Print для вывода информации о факсе
        public override void Print()
        {
            base.Print(); // Вызов метода базового класса
            Console.WriteLine($"Количество телефонных линий: {PhoneLines}");
        }
    }

    // Основной класс программы
    internal class Program
    {
        static void Main()
        {
            List<OfficeEquip> devices = new List<OfficeEquip>(); // Список для хранения устройств
            char choice;

            // Цикл для отображения меню и обработки выбора пользователя
            while (true)
            {
                ShowMenu(); // Отображение меню
                choice = Console.ReadKey().KeyChar; // Чтение выбора пользователя
                Console.WriteLine();

                if (choice == '5') // Выход из программы
                {
                    break;
                }

                ProcessUserChoice(choice, devices); // Обработка выбора пользователя
            }
        }

        // Метод для отображения меню
        static void ShowMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Доступные действия:");
            Console.WriteLine("1 - Просмотреть все устройства");
            Console.WriteLine("2 - Ввести новое офисное оборудование");
            Console.WriteLine("3 - Ввести новый факс");
            Console.WriteLine("4 - Ввести новый принтер");
            Console.WriteLine("5 - Завершить программу");
            Console.Write("Ваш выбор: ");
        }

        // Метод для обработки выбора пользователя
        static void ProcessUserChoice(char choice, List<OfficeEquip> devices)
        {
            switch (choice)
            {
                case '2':
                    AddOfficeEquip(devices); // Добавление нового офисного оборудования
                    break;
                case '4':
                    AddPrinter(devices); // Добавление нового принтера
                    break;
                case '3':
                    AddFax(devices); // Добавление нового факса
                    break;
                case '1':
                    PrintDevices(devices); // Вывод информации о всех устройствах
                    break;
                default:
                    Console.WriteLine("Некорректный ввод, пробуйте снова.");
                    break;
            }
        }

        // Метод для добавления нового офисного оборудования
        static void AddOfficeEquip(List<OfficeEquip> devices)
        {
            var officeEquip = new OfficeEquip
            {
                Model = GetModel("Введите модель: "),
                Price = GetValidInt("Введите цену: ", 1, 100000),
                ScanSpeed = GetValidInt("Введите скорость сканирования: ", 1, 200)
            };
            devices.Add(officeEquip);
        }

        // Метод для добавления нового принтера
        static void AddPrinter(List<OfficeEquip> devices)
        {
            var printer = new Printer
            {
                Model = GetModel("Введите модель: "),
                Storage = GetValidInt("Введите ёмкость картриджа (страницы): ", 500, 15000),
                Price = GetValidInt("Введите цену: ", 1, 100000),
                ScanSpeed = GetValidInt("Введите скорость сканирования: ", 1, 200)
            };
            devices.Add(printer);
        }

        // Метод для добавления нового факса
        static void AddFax(List<OfficeEquip> devices)
        {
            var fax = new Fax
            {
                Model = GetModel("Введите модель: "),
                Price = GetValidInt("Введите цену: ", 1, 100000),
                ScanSpeed = GetValidInt("Введите скорость сканирования: ", 1, 200),
                PhoneLines = GetValidInt("Введите количество телефонных линий: ", 1, 50)
            };
            devices.Add(fax);
        }

        // Метод для вывода информации о всех устройствах
        static void PrintDevices(List<OfficeEquip> devices)
        {
            if (devices.Count > 0)
            {
                foreach (var device in devices)
                {
                    device.Print(); // Вывод информации о каждом устройстве
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Нет устройств.");
            }
        }

        // Метод для ввода модели
        static string GetModel(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        // Метод для ввода целого числа в заданном диапазоне
        static int GetValidInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод, пробуйте снова.");
                }
            }
        }
    }
}
