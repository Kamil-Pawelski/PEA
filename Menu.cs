namespace ATSP
{
    class Program
    {
        public static void Main()
        {
            RandomOrFileMatrix randomOrFileMatrix = new RandomOrFileMatrix();
            Matrix matrix = null;
            BruteForceSearch bruteForceSearch = null;

            Matrix matrix1 = randomOrFileMatrix.GenerateRandomGraph(100);
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
                        System.Console.WriteLine("Proszę podać wymiar macierzy do wygenerowania: ");
                        matrix = randomOrFileMatrix.GenerateRandomGraph(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 3:
                        if (matrix != null)
                            randomOrFileMatrix.MatrixFileInfo();
                        break;
                    case 4:
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
                }
                System.Console.WriteLine();
            } while (choice != 5);



            // for (int i = 0; i < 10; i++)
            // {
            //     BruteForceSearch bruteForceSearch1 = new BruteForceSearch(matrix1, i);
            //     bruteForceSearch1.Search();
            // }
            //matrix1.Print();
        }
        public static void MainMenu()
        {
            System.Console.WriteLine("Wybierz opcję:\n1. Wczytaj dane z pliku.\t\t2. Wygeneruj losowe dane.\n3. Wyświetl ostatnio wczytane dane.\t4. Uruchom wybrany algorytm\n5. Wyjście z programu");
        }
        public static void AlgorithmMenu()
        {
            System.Console.WriteLine("Wybierz opcję:\n1. Przegląd zupełny.");
        }
    }
}