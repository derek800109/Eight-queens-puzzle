using System;
using System.Collections.Generic;

namespace queens
{
    class Program
    {
        private const int EmptyType = 0;
        private const int DefaultType = 1;
        private const int QueenType = 2;

        static void Main(string[] args)
        {
            int n;

            try
            {
                n = Int32.Parse(args[0]);
            }
            catch
            {
                n = 8;
            }

            PrintQueens(n);
        }

        public static void PrintQueens(int n)
        {
            int[] table = CreateTable(n);

            HashSet<string> processQueens = new HashSet<string>();

            recursive(table, n, n, processQueens);

            foreach (var processQueen in processQueens)
            {
                Console.WriteLine($"Title:{processQueen}");
                PrintTable(processQueen, n);
            }

            Console.WriteLine($"Total: {processQueens.Count}");
        }

        public static void recursive(
            int[] table,
            int count,
            int n,
            HashSet<string> processQueens)
        {
            if (count == 0)
            {
                // Console.WriteLine($"Count0: {GetArrayString(table, n)}");

                if (IsCompleted(table, n))
                {
                    processQueens.Add(GetArrayString(table, n));
                    // Console.WriteLine($"Complete: {GetArrayString(table, n)}");
                }

                return;
            }

            for (int i = 0; i < n * n; i++)
            {
                if (table[i] == DefaultType)
                {
                    int[] preTable = CopyArray(table, n);

                    EraseEatenCell(table, i, n);

                    // Console.WriteLine($"in. count:{count - 1}, index:{i} {GetArrayString(table, n)}");
                    recursive(table, count - 1, n, processQueens);

                    table = preTable;
                    // Console.WriteLine($"out. count:{count}, index:{i} {GetArrayString(table, n)}");
                }
            }
        }

        public static int[] ToTable(string tableStr, int n)
        {
            int[] table = new int[n * n];

            for (int i = 0; i < n * n; i++)
                table[i] = tableStr[i] - '0';

            return table;
        }

        public static int[] CopyArray(int[] table, int n)
        {
            int[] t = new int[n * n];

            for (int i = 0; i < n * n; i++)
                t[i] = table[i];

            return t;
        }

        public static int GetQueenCount(int[] table, int n)
        {
            int trueCount = 0;

            for (int i = 0; i < n * n; i++)
                if (table[i] == QueenType)
                    trueCount++;

            return trueCount;
        }

        public static bool IsCompleted(int[] table, int n)
        {
            return GetQueenCount(table, n) == n;
        }

        public static void eraseALLEatenCell(int[] table, int n)
        {
            for (int i = 0; i < n * n; i++)
                if (table[i] == DefaultType)
                    EraseEatenCell(table, i, n);
        }

        public static void EraseEatenCell(int[] table, int position, int n)
        {
            int row = position / n;
            int col = position % n;

            int index = 0;

            //
            table[position] = QueenType;

            // erase row
            for (int i = 0; i < n; i++)
            {
                index = row * n + i;
                if (index != position)
                {
                    table[index] = EmptyType;
                    // Console.WriteLine($"row: {index}");
                }
            }

            // erase col
            for (int i = 0; i < n; i++)
            {
                index = i * n + col;
                if (index != position)
                {
                    table[index] = EmptyType;
                    // Console.WriteLine($"row: {index}");
                }
            }

            int moveRow = 0;
            int moveCol = 0;

            // left up
            moveRow = row;
            moveCol = col;
            while (moveRow > 0 && moveCol > 0)
            {
                index = --moveRow * n + --moveCol;
                table[index] = EmptyType;
                // Console.WriteLine($"left up: {index}");
            }

            // right up
            moveRow = row;
            moveCol = col;
            while (moveRow > 0 && moveCol < n - 1)
            {
                index = --moveRow * n + ++moveCol;
                table[index] = EmptyType;
                // Console.WriteLine($"right up: {index}");
            }

            // left down
            moveRow = row;
            moveCol = col;
            while (moveCol > 0 && moveRow < n - 1)
            {
                index = ++moveRow * n + --moveCol;
                table[index] = EmptyType;
                // Console.WriteLine($"left down: {index}");
            }

            // right down
            moveRow = row;
            moveCol = col;
            while (moveRow < n - 1 && moveCol < n - 1)
            {
                index = ++moveRow * n + ++moveCol;
                table[index] = EmptyType;
                // Console.WriteLine($"right down: {index}");
            }
        }

        public static string GetArrayString(int[] array, int n)
        {
            string line = "";

            for (int i = 0; i < n * n; i++)
                line += $"{array[i]}";

            return line;
        }

        public static int[] CreateTable(int n)
        {
            int total = n * n;
            int[] table = new int[total];

            for (int i = 0; i < total; i++)
                table[i] = DefaultType;

            return table;
        }

        public static void PrintTable(string table, int n)
        {
            for (int row = 0; row < n; row++)
            {
                string line = "";

                for (int col = 0; col < n; col++)
                {
                    line += GetChess(table[row * n + col]);
                }

                Console.WriteLine(line);
            }

            Console.WriteLine("");
        }

        public static char GetChess(char value)
        {
            switch (value)
            {
                case '2':
                    return 'Q';
                case '0':
                case '1':
                default:
                    return '.';
            }
        }
    }
}
