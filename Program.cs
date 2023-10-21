namespace ATSP
{
    class Program
    {
        public static void Main()
        {
            RandomOrFileMatrix filesReader = new RandomOrFileMatrix();
            Matrix matrix = filesReader.ReadFile(@"files\ftv10.atsp");
            for (int i = 0; i < 10; i++)
            {
                BruteForceSearch bruteForceSearch = new BruteForceSearch(matrix, i);
                bruteForceSearch.Search();

            }

            Matrix matrix1 = filesReader.GenerateRandomGraph(10);
            for (int i = 0; i < 10; i++)
            {
                BruteForceSearch bruteForceSearch1 = new BruteForceSearch(matrix1, i);
                bruteForceSearch1.Search();
            }

        }
    }
}