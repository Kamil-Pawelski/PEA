using System;
using System.IO;

namespace ATSP
{

    public class FilesReader
    {
        private readonly string _fileName;
        private string _type;
        private string _comment;
        private int _dimension;
        private string _fileMatrixName;
        private Matrix _matrix;


        public FilesReader(string fileName)
        {
            _fileName = fileName;
        }

        public Matrix ReadFile()
        {
            try
            {
                var lines = File.ReadAllLines(_fileName);
                _fileMatrixName = lines[0];
                _type = lines[1];
                _comment = lines[2];
                _dimension = Convert.ToInt32(lines[3].Split(": ")[1]);
                var matrixData = lines.Skip(7).ToArray();
                var currentRow = 0;
                var currentCol = 0;
                var fileMatrix = new int[_dimension, _dimension];
                for (var i = 0; i < matrixData.Length && matrixData[i] != "EOF"; i++)
                {
                    var lineData = matrixData[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var s in lineData)
                    {
                        fileMatrix[currentRow, currentCol] = int.Parse(s);
                        currentCol++;

                        if (currentCol == _dimension)
                        {
                            currentCol = 0;
                            currentRow++;
                        }
                    }
                }

                Matrix matrix = new Matrix(_dimension, fileMatrix);
                matrix.Print();
                return matrix;
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }

            return null;
        }

        public void MatrixInfo()
        {
            var fileName = _fileName.Split('\\')[1].Split('.')[0];
            Console.WriteLine($"NAME: {fileName}\n{_type}\n{_comment}\nDIMENSION: {_dimension}");
        }
    }
}