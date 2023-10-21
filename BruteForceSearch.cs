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


        public void Start()
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
            } while (GetNextPermutation());


            PrintResult(minPermutation, minPath);
        }


        private int CalculatePathLength()
        {
            int totalLength = 0;
            int prev = _vertex;
            //Iteruje przez każdy wierzchołek i sumuje wagi
            foreach (var t in _permutation)
            {
                totalLength += _matrix.GetWeight(prev, t);
                prev = t;
            }

            // Dodaj wagę krawędzi powrotnej do wierzchołka startowego
            return totalLength + _matrix.GetWeight(prev, _vertex);
        }

        private bool GetNextPermutation()
        {
            // Jak o jeden miejsce dalej jest mniejszy od nastepnego 
            int lastIncreasing = _permutation.Length - 2;
            while (lastIncreasing >= 0 && _permutation[lastIncreasing] >= _permutation[lastIncreasing + 1])
            {
                lastIncreasing--;
            }

            if (lastIncreasing < 0) return false; //jak nie ma to zwraca fałsz

            // Znajdź najmniejszy element w spadającej sekcji, który jest większy niż element pod indeksem lastIncreasing
            int element = Array.FindLastIndex(_permutation, x => x > _permutation[lastIncreasing]);

            // Zamień miejscami te dwa elementy
            int temp = _permutation[lastIncreasing];
            _permutation[lastIncreasing] = _permutation[element];
            _permutation[element] = temp;


            Array.Reverse(_permutation, lastIncreasing + 1, _permutation.Length - lastIncreasing - 1);

            return true;
        }


        private void PrintResult(int[] path, int length)
        {
            Console.WriteLine($"Scieżka: {_vertex} {string.Join(" ", path)} {_vertex}\t Odległość: {length}.");
        }
    }
}
