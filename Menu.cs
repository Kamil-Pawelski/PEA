
using System;
using System.Drawing;

namespace ATSP
{
    class Menu
    {

        private static Matrix? matrix = null;
        private static RandomOrFileMatrix randomOrFileMatrix = new (123);

        /// <summary>
        /// Menu 
        /// </summary>
        public static void Main()
        {


            string choice;
            do
            {
                MainMenu();
                choice = Console.ReadLine() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        System.Console.WriteLine("Proszę podać pełną ścieżke pliku: ");
                        // Przykładowa ścieżka pliku, zastąp ścieżką do własnego pliku
                        matrix = randomOrFileMatrix.ReadFile(Console.ReadLine() ?? string.Empty);
                        break;
                    case "2":
                        System.Console.WriteLine("Proszę podać wymiar macierzy do wygenerowania: ");
                        matrix = randomOrFileMatrix.GenerateRandomMatrix(Convert.ToInt32(Console.ReadLine()));

                        break;
                    case "3":
                        if (matrix != null)
                            randomOrFileMatrix.MatrixFileInfo();
                        break;
                    case "4":
                        if (matrix == null)
                            break;
                        matrix.Print();
                        break;
                    case "5":
                        ExecuteAlgorithm();
                        break;
                    case "6":
                        ExecuteAlgorithmMultipleTimes();
                        break;
                    case "7":
                        System.Console.WriteLine("Koniec działania programu.");
                        break;
                    default:
                        System.Console.WriteLine("Nieprawidłowa opcja!");
                        break;
                }
                System.Console.WriteLine();
            } while (choice != "7");

        }
        /// <summary>
        /// Główne menu
        /// </summary>
        public static void MainMenu() => System.Console.WriteLine("Wybierz opcję:\n" +
            "1. Podaj ścieżkę do pliku.\t\t2. Wygeneruj losowe dane.\n" +
            "3. Wyświetl ostatnio wczytane dane.\t4. Wypisz macierz\n" +
            "5. Uruchom wybrany algorytm\t\t6. Uruchom wybrany algorytm x razy.\n" +
            "7. Wyjście z programu");

        /// <summary>
        /// Uruchamia wybrany algorytm raz
        /// </summary>
        public static void ExecuteAlgorithm()
        {
            if (matrix != null)
            {
                AlgorithmMenu();
                string algorithmChoice = Console.ReadLine() ?? string.Empty;
                switch (algorithmChoice)
                {
                    case "1":
                        System.Console.WriteLine("Wskaż, wierzchołek do rozpoczęcia: ");
                        BruteForceSearch bruteForceSearch = new (matrix, Convert.ToInt32(Console.ReadLine()));
                        bruteForceSearch.Search();
                        break;
                    case "2":
                        System.Console.WriteLine("Wskaż, wierzchołek do rozpoczęcia: ");
                        BranchAndBound branchAndBound = new (matrix, Convert.ToInt32(Console.ReadLine()));
                        branchAndBound.CalculatePath();
                        break;
                }
            }
        }
        /// <summary>
        /// Uruchamia wybrany algorytm x razy
        /// </summary>
        public static void ExecuteAlgorithmMultipleTimes()
        {

            AlgorithmMenu();
            string algorithmChoice = Console.ReadLine() ?? string.Empty;
            int loopLength;
            int size;
            int startVertex;
            switch (algorithmChoice)
            {
                case "1":
                    AssignValues(out loopLength, out size, out startVertex, out randomOrFileMatrix, out matrix);
                    BruteForceSearch bruteForceSearch = new (matrix, startVertex);
                    matrix.Print();
                    for (int i = 0; i < loopLength; i++)
                    {
                        bruteForceSearch.Search();
                        matrix = randomOrFileMatrix.GenerateRandomMatrix(size);
                        bruteForceSearch = new (matrix, startVertex);
                    }
                    break;
                case "2":

                    AssignValues(out loopLength, out size, out startVertex, out randomOrFileMatrix, out matrix);
                    BranchAndBound branchAndBound = new (matrix, startVertex);

                    matrix.Print();
                    for (int i = 0; i < loopLength; i++)
                    {
                        branchAndBound.CalculatePath();
                        matrix = randomOrFileMatrix.GenerateRandomMatrix(size);
                        branchAndBound = new BranchAndBound(matrix, startVertex);
                    }
                    break;

            }
        }
        public static void AssignValues(out int loopLength, out int size, out int vertex, out RandomOrFileMatrix randomMatrix, out Matrix generatedMatrix)
        {
            System.Console.WriteLine("Proszę podać wymiar macierzy do wygenerowania: ");
            size = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Ile iteracji?");
            loopLength = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Wskaż, wierzchołek do rozpoczęcia: ");
            vertex = Convert.ToInt32(Console.ReadLine());
            randomMatrix = new RandomOrFileMatrix(123);
            generatedMatrix = randomOrFileMatrix.GenerateRandomMatrix(size);

        }

        /// <summary>
        /// Menu z wyborem algorytmów
        /// </summary>
        public static void AlgorithmMenu() => System.Console.WriteLine("Wybierz opcję:\n1. Przegląd zupełny.\t2. Metoda podziału i ograniczeń.");
    }
}