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

        public BruteForceSearch(Matrix matrix, int vertex)
        {
            _matrix = matrix;
            _vertex = vertex;
            _permutation = Enumerable.Range(0, _matrix.Size).Where(i => i != _vertex).ToArray();
            _bestPermutation = new int[_matrix.Size - 1];
            _minPathLength = int.MaxValue;
        }

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

        private void GenerateNextPermutation()
        {
            int lastIncreasing = -1;
            for (int i = _permutation.Length - 2; i >= 0; i--)
            {
                if (_permutation[i] < _permutation[i + 1])
                {
                    lastIncreasing = i;
                    break;
                }
            }

            if (lastIncreasing == -1) return;

            int swapWith = -1;
            for (int i = _permutation.Length - 1; i > lastIncreasing; i--)
            {
                if (_permutation[i] > _permutation[lastIncreasing])
                {
                    swapWith = i;
                    break;
                }
            }

            Swap(ref _permutation[lastIncreasing], ref _permutation[swapWith]);
            Array.Reverse(_permutation, lastIncreasing + 1, _permutation.Length - lastIncreasing - 1);
        }

        public void Search()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            do
            {
                int currentPathLen = CalculatePathLength();
                if (currentPathLen < _minPathLength)
                {
                    _minPathLength = currentPathLen;
                    Array.Copy(_permutation, _bestPermutation, _permutation.Length);
                }

                if (IsLastPermutation()) break;

                GenerateNextPermutation();

            } while (true);
            stopwatch.Stop();
            var elapsedTime = stopwatch.ElapsedMilliseconds;
            PrintResult(_bestPermutation, _minPathLength, elapsedTime);
            string filePath = "wyniki.csv";
            string delimiter = ";";
            string newLine = Environment.NewLine;

            // Jeśli plik nie istnieje, dodaj nagłówek
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"Operacja{delimiter}Czas (ms){newLine}");
            }

            // Dopisz wynik do pliku
            File.AppendAllText(filePath, $"Wynik{delimiter}{elapsedTime}{newLine}");

            _permutation = Enumerable.Range(0, _matrix.Size).Where(i => i != _vertex).ToArray();
            _bestPermutation = new int[_matrix.Size - 1];
            _minPathLength = int.MaxValue;
        }



        private void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        private int CalculatePathLength()
        {
            int total = _vertex;
            int sum = 0;

            for (int i = 0; i < _permutation.Length; i++)
            {
                sum += _matrix.GetWeight(total, _permutation[i]);
                total = _permutation[i];
            }

            sum += _matrix.GetWeight(total, _vertex);
            return sum;
        }

        private void PrintResult(int[] path, int length, long elapsedTime)
        {
            Console.WriteLine($"Scieżka: {_vertex} {string.Join(" ", path)} {_vertex}\t\tOdległość: {length}.\tCzas potrzebny do wykonania: {elapsedTime}(ms)");
        }
    }
}
