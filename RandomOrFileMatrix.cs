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

        public RandomOrFileMatrix()
        {
            _type = default;
            _comment = default;
            _dimension = default;
            _fileMatrixName = default;
        }
        public Matrix ReadFile(string fileName)
        {
            try
            {
                var lines = File.ReadAllLines(fileName);
                _fileMatrixName = lines[0];
                _type = lines[1];
                _comment = lines[2];
                _dimension = Convert.ToInt32(lines[3].Split(": ")[1]);
                MatrixFileInfo(fileName);
                var matrixData = lines.Skip(7).ToArray();
                int row = 0;
                int column = 0;
                var fileMatrix = new int[_dimension, _dimension];
                for (int i = 0; i < matrixData.Length && matrixData[i] != "EOF"; i++)
                {
                    var lineData = matrixData[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var number in lineData)
                    {
                        fileMatrix[row, column] = int.Parse(number);
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

        public Matrix GenerateRandomGraph(int dimension)
        {
            int[,] matrix = new int[dimension, dimension];
            Random random = new Random();

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
        private void MatrixFileInfo(string fileName)
        {

            Console.WriteLine($"NAME: {fileName.Split('\\')[1].Split('.')[0]}\n{_type}\n{_comment}\nDIMENSION: {_dimension}");
        }
    }
}