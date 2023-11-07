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
            get => _matrix;
            set => _matrix = value ?? _matrix;
        }
        /// <summary>
        /// Właściwość zwracająca rozmiar
        /// </summary>
        public int Size
        {
            get => _size;
        }

        /// <summary>
        /// Metoda zwracająca odległość między punktami a i b.
        /// </summary>
        /// <param name="a">Punkt a</param>
        /// <param name="b">Punkt b</param>
        /// <returns></returns>
        public int GetWeight(int a, int b) => _matrix[a, b];

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
        /// <summary>
        /// Kopiuje Macierz
        /// </summary>
        /// <param name="currentVertex">obecny wierzchołek na który jesteśmy</param>
        /// <param name="nextVertex">potencjalny kolejny</param>
        /// <returns></returns>
        public Matrix CloneMatrix(int currentVertex, int nextVertex)
        {
            var newMatrixData = (int[,])_matrix.Clone();
            var newMatrix = new Matrix(_size, newMatrixData);

            for (int i = 0; i < _size; i++) //klonujemy i zamieniamy na -1 wiersze i kolumny
            {
                newMatrix.MatrixData[currentVertex, i] = -1;
                newMatrix.MatrixData[i, nextVertex] = -1;
            }
            newMatrix.MatrixData[nextVertex, currentVertex] = -1; //dodatkowo blokujemy droge w drugą stronę

            return newMatrix;
        }

        //Metody do B&B

        /// <summary>
        /// Redukcja wierszy i kolumn
        /// </summary>
        /// <returns>Zwraca koszt z redukowania</returns>
        public int ReduceRowsAndColumns()
        {
            int reductionCost = 0;

            for (int i = 0; i < _size; i++)
            {
                int[] row = GetRow(_matrix, i);
                int rowMin = FindMinimumExcluding(row); 
                if (rowMin > 0)
                {
                    for (int j = 0; j < _size; j++) 
                    {
                        if (_matrix[i, j] != -1) _matrix[i, j] -= rowMin; 
                    }
                    reductionCost += rowMin;
                }
            }

            
            for (int j = 0; j < _size; j++)
            {
                int[] column = GetColumn(_matrix, j);
                int colMin = FindMinimumExcluding(column);
                if (colMin > 0)
                {
                    for (int i = 0; i < _size; i++)
                    {
                        if (_matrix[i, j] != -1) _matrix[i, j] -= colMin; 
                    }
                    reductionCost += colMin;
                }
            }

            return reductionCost;
        }
        /// <summary>
        /// Pomocnicza metoda do wyciągania wierszy 
        /// </summary>
        /// <param name="matrix">Macierz na której pracujemy</param>
        /// <param name="rowNumber">Który wiersz</param>
        /// <returns></returns>
        private int[] GetRow(int[,] matrix, int rowNumber)
        {
            int[] row = new int[matrix.GetLength(1)]; 
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                row[i] = matrix[rowNumber, i];
            }
            return row;
        }

        /// <summary>
        /// Pomocnicza metoda do wyciągania wierszy 
        /// </summary>
        /// <param name="matrix">Macierz na której pracujemy</param>
        /// <param name="columnNumber">Która kolumna</param>
        /// <returns></returns>
        private int[] GetColumn(int[,] matrix, int columnNumber)
        {
            int[] column = new int[matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                column[i] = matrix[i, columnNumber];
            }
            return column;
        }


        /// <summary>
        /// Pomocnicza metoda do szukania minimalnej wartości
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Zwraca min lub 0 w wypadku jakby wszystkie były -1</returns>
        private int FindMinimumExcluding(int[] array)
        {
            int min = int.MaxValue;
            foreach (int value in array)
            {
                if (value != -1 && value < min)
                {
                    min = value;
                }
            }
            return min == int.MaxValue ? 0 : min; 
        }


    }
}