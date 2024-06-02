using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace practiceForWeek.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Следующий код отвечает за решение задачи #1
        //Если пользователь нажимает на textbox, очищаю его для ввода значения пользователем
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите число")
            {
                textBox.Text = "";
            }
        }

        //Если пользователь ничего не ввёл, возвращаю ему подсказку в значении textbox, если он что-то ввёл, этот блок кода не меняет значения, введённого пользователем
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите число";
            }
        }

        //Regex выражение для ввода только целых цифр
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); // разрешены только цифры
            return !regex.IsMatch(text);
        }

        //Проверка на простое число
        //Число меньше 2 не является простым
        //Для чисел от 2 до квадратного корня из заданного числа проверяется, делится ли число на текущий делитель.Если делится, число не простое
        //Если делений не найдено, число простое
        private static bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        //Обработчик для кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userInput.Text) || !IsTextAllowed(userInput.Text))
            {
                MessageBox.Show("Пожалуйста, введите целое число.");
                return;
            }

            int number = Convert.ToInt32(userInput.Text);
            bool isEven = number % 2 == 0;
            string parity = isEven ? "чётным" : "нечётным";

            string primality;
            if (number == 1)
            {
                userOutput.Width = 500;
                primality = "не является ни простым, ни составным";
            }
            else
            {
                userOutput.Width = 240;
                bool isPrime = IsPrime(number);
                primality = isPrime ? "простым" : "составным";
            }

            userOutput.Content = $"{parity} и {primality}";
        }

        // Следующий код отвечает за решение задачи #2

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(twoNumbers.Text))
            {
                MessageBox.Show("Пожалуйста, введите два числа через пробел.");
                return;
            }

            var numbers = twoNumbers.Text.Split(' ');
            if (numbers.Length != 2 || !IsTextAllowed(numbers[0]) || !IsTextAllowed(numbers[1]))
            {
                MessageBox.Show("Пожалуйста, введите два целых числа через пробел.");
                return;
            }

            int num1 = Convert.ToInt32(numbers[0]);
            int num2 = Convert.ToInt32(numbers[1]);

            int gcd = GCD(num1, num2);
            int lcm = LCM(num1, num2);

            divider.Content = $"НОК: {lcm}, НОД: {gcd}";
        }

        // Метод для вычисления НОД (наибольшего общего делителя)
        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Метод для вычисления НОК (наименьшего общего кратного)
        private static int LCM(int a, int b)
        {
            return (a / GCD(a, b)) * b;
        }


        // Следующий код отвечает за решение задачи #3

        private void TextBox_GotFocus_3(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите предложение")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_3(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите предложение";
            }
        }

        private void ProcessSentence_Click(object sender, RoutedEventArgs e)
        {
            string sentence = sentenceInput.Text;

            if (string.IsNullOrWhiteSpace(sentence) || sentence == "Введите предложение")
            {
                MessageBox.Show("Пожалуйста, введите предложение.");
                return;
            }

            // Регулярное выражение для разделения строки на слова
            Regex regex = new Regex(@"\b\w+\b");

            // Используем регулярное выражение для разделения строки на слова
            string[] words = regex.Matches(sentence)
                                  .Cast<Match>()
                                  .Select(match => match.Value)
                                  .ToArray();

            // Сортируем слова в алфавитном порядке с заглавной буквы
            words = words.Select(w => char.ToUpper(w[0]) + w.Substring(1))
                         .OrderBy(w => w)
                         .ToArray();

            // Удаляем дубликаты слов
            words = words.Distinct().ToArray();

            // Отображаем результаты
            wordCount.Content = words.Length.ToString();
            sortedWords.Text = string.Join(", ", words);
        }

        // Следующий код отвечает за решение задачи 4

        private void TextBox_GotFocus_4_Text(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите текст")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_4_Text(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите текст";
            }
        }

        private void TextBox_GotFocus_4_Word(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите слово для поиска")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_4_Word(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите слово для поиска";
            }
        }

        private void CountWordOccurrences_Click(object sender, RoutedEventArgs e)
        {
            string text = txtInput.Text;
            string word = wordsInput.Text;

            if (string.IsNullOrWhiteSpace(text) || text == "Введите текст" ||
                string.IsNullOrWhiteSpace(word) || word == "Введите слово для поиска")
            {
                MessageBox.Show("Пожалуйста, введите текст и слово для поиска.");
                return;
            }

            // Привести текст и слово к нижнему регистру для учета регистра
            text = text.ToLower();
            word = word.ToLower();

            // Удалить специальные символы
            text = Regex.Replace(text, @"[^\w\s]", "");
            word = Regex.Replace(word, @"[^\w\s]", "");

            // Использовать регулярное выражение для поиска слова
            int count = Regex.Matches(text, $@"\b{Regex.Escape(word)}\b").Count;

            // Отобразить результат
            wordCountResult.Content = count.ToString();
        }

        // Следующий код отвечает за решение задачи 5

        private void TextBox_GotFocus_5(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите число N")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_5(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите число N";
            }
        }

        private void FindPalindromes_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(numberInput.Text, out int N) || N > 100 || N < 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное число N (0 <= N <= 100).");
                return;
            }

            var palindromes = Enumerable.Range(0, N + 1)
                                        .Where(IsPalindrome)
                                        .Select(num => num.ToString())
                                        .ToList();

            palindromeResults.Text = string.Join(", ", palindromes);
        }

        private bool IsPalindrome(int number)
        {
            var str = number.ToString();
            return str == new string(str.Reverse().ToArray());
        }

        // Следующий код отвечает за решение задачи 6

        private void TextBox_GotFocus_6_Capacity(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите грузоподъемность")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_6_Capacity(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите грузоподъемность";
            }
        }

        private void TextBox_GotFocus_6_Items(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Введите параметры вещей")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus_6_Items(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Введите параметры вещей";
            }
        }

        private void FillKnapsack_Click(object sender, RoutedEventArgs e)
        {
            // Проверка корректности ввода грузоподъемности
            if (!int.TryParse(capacityInput.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректную грузоподъемность.");
                return;
            }

            // Разделение ввода данных о вещах на строки
            var items = itemsInput.Text
                .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(' '))
                .ToList();

            var validItems = new System.Collections.Generic.List<(int weight, int value)>();

            foreach (var parts in items)
            {
                // Проверка корректности ввода параметров вещей
                if (parts.Length != 2 ||
                    !int.TryParse(parts[0], out int weight) || weight <= 0 ||
                    !int.TryParse(parts[1], out int value) || value <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректные параметры вещей (вес и стоимость должны быть положительными числами).");
                    return;
                }

                validItems.Add((weight, value));
            }

            // Решение задачи о рюкзаке
            int n = validItems.Count;
            int[,] dp = new int[n + 1, capacity + 1];

            for (int i = 1; i <= n; i++)
            {
                for (int w = 1; w <= capacity; w++)
                {
                    if (validItems[i - 1].weight <= w)
                    {
                        dp[i, w] = Math.Max(dp[i - 1, w], dp[i - 1, w - validItems[i - 1].weight] + validItems[i - 1].value);
                    }
                    else
                    {
                        dp[i, w] = dp[i - 1, w];
                    }
                }
            }

            // Получение максимальной стоимости
            int maxValue = dp[n, capacity];
            maxValueResult.Content = maxValue.ToString();

            // Получение выбранных вещей
            var selectedItemsList = new System.Collections.Generic.List<string>();
            int remainingCapacity = capacity;
            int filledWeight = 0;
            for (int i = n; i > 0 && remainingCapacity > 0; i--)
            {
                if (dp[i, remainingCapacity] != dp[i - 1, remainingCapacity])
                {
                    selectedItemsList.Add($"Вещь {i} (вес: {validItems[i - 1].weight}, стоимость: {validItems[i - 1].value})");
                    remainingCapacity -= validItems[i - 1].weight;
                    filledWeight += validItems[i - 1].weight;
                }
            }

            selectedItems.Text = string.Join("\n", selectedItemsList);
            maxkgResult.Content = $"{filledWeight}/{capacity} кг";
        }

        // Следующий код отвечает за решение задачи 7
        private void AnalyzeCollection_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный тип коллекции
            string collectionType = ((ComboBoxItem)collectionTypeComboBox.SelectedItem).Content.ToString();

            // Получаем количество элементов
            if (!int.TryParse(elementCountInput.Text, out int elementCount) || elementCount <= 0)
            {
                MessageBox.Show("Пожалуйста, введите корректное количество элементов.");
                return;
            }

            // Анализируем коллекцию
            string results = AnalyzeCollectionPerformance(collectionType, elementCount);

            // Выводим результаты анализа
            analysisResults.Text = results;
        }

        private string AnalyzeCollectionPerformance(string collectionType, int elementCount)
        {
            var rand = new Random();
            var stopwatch = new Stopwatch();
            string results = $"Анализ коллекции типа {collectionType} с {elementCount} элементами:\n\n";

            switch (collectionType)
            {
                case "List":
                    var list = new List<int>();

                    // Add
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        list.Add(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Добавление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Insert
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        list.Insert(rand.Next(list.Count), rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Вставка: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Remove
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        list.Remove(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Удаление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Find
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        list.Contains(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Поиск: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Sort
                    stopwatch.Start();
                    list.Sort();
                    stopwatch.Stop();
                    results += $"Сортировка: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();
                    break;

                case "Dictionary":
                    var dictionary = new Dictionary<int, int>();

                    // Add
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        dictionary[i] = rand.Next();
                    }
                    stopwatch.Stop();
                    results += $"Добавление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Remove
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        dictionary.Remove(i);
                    }
                    stopwatch.Stop();
                    results += $"Удаление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Find
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        dictionary.ContainsKey(rand.Next(elementCount));
                    }
                    stopwatch.Stop();
                    results += $"Поиск: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();
                    break;

                case "Queue":
                    var queue = new Queue<int>();

                    // Enqueue
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        queue.Enqueue(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Добавление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Dequeue
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        queue.Dequeue();
                    }
                    stopwatch.Stop();
                    results += $"Удаление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Contains
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        queue.Contains(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Поиск: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();
                    break;

                case "Stack":
                    var stack = new Stack<int>();

                    // Push
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        stack.Push(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Добавление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Pop
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        stack.Pop();
                    }
                    stopwatch.Stop();
                    results += $"Удаление: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();

                    // Contains
                    stopwatch.Start();
                    for (int i = 0; i < elementCount; i++)
                    {
                        stack.Contains(rand.Next());
                    }
                    stopwatch.Stop();
                    results += $"Поиск: {stopwatch.ElapsedMilliseconds} мс\n";
                    stopwatch.Reset();
                    break;
            }
            // Возвращаем значение
            return results;
        }
    }
}
