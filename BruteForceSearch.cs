using System.Text;

namespace ATSP
{
    class BruteForceSearch
    {
        private Matrix _matrix;
        private int _vertex;
        private int[] _permutation;


        public BruteForceSearch(Matrix matrix, int vertex)
        {
            _matrix = matrix;
            _vertex = vertex;
            _permutation = new int[_matrix.Size - 1];
        }


        public void Search()
        {
            int minPath = int.MaxValue;
            int[] minPermutation = new int[_matrix.Size - 1];

            // Inicjalizacja pierwszej permutacji (z wyłączeniem wierzchołka startowego)
            _permutation = Enumerable.Range(0, _matrix.Size).Where(i => i != _vertex).ToArray();

            do
            {
                int currentPathLen = CalculatePathLength();
                if (currentPathLen < minPath)
                {
                    minPath = currentPathLen;
                    Array.Copy(_permutation, minPermutation, _permutation.Length);
                }

                // Jak o jeden miejsce dalej jest mniejszy od nastepnego 
                int lastIncreasing = _permutation.Length - 2;
                while (lastIncreasing >= 0 && _permutation[lastIncreasing] >= _permutation[lastIncreasing + 1])
                {
                    lastIncreasing--;
                }

                if (lastIncreasing < 0) break;  // Jak nie ma to zakończ pętlę do-while

                // Znajdowanie najmniejszieszego elementu w sekcji, który jest większy niż element pod indeksem lastIncreasing
                int element = Array.FindLastIndex(_permutation, x => x > _permutation[lastIncreasing]);

                // Zamiana miejscami
                int temp = _permutation[lastIncreasing];
                _permutation[lastIncreasing] = _permutation[element];
                _permutation[element] = temp;

                Array.Reverse(_permutation, lastIncreasing + 1, _permutation.Length - lastIncreasing - 1);

            } while (true);

            PrintResult(minPermutation, minPath);
        }


        private int CalculatePathLength()
        {
            int total = 0;
            int prev = _vertex;

            for (int i = 0; i < _permutation.Length; i++)
            {
                total += _matrix.GetWeight(prev, _permutation[i]);
                prev = _permutation[i];
            }

            return total + _matrix.GetWeight(prev, _vertex);
        }


        private void PrintResult(int[] path, int length)
        {
            Console.WriteLine($"Scieżka: {_vertex} {string.Join(" ", path)} {_vertex}\t Odległość: {length}.");
        }
    }
}
