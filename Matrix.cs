namespace ATSP
{
    public class Matrix
    {
        private int[,] _matrix;
        private int _size;

        /// <summary>
        /// Konstruktor inicjalizuje pola
        /// </summary>
        /// <param name="size"></param>
        /// <param name="matrix"></param>
        public Matrix(int size, int[,] matrix)
        {
            _matrix = matrix;
            _size = size;
        }

        /// <summary>
        /// Właściwość zwracająca lub ustawiająca macierz
        /// </summary>
        public int[,] MatrixData
        {
            get { return _matrix; }
            set { if (value != null) _matrix = value; }
        }
        /// <summary>
        /// Właściwość zwracająca rozmiar
        /// </summary>
        public int Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Metoda zwracająca odległość między punktami a i b.
        /// </summary>
        /// <param name="a">Punkt a</param>
        /// <param name="b">Punkt b</param>
        /// <returns></returns>
        public int GetWeight(int a, int b)
        {
            return _matrix[a, b];
        }

        /// <summary>
        /// Wypisanie macierzy
        /// </summary>
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