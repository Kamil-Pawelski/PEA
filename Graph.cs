namespace ATSP
{
    public class Matrix
    {
        private readonly int _size;
        private int[,] _matrix;

        public Matrix(int size, int[,] matrix)
        {
            _matrix = matrix;
            _size = size;
        }

        public int[,] MatrixData
        {
            get { return _matrix; }
            set { if (value != null) _matrix = value; }
        }

        public int Size
        {
            get;
        }

        public int GetWeight(int a, int b)
        {
            return _matrix[a, b];
        }

        public void Print()
        {
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++) Console.Write(_matrix[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
}