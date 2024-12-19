using System.Text.RegularExpressions;

namespace lab_5_6
{
    internal class Program
    {
        static Random rnd = new Random();

        static string StringInput()
        {
            Console.WriteLine("Введите предложения: ");

            Regex sentenceRegex = new Regex(@"^[0-9A-Za-zА-Яа-яЁё\s,;:.!?]*$");
            bool isMatch;
            string input;

            do
            {
                input = Console.ReadLine();
                isMatch = sentenceRegex.IsMatch(input);

                if (!isMatch)
                {
                    Console.WriteLine("Некорректное предложение... Попробуйте снова");
                }
            } while (!isMatch);

            return input;
        }
        static int ZeroAndPositiveIntegerInput(string message)
        {
            bool isCorrect = false;
            int answer = 0;
            do
            {
                Console.WriteLine(message);
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    if (answer >= 0)
                    {
                        isCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine("Введите число, большее или равное 0");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите целое число");
                    isCorrect = false;
                }
            } while (!isCorrect);

            return answer;
        }
        static int IntegerInput(string message)
        {
            bool isCorrect = false;
            int answer = 0;
            do
            {
                Console.WriteLine(message);
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    isCorrect = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите целое число");
                    isCorrect = false;
                }
            } while (!isCorrect);

            return answer;
        }
        static int NaturalIntegerInput(string message)
        {
            bool isCorrect = false;
            int answer = 0;
            do
            {
                Console.WriteLine(message);
                try
                {
                    answer = int.Parse(Console.ReadLine());
                    if (answer > 0)
                    {
                        isCorrect = true;
                    }
                    else
                    {
                        Console.WriteLine("Введите число, большее 0");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите целое число");
                    isCorrect = false;
                }
            } while (!isCorrect);

            return answer;
        }
        static bool IsEmptyArray(int[,] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("Массив пустой!!!");
                return false;
            }
            return true;
        }

        static bool IsEmptyRagArray(int[][] arr)
        {
            if (arr == null || arr.Length == 0)
            {
                Console.WriteLine("Массив пустой!!!");
                return false;
            }
            return true;
        }

        static void PrintMatrix(int[,] arr)
        {
            if (!IsEmptyArray(arr)) return;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                int cols = arr.GetLength(1);
                Console.Write($"Строка {i + 1, 3}: ");
                for(int j = 0; j < cols; j++)
                {
                    Console.Write($"{arr[i, j], 4}");
                }
                // \n
                Console.WriteLine();
            }
        }

        static int[,] CreateDynamicMatrix()
        {
            int rows = NaturalIntegerInput("Количество строк: ");
            int cols = NaturalIntegerInput("Количество колонок: ");

            int[,] arr = new int[rows, cols];
            int answer;
            answer = ZeroAndPositiveIntegerInput("1. Заполнить с помощью датчика случайных чисел\n" +
            "2. Заполнить вручную\n" +
            "0. ВЫХОД\n");

            switch (answer)
            {
                case 1:
                    for (int i = 0; i < rows; i++)
                    {
                        for(int j = 0; j < cols; j++)
                        {
                            arr[i, j] = rnd.Next(-100, 100);
                        }
                    }

                    PrintMatrix(arr);
                    break;
                case 2:
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            arr[i, j] = IntegerInput($"{i + 1, 3} строка, {j + 1, 3} столбец: ");
                        }
                    }

                    PrintMatrix(arr);
                    break;
                case 0:
                    break;
            }

            return arr;
        }

        static int[,] AddColumnToMatrix(int[,] arr)
        {
            if (!IsEmptyArray(arr)) return arr;

            int[,] newArr = new int[arr.GetLength(0), arr.GetLength(1) + 1];
            // скопировать элементы старого массива в новый
            for(int i = 0; i < arr.GetLength(0); i++)
            {
                for(int j = 0; j < arr.GetLength(1); j++)
                {
                    newArr[i, j + 1] = arr[i, j]; 
                }
            }

            int answer;
            answer = ZeroAndPositiveIntegerInput("1. Заполнить с помощью датчика случайных чисел\n" +
            "2. Заполнить вручную\n" +
            "0. Просто добавить столбец из 0\n");

            switch (answer)
            {
                case 1:
                    for (int i = 0; i < newArr.GetLength(0); i++)
                    {
                        newArr[i, 0] = rnd.Next(-100, 100);
                    }
                    break;
                case 2:
                    for (int i = 0; i < newArr.GetLength(0); i++)
                    {
                        newArr[i, 0] = IntegerInput($"{i + 1, 3} строка, {1} столбец: ");
                    }
                    break;
                case 0:
                    break;
            }


            PrintMatrix(newArr);
            return newArr;
        }

        static void PrintRagArray(int[][] ragArr)
        {
            for(int i = 0; i < ragArr.Length; i++)
            {
                Console.Write($"Строка {i + 1}: ");
                for(int j = 0; j < ragArr[i].Length; j++)
                {
                    Console.Write($"{ragArr[i][j], 4}");
                }
                // \n
                Console.WriteLine();
            }
        }

        static int[][] CreateDynamicRaggedArr()
        {
            int answer = ZeroAndPositiveIntegerInput("1. Заполнить с помощью датчика случайных чисел\n" +
            "2. Заполнить вручную\n" +
            "0. ВЫХОД\n");

            if (answer == 0)
            {
                return null;
            } else if (answer == 1)
            {
                // случайные числа
                int rows = NaturalIntegerInput("Количество строк: ");
                int[][] ragArr = new int[rows][];

                for (int i = 0; i < rows; i++)
                {
                    int currentCols = NaturalIntegerInput($"Кол-во колонок в строке №{i + 1}: ");
                    ragArr[i] = new int[currentCols];

                    for(int j = 0; j < currentCols; j++)
                    {
                        ragArr[i][j] = rnd.Next(-100, 100);
                    }
                }

                PrintRagArray(ragArr);
                return ragArr;
            } 
            else if (answer == 2)
            {
                // пользовательский ввод
                int rows = NaturalIntegerInput("Количество строк: ");
                int[][] ragArr = new int[rows][];

                for (int i = 0; i < rows; i++)
                {
                    int currentCols = NaturalIntegerInput($"Кол-во колонок в строке №{i + 1}: ");
                    ragArr[i] = new int[currentCols];

                    for (int j = 0; j < currentCols; j++)
                    {
                        ragArr[i][j] = IntegerInput($"{i + 1} строка, {j + 1} столбец: ");
                    }
                }

                PrintRagArray(ragArr);
                return ragArr;
            }
            else
            {
                return null;
            }
        }

        static int[][] DeleteFromToRaggedArr(int[][] ragArr)
        {
            if (!IsEmptyRagArray(ragArr)) return ragArr;

            int k1 = NaturalIntegerInput("Удалить начиная со строки номер: ");
            int k2 = NaturalIntegerInput("Удалить заканчивая строкой номер: ");
            if (k1 > k2)
            {
                Console.WriteLine("Мда... Вы ввели нижнюю границу больше верхней, но ничего страшного, мы разберемся :)");
                // поменять местами границы
                int copy_k2 = k2;
                k2 = k1;
                k1 = copy_k2;
            }

            if (k2 - k1 + 1 >= ragArr.Length)
            {
                Console.WriteLine("Вы ввели диапазон, который больше или равен длине массива, мы не будем удалять массив полностью...");
                return ragArr;
            }
            
            int[][] newArr = new int[ragArr.Length - (k2 - k1 + 1)][];
            
            Console.WriteLine(newArr.Length);
            
            int k = 0;
            for(int i = 0; i < ragArr.Length; i++)
            {
                if (i + 1 < k1 || i + 1 > k2)
                {
                    newArr[k] = ragArr[i];
                    k++;
                }
            }

            PrintRagArray(newArr);
            return newArr;
        }

        static string GetStringTest()
        {

            bool isCorrect;
            int answer;
            string test = "";
            Dictionary<int, string> tests = new Dictionary<int, string>();

            tests[1] = "if a then do b";
            tests[2] = "if If then DO null const a var b";
            tests[3] = "в charcharcharcharchar родилась ёлочка";

            Console.WriteLine("Выберите из списка: ");
            for(int i = 1; i < 4; i++)
            {
                Console.WriteLine($"{i}. {tests[i]}");
            }

            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), out answer);
                if (answer < 1 || answer > 3)
                {
                    isCorrect = false;
                    Console.WriteLine("Введите номер из списка!");
                }
                if (!isCorrect)
                {
                    Console.WriteLine("Неверный ввод!");
                } else
                {
                    Console.WriteLine($"Ваш выбор: {tests[answer]}");
                }
            } while (!isCorrect);

            return tests[answer];
        }

        static void DetectCsharpWords(string input)
        {
            if (input == "")
            {
                Console.WriteLine("Строка пустая!");
                return;
            }
            string pattern = @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|while)\b";

            MatchCollection matches = Regex.Matches(input, pattern);

            Dictionary<string, int> keywords = new Dictionary<string, int>();

            foreach (Match match in matches)
            {
                string keyword = match.Value.ToLower();

                if (keywords.ContainsKey(keyword))
                {
                    keywords[keyword]++;
                }
                else
                {
                    keywords[keyword] = 1;
                }
            }

            // Вывод результатов
            if (keywords.Count == 0) {
                Console.WriteLine("Не найдено!");
                return;
            }
            Console.WriteLine("Найдено: ");
            
            foreach (var keyword in keywords)
            {
                Console.WriteLine($"{keyword.Key}: {keyword.Value}");
            }
        }

        static void Main(string[] args)
        {
            int[,] arr = null;
            int[][] ragArr = null;
            string str = "";
            int answer;

            do
            {
                answer = ZeroAndPositiveIntegerInput(
                    "\n1. Сформировать динамический двумерный массив, заполнить его и вывести на печать.\n" +
                    "2. Добавить столбец в начало матрицы\n" +
                    "3. Сформировать динамический рваный массив, заполнить его и вывести на печать\n" +
                    "4. Удалить строки начиная с номера K1 и заканчивая номером К2 включительно\n" +
                    "5. Ввести строку из предложений.\n" +
                    "6. Определить есть ли в строке ключевые слова C#. Если есть, то напечатать сколько раз встречается каждое слово\n" +
                    "7. Получить строку для тестирования\n" +
                    "0. ВЫХОД\n"
                    );
                switch (answer)
                {
                    case 1:
                        arr = CreateDynamicMatrix();
                        break;
                    case 2:
                        arr = AddColumnToMatrix(arr);
                        break;
                    case 3:
                        int[][] res = CreateDynamicRaggedArr();

                        if (res != null)
                        {
                            ragArr = res;
                        }
                        break;
                    case 4:
                        ragArr = DeleteFromToRaggedArr(ragArr);
                        break;
                    case 5:
                        str = StringInput();
                        break;
                    case 6:
                        DetectCsharpWords(str);
                        break;
                    case 7:
                        str = GetStringTest();
                        break;
                }
            } while (answer != 0);
        }
    }
}
