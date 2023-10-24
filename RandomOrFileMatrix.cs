using System;
using System.IO;

namespace ATSP
{

    public class RandomOrFileMatrix
    {

        private string _type;
        private string _comment;
        private int _dimension;
        private string _fileMatrixName;
        /// <summary>
        /// Konstruktor domyślne pola
        /// </summary>
        public RandomOrFileMatrix()
        {
            _type = string.Empty;
            _comment = string.Empty;
            _dimension = default;
            _fileMatrixName = string.Empty;
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
                _fileMatrixName = lines[0];
                _type = lines[1];
                _comment = lines[2];
                _dimension = Convert.ToInt32(lines[3].Split(": ")[1]); // Dzielimy napis DIMENSION: X (gdzie x to liczba) na dwie i bierzemy tylko liczbe którą zmieniamy na inta
                var matrixData = lines.Skip(7).ToArray(); // Pomijamy pierwsze 7 linijek
                int row = 0;
                int column = 0;
                var fileMatrix = new int[_dimension, _dimension];
                for (int i = 0; i < matrixData.Length && matrixData[i] != "EOF"; i++) //Pętla wypełniająca macierz
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
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }
            return null;
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
            _fileMatrixName = "NAME: Wygenerowany";
            _type = "TYPE: ATSP";
            _comment = "COMMENT:  Asymmetric TSP (Fischetti) for DIMENSION " + dimension;
            _dimension = dimension;
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
        public void MatrixFileInfo()
        {

            Console.WriteLine($"{_fileMatrixName}\n{_type}\n{_comment}\nDIMENSION: {_dimension}");
        }
    }
}