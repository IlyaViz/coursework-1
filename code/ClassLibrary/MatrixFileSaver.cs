namespace ClassLibrary
{
    public static class MatrixFileSaver
    {
        public static void SaveMatrixWithAutoName<T>(string folderPath, T[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            string name;
            string fullPath;
            DateTime date = DateTime.Now;

            name = $"{date.Day}{date.Month}{date.Year}_{date.Hour}{date.Minute}{date.Second}.txt";
            fullPath = Path.Combine(folderPath, name);

            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        writer.Write(matrix[i, j]);

                        if (j != cols - 1)
                        {
                            writer.Write(" ");
                        }
                    }

                    if (i != rows - 1)
                    {
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}
