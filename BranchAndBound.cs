using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ATSP
{
    public class BranchAndBound
    {
        private Matrix _matrix;
        private int _bestUpperBound;
        private List<int> _bestPath;
        private int _startVertex;
        /// <summary>
        /// Konstruktor obiektu BranchAndBound
        /// </summary>
        /// <param name="matrix">macierz</param>
        /// <param name="startVertex">startowy wierzchołek</param>
        public BranchAndBound(Matrix matrix, int startVertex)
        {
            _matrix = matrix;
            _bestUpperBound = int.MaxValue;
            _bestPath = new List<int>();
            _startVertex = startVertex;
        }
        /// <summary>
        /// Główna metoda do obliczania ścieżki
        /// </summary>
        public void CalculatePath()
        {
            var stopwatch = new Stopwatch(); //mierzenie czasu
            stopwatch.Start();
            Matrix initialMatrix = new Matrix(_matrix.Size, (int[,])_matrix.MatrixData.Clone()); //macież na której będziemy pracować, aby nie nadpisywać poprzedniej (raz zadziała ale dla każde
            int lowerBound = initialMatrix.ReduceRowsAndColumns();
            FindBestTour(_startVertex, lowerBound, new List<int> { _startVertex }, initialMatrix);    
            stopwatch.Stop();
            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            PrintResult(_bestPath, _bestUpperBound, elapsedTime);
            SaveResultToFile(elapsedTime);
        }

        /// <summary>
        /// Szukanie najlepszej ścieżki
        /// </summary>
        /// <param name="currentVertex">Obecny wierzchołek</param>
        /// <param name="currentCost">Obecnyt koszt</param>
        /// <param name="currentPath">Obecna ścieżka</param>
        /// <param name="matrix"> obecna macierz</param>
        private void FindBestTour(int currentVertex, int currentCost, List<int> currentPath, Matrix matrix)
        {    

            if (currentPath.Count == matrix.Size) //Sprawdzamy czy już mamy wszystkie wierzchołki i czy możemy już wracać
            {
                currentCost += matrix.GetWeight(currentVertex, _startVertex);
                if (currentCost < _bestUpperBound)
                {
                    _bestUpperBound = currentCost;
                    _bestPath = new List<int>(currentPath) { _startVertex }; 
                }
                return;
            }

            for (int nextVertex = 0; nextVertex < matrix.Size; nextVertex++) //jeśli nie wyszukujemy
            {
                if (matrix.GetWeight(currentVertex, nextVertex) != -1 && !currentPath.Contains(nextVertex)) //jak -1 albo wierzchołek został odzwiedzony to pomijamy
                {
                    Matrix reducedMatrix = matrix.CloneMatrix(currentVertex, nextVertex); //klonujemy macierz
                    int reductionCost = reducedMatrix.ReduceRowsAndColumns(); //redukujemy

                    int newCost = currentCost + matrix.GetWeight(currentVertex, nextVertex) + reductionCost; //obliczamy koszt z dotychczasowej sciezki + wagi + tego co z redukowaliśmy
                    if (newCost < _bestUpperBound) //jesli mniejszy to obliczony koszt lepszy od
                    {
                        List<int> newPath = new List<int>(currentPath) { nextVertex }; //dodajemy do nowej listy obecna sciezke plus kolejny wierzcholek
                        FindBestTour(nextVertex, newCost, newPath, reducedMatrix); //zaczynamy rekutrencyjnie liczyc na nowych z redukowanych danych zaczyna sie rozgalezianie
                    }
                }
            }
        }
        /// <summary>
        /// zapis do pliku
        /// </summary>
        /// <param name="elapsedTime"> czas wykonania </param>
        private void SaveResultToFile(double elapsedTime)
        {
            string filePath = "wyniki.csv";
            string delimiter = ";";
            string newLine = Environment.NewLine;

            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, $"Operacja{delimiter}Czas (ms){newLine}");
            }

            File.AppendAllText(filePath, $"Wynik{_matrix.Size}{delimiter}{elapsedTime}{newLine}");
        }
        /// <summary>
        /// wypisanie wyniku
        /// </summary>
        /// <param name="path"> sciezka</param>
        /// <param name="length"> dlugosc</param>
        /// <param name="elapsedTime">Czas wykonania</param>
        private void PrintResult(List<int> path, int length, double elapsedTime) => Console.WriteLine($"Scieżka: {string.Join(" ", path)}\t\t" + 
            $"Odległość: {length}.\tCzas potrzebny do wykonania: {elapsedTime}(ms)");

    }
}
