using System;
using System.Linq;

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

            PrintResult(_bestPermutation, _minPathLength);
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

        private void PrintResult(int[] path, int length)
        {
            Console.WriteLine($"Scieżka: {_vertex} {string.Join(" ", path)} {_vertex}\t Odległość: {length}.");
        }
    }
}
