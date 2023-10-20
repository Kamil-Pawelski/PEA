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
            get {return _matrix;}
            set {if(value != null) _matrix = value;}
        }
     
        public int Size
        {
            get;
        }
        public int GetCost(int[] solution)
        {
            var cost = 0;
            for (var i = 0; i < solution.Length - 1; i++) cost += GetWeight(solution[i], solution[i + 1]);
            cost += GetWeight(solution[^1], solution[0]);
            return cost;
        }

        public int GetWeight(int a, int b)
        {
            return _matrix[a, b];
        }
        
        public static void PrintSolution(IEnumerable<int> solution)
        {
            foreach (var i1 in solution) Console.Write("{0} ", i1);
            Console.WriteLine();
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
