namespace ATSP
{
    class Program
    {
        public static void Main()
        {
            RandomOrFileMatrix randomOrFileMatrix = new RandomOrFileMatrix();
            Matrix matrix = null;
            BruteForceSearch bruteForceSearch = null;
            int choice = default;
            int algorithmChoice = default;
            do
            {
                MainMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        System.Console.WriteLine("Proszę podać nazwę pliku: ");
                        var fileName = Console.ReadLine();
                        matrix = randomOrFileMatrix.ReadFile(@"files\" + fileName);
                        break;
                    case 2:
                        System.Console.WriteLine("Proszę podać pełną ścieżke pliku: ");
                        var pathName = Console.ReadLine() ?? String.Empty;
                        matrix = randomOrFileMatrix.ReadFile(pathName);
                        break;
                    case 3:
                        System.Console.WriteLine("Proszę podać wymiar macierzy do wygenerowania: ");
                        matrix = randomOrFileMatrix.GenerateRandomGraph(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 4:
                        if (matrix != null)
                            randomOrFileMatrix.MatrixFileInfo();
                        break;
                    case 5:
                        if (matrix != null)
                        {
                            AlgorithmMenu();
                            algorithmChoice = Convert.ToInt32(Console.ReadLine());
                            switch (algorithmChoice)
                            {
                                case 1:
                                    System.Console.WriteLine("Wskaż, wierzchołek do rozpoczęcia: ");
                                    bruteForceSearch = new BruteForceSearch(matrix, Convert.ToInt32(Console.ReadLine()));
                                    bruteForceSearch.Search();
                                    break;
                            }

                        }
                        break;
                    case 6:
                        if (matrix != null)
                        {
                            AlgorithmMenu();
                            algorithmChoice = Convert.ToInt32(Console.ReadLine());
                            switch (algorithmChoice)
                            {
                                case 1:
                                    System.Console.WriteLine("Ile iteracji?");
                                    int loopLenght = Convert.ToInt32(Console.ReadLine());
                                    System.Console.WriteLine("Wskaż, wierzchołek do rozpoczęcia: ");
                                    int vertex = Convert.ToInt32(Console.ReadLine());
                                    bruteForceSearch = new BruteForceSearch(matrix, vertex);
                                    for (int i = 0; i < loopLenght; i++)
                                    {

                                        bruteForceSearch.Search();
                                    }

                                    break;
                            }

                        }
                        break;
                    case 7:
                        System.Console.WriteLine("Koniec działania programu.");
                        break;
                    default:
                        System.Console.WriteLine("Nieprawidłowa opcja!");
                        break;
                }
                System.Console.WriteLine();
            } while (choice != 7);
        }
        public static void MainMenu()
        {
            System.Console.WriteLine("Wybierz opcję:\n1. Wczytaj dane z pliku (Podanie nazwy pliku).\t2. Wczytaj dane z pliku (Podanie całej ścieżki do pliku).\n3. Wygeneruj losowe dane.\t\t\t4. Wyświetl ostatnio wczytane dane.\n5. Uruchom wybrany algorytm\t\t\t6. Testy\n7. Wyjście z programu");
        }
        public static void AlgorithmMenu()
        {
            System.Console.WriteLine("Wybierz opcję:\n1. Przegląd zupełny.");
        }
    }
}