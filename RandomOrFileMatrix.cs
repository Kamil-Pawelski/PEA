using System;
using System.Data.Common;
using System.IO;

namespace ATSP
{

    public class RandomOrFileMatrix
    {

        private int _dimension;
        private int _result;
    
        public int Dimension
        {
            get => _dimension;
            set => _dimension = value;
        }
        
        public int Result
        {
            get => _result;
            set => _result = value;
        }
 
        /// <summary>
        /// Konstruktor domyślne pola
        /// </summary>
        public RandomOrFileMatrix()
        {
            _dimension = default;
            _result = default;
        }
        /// <summary>
        /// Wczytawanie danych z pliku
        /// </summary>
        /// <param name="fileName">Nazwa pliku</param>
        /// <returns></returns>
        public Matrix? ReadFile(string fileName)
        {
            try
            {
                var lines = File.ReadAllLines(fileName);
                Dimension = Convert.ToInt32(lines[0]); // Dzielimy napis DIMENSION: X (gdzie x to liczba) na dwie i bierzemy tylko liczbe którą zmieniamy na inta
                var matrixData = lines.Skip(1).SkipLast(1).ToArray(); // Pobieramy bez pierwszej liniki i ostatniej
                Result = Convert.ToInt32(lines.Last().Split('=')[1]);
                return LoadDataIntoMatrix(matrixData);
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception);
            }
            return null;
        }
        /// <summary>
        /// Zapisuje już wczytane dane z pliku do macierzy
        /// </summary>
        /// <param name="matrixData"></param>
        /// <returns></returns>
        private Matrix LoadDataIntoMatrix(string[] matrixData)
        {
            int row = 0;
            int column = 0;
            var fileMatrix = new int[_dimension, _dimension];
            for (int i = 0; i < matrixData.Length - 1; i++) //Pętla wypełniająca macierz
            {
                var lineData = matrixData[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //Dzielimy po spacjach a StringSplitOptions.RemoveEmptyEntries wyeliminuje nam niepotrzebne ciągi w tablicy
                foreach (var number in lineData)
                {
                    fileMatrix[row, column] = Convert.ToInt32(number);
                    column++;
                    if (column == _dimension)
                    {
                        column = 0;
                        row++;
                    }
                }
            }
            return new Matrix(_dimension, fileMatrix);
        }
        /// <summary>
        /// Generowanie własnej macierzy
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public Matrix GenerateRandomMatrix(int dimension)
        {
            int[,] matrix = new int[dimension, dimension];
            Random random = new Random();
            Dimension = dimension;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = -1;
                    }
                    else
                    {
                        matrix[i, j] = random.Next(1, 1001);
                    }
                }
            }
            return new Matrix(dimension, matrix);
        }

        /// <summary>
        /// Wypisanie informacji na temat macierzy
        /// </summary>
        public void MatrixFileInfo() => Console.WriteLine($"ATSP\nDIMENSION: {Dimension}x{Dimension}\nRESULT: {Result}");
    }
}