using System;

namespace ATSP
{
    class Program
    {
        public static void Main()
        {
            FilesReader filesReader = new FilesReader("files\\ftv47.atsp");
            Matrix matrix = filesReader.ReadFile();
            filesReader.MatrixInfo();
        }
    }
}