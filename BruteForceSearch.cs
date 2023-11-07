using System;
using System.Diagnostics;
namespace ATSP
{
    class BruteForceSearch
    {
        private Matrix _matrix;
        private int _vertex;
        private int[] _permutation;
        private int[] _bestPermutation;
        private int _minPathLength;
        /// <summary>
        /// Konstruktor tworzący objekt umożliwiający zastosowanie algorytmu przeglądu zupełnego
        /// </summary>
        /// <param name="matrix">Macierz</param>
        /// <param name="vertex">Wybrany wierzchołek</param>
        public BruteForceSearch(Matrix matrix, int vertex)
        {
            _matrix = matrix;
            _vertex = vertex;
            List<int> permutation = new List<int>();
            for (int i = 0; i < _matrix.Size; i++)
            {
                if (i != _vertex)
                    permutation.Add(i);
            }
            _permutation = permutation.ToArray();
            _bestPermutation = new int[_matrix.Size - 1];
            _minPathLength = int.MaxValue;
        }
        /// <summary>
        /// Inicjalizacja pewnych pól klasy na nowo.
        /// </summary>
        private void Initialize() 
        {
            List<int> permutation = new List<int>(); 
            for (int i = 0; i < _matrix.Size; i++)
            {
                if (i != _vertex)
                    permutation.Add(i);
            }
            _permutation = permutation.ToArray();
            _bestPermutation = new int[_matrix.Size - 1];
            _minPathLength = int.MaxValue;
        }


        /// <summary>
        /// Metoda umożliwiająca wykonanie przeglądu zupełnego
        /// </summary>
        public void Search()
        {
            var stopwatch = new Stopwatch(); //mierzenie czasu
            stopwatch.Start();

            do
            {
                int currentPathLen = CalculatePathLength(); //obliczanie ścieżki
                if (currentPathLen < _minPathLength) //sprawdzenie czy będzie krótsza
                {
                    _minPathLength = currentPathLen;
                    Array.Copy(_permutation, _bestPermutation, _permutation.Length);
                }

                if (IsLastPermutation()) break; //zakonczenie dzialania petli w przypadku ostatnie permutacji

                GenerateNextPermutation(); //generowanie kolejnej permutacji w innym wypadku
            } while (true);

            stopwatch.Stop();
            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;

            PrintResult(_bestPermutation, _minPathLength, elapsedTime);

            SaveResultToFile(elapsedTime);

           // Initialize();
        }
        /// <summary>
        /// Zapisuje dla danego rozmiaru macierzy czas potrzebny do wykonania 
        /// </summary>
        /// <param name="elapsedTime"></param>
    

        /// <summary>
        /// Sprawdzenie czy mamy jeszcze permutacje do wykonania
        /// </summary>
        /// <returns>Zwraca false jak mamy, jeśli nie mamy zwraca true</returns>
        private bool IsLastPermutation()
        {
            for (int i = 0; i < _permutation.Length - 1; i++)
            {
                if (_permutation[i] < _permutation[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Generowanie kolejnej permutacji
        /// </summary>
        private void GenerateNextPermutation()
        {
            int swapStart = -1;     // Szukamy elementu od KOŃCA który będzie mniejszy od następnego 
            for (int i = _permutation.Length - 2; i >= 0; i--)
            {
                if (_permutation[i] < _permutation[i + 1])
                {
                    swapStart = i;
                    break;
                }
            }

            if (swapStart == -1) return; // Jeśli nie znaleziono to kończenie działania

            int swapWith = -1;  // Szukamy elementu większego od elementu znalezionego poprzednio w pętli
            for (int i = _permutation.Length - 1; i > swapStart; i--)
            {
                if (_permutation[i] > _permutation[swapStart])
                {
                    swapWith = i;
                    break;
                }
            }


            Swap(ref _permutation[swapStart], ref _permutation[swapWith]); // Zamieniami miejscami elementy znalezione
            Array.Reverse(_permutation, swapStart + 1, _permutation.Length - swapStart - 1); // Odwrócenie części tablicy do uzyskania prawidłowej kolejności
        }
        /// <summary>
        /// Zapisanie wyników do pliku
        /// </summary>
        /// <param name="elapsedTime">Czas wykonania operacji</param>
        private void SaveResultToFile(double elapsedTime)
        {
            string filePath = "wyniki.csv";
            string delimiter = ";";
            string newLine = Environment.NewLine;

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"Operacja{delimiter}Czas (ms){newLine}");
            }

            File.AppendAllText(filePath, $"Wynik{_matrix.Size}{delimiter}{elapsedTime}{newLine}");
        }


        /// <summary>
        ///  Zamienienie elementów miedzy sobą
        /// </summary>
        /// <param name="a"> Pierwszy element do zamiany </param>
        /// <param name="b"> Drugi element do zamiany</param>
        private void Swap(ref int a, ref int b)
        {
            (a, b) = (b, a);
        }

        /// <summary>
        /// Obliczanie całkowitej trasy od wierzchołka
        /// </summary>
        /// <returns></returns>
        private int CalculatePathLength()
        {
            int currentVertex = _vertex; //inicjalizacja pól
            int sum = 0;

            for (int i = 0; i < _permutation.Length; i++) // Iteracja po wierzchołkach
            {
                sum += _matrix.GetWeight(currentVertex, _permutation[i]); // Suma pomiędzy odległościami
                currentVertex = _permutation[i]; // Zmiana wierzchołka
            }

            sum += _matrix.GetWeight(currentVertex, _vertex);
            return sum;
        }

        /// <summary>
        /// WYpisanie wyniku algorytmu
        /// </summary>
        /// <param name="path">Ścieżka przez którą się przeszło</param>
        /// <param name="length">Odgległość</param>
        /// <param name="elapsedTime">Czas potrzebny do wykonania</param>
        private void PrintResult(int[] path, int length, double elapsedTime) => Console.WriteLine($"Scieżka: {_vertex} {string.Join(" ", path)} {_vertex}\t\t" +
                                                                                                  $"Odległość: {length}.\tCzas potrzebny do wykonania: {elapsedTime}(ms)");
    }
}
