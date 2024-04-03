using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using ClassLibrary;
using System.Security.Cryptography;

namespace WinForms
{
    public partial class ResultForm : Form
    {
        private MainForm main_form;
        private MethodsEnum method;
        private List<List<MazePoint>> input_maze_point_matrix;
        private List<List<MazePoint>> output_maze_point_matrix = new List<List<MazePoint>>();
        private TimeSpan algorithm_taken_time;
        private int path_length;
        private int visited_vertex_during_algorithm;

        public ResultForm(MainForm mainForm, MethodsEnum selectedMethod, List<List<MazePoint>> inputMazePointMatrix)
        {
            InitializeComponent();

            main_form = mainForm;
            method = selectedMethod;
            input_maze_point_matrix = inputMazePointMatrix;
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {
            try
            {
                ProcessMazePath();
            }
            catch (PathNotFoundException)
            {
                MessageBox.Show("Шляху не існує, повернення...");
                Close();
                return;
            }

            DrawMaze();
            UpdateTextBoxes();
        }

        private void UpdateTextBoxes()
        {
            if (method == MethodsEnum.Dijkstra)
            {
                MethodTextBox.Text = "Метод Дейкстри";
            }
            else if (method == MethodsEnum.AStarManhattan)
            {
                MethodTextBox.Text = "Метод A*(манхеттенська евристика)";
            }
            else if (method == MethodsEnum.AStarEuclidean)
            {
                MethodTextBox.Text = "Метод А*(евклідова евристика)";
            }
            SaveResultToFileButton.Enabled = true;
            TakenTimeTextBox.Text += $"{algorithm_taken_time.Milliseconds}.{algorithm_taken_time.Microseconds}мс";
            PathLengthTextBox.Text += path_length;
            VisitedCounterTextBox.Text += visited_vertex_during_algorithm;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            main_form.Show();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProcessMazePath()
        {
            (List<List<MazeVertex>>, List<MazeVertex>) verticesTuple = FindVertexPath();
            List<List<MazeVertex>> initVertexMatrix = verticesTuple.Item1;
            List<MazeVertex> resultVertices = verticesTuple.Item2;

            int length = input_maze_point_matrix.Count;
            Point topElementLocation = VisitedCounterTextBox.Location;

            for (int i = 0; i < length; i++)
            {
                output_maze_point_matrix.Add(new List<MazePoint>());

                for (int j = 0; j < length; j++)
                {
                    MazePoint mazePoint = new MazePoint();
                    mazePoint.Location = new Point(MazeConstants.MAZE_MARGIN_LEFT + j * MazeConstants.SPACE_BETWEEN_CELLS, topElementLocation.Y + MazeConstants.MAZE_MARGIN_TOP + i * MazeConstants.SPACE_BETWEEN_CELLS);
                    mazePoint.Enabled = false;

                    if (resultVertices.Contains(initVertexMatrix[i][j]))
                    {
                        if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.START || input_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                        {
                            mazePoint.State = input_maze_point_matrix[i][j].State;
                        }
                        else
                        {
                            mazePoint.State = MazePointStatesEnum.FOUND_PATH;
                        }
                    }
                    else
                    {
                        mazePoint.State = input_maze_point_matrix[i][j].State;
                    }

                    output_maze_point_matrix[i].Add(mazePoint);
                }
            }

        }

        private void DrawMaze()
        {
            int length = output_maze_point_matrix.Count;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Controls.Add(output_maze_point_matrix[i][j]);
                }
            }
        }

        private (List<List<MazeVertex>>, List<MazeVertex>) FindVertexPath()
        {
            (List<List<MazeVertex>>, MazeVertex, MazeVertex) verticesTuple = GetVertices();
            List<List<MazeVertex>> initVertexMatrix = verticesTuple.Item1;
            MazeVertex start = verticesTuple.Item2;
            MazeVertex end = verticesTuple.Item3;

            MazeGraph graph = new MazeGraph(initVertexMatrix);

            (List<MazeVertex>, TimeSpan, int) algorithmResult;
            if (method == MethodsEnum.Dijkstra)
            {
                algorithmResult = MazeSolver.Solve(graph, start, end, MazeSolver.DijsktraDistance);
            }
            else if (method == MethodsEnum.AStarManhattan)
            {
                algorithmResult = MazeSolver.Solve(graph, start, end, MazeSolver.ManhattanDistance);
            }
            else
            {
                algorithmResult = MazeSolver.Solve(graph, start, end, MazeSolver.EuclideanDistance);
            }

            algorithm_taken_time = algorithmResult.Item2;
            path_length = algorithmResult.Item1.Count-1;
            visited_vertex_during_algorithm = algorithmResult.Item3;

            return (initVertexMatrix, algorithmResult.Item1);
        }

        private (List<List<MazeVertex>>, MazeVertex, MazeVertex) GetVertices()
        {
            int length = input_maze_point_matrix.Count;

            List<List<MazeVertex>> vertexMatrix = new List<List<MazeVertex>>();
            MazeVertex start = null;
            MazeVertex end = null;

            MazeVertex temp;
            for (int i = 0; i < length; i++)
            {
                vertexMatrix.Add(new List<MazeVertex>());

                for (int j = 0; j < length; j++)
                {
                    temp = new MazeVertex();
                    temp.X = j;
                    temp.Y = i;
                    vertexMatrix[i].Add(temp);
                }
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (input_maze_point_matrix[i][j].State != MazePointStatesEnum.WALL)
                    {
                        if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.START)
                        {
                            start = vertexMatrix[i][j];
                        }
                        else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                        {
                            end = vertexMatrix[i][j];
                        }

                        List<(int, int)> verticalAndHorizontalCoordinates = new List<(int, int)>
                        {
                            (0, 1),
                            (0, -1),
                            (1, 0),
                            (-1, 0),

                        };
                        foreach ((int, int) path in verticalAndHorizontalCoordinates)
                        {
                            int neighbourI = i + path.Item1;
                            int neighbourJ = j + path.Item2;
                            if (neighbourI >= 0 && neighbourI < length && neighbourJ >= 0 && neighbourJ < length)
                            {
                                if (input_maze_point_matrix[neighbourI][neighbourJ].State != MazePointStatesEnum.WALL)
                                {
                                    vertexMatrix[i][j].AddNeighbour(vertexMatrix[neighbourI][neighbourJ]);
                                }
                            }
                        }

                        List<(int, int)> diagonalCoordinates = new List<(int, int)>
                        {
                            (1, 1),
                            (1, -1),
                            (-1, -1),
                            (-1, 1)
                        };
                        foreach ((int, int) path in diagonalCoordinates)
                        {
                            int neighbourI = i + path.Item1;
                            int neighbourJ = j + path.Item2;
                            if (neighbourI >= 0 && neighbourI < length && neighbourJ >= 0 && neighbourJ < length)
                            {
                                if (input_maze_point_matrix[neighbourI][neighbourJ].State != MazePointStatesEnum.WALL)
                                {
                                    if (input_maze_point_matrix[neighbourI][j].State != MazePointStatesEnum.WALL || input_maze_point_matrix[i][neighbourJ].State != MazePointStatesEnum.WALL)
                                    {
                                        vertexMatrix[i][j].AddNeighbour(vertexMatrix[neighbourI][neighbourJ]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return (vertexMatrix, start, end);
        }

        private void SaveResultToFileButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                int length = output_maze_point_matrix.Count;
                char[,] charMatrix = new char[length, length];

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (output_maze_point_matrix[i][j].State == MazePointStatesEnum.START)
                        {
                            charMatrix[i, j] = 's';
                        }
                        else if (output_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                        {
                            charMatrix[i, j] = 'e';
                        }
                        else if (output_maze_point_matrix[i][j].State == MazePointStatesEnum.FOUND_PATH)
                        {
                            charMatrix[i, j] = '*';
                        }
                        else if (output_maze_point_matrix[i][j].State == MazePointStatesEnum.PATH)
                        {
                            charMatrix[i, j] = '-';
                        }
                        else if (output_maze_point_matrix[i][j].State == MazePointStatesEnum.WALL)
                        {
                            charMatrix[i, j] = '#';
                        }
                    }
                }

                MatrixFileSaver.SaveMatrixWithAutoName(dialog.SelectedPath, charMatrix);
            }
            else
            {
                MessageBox.Show("Помилка... Спробуйте іншу папку");
            }


        }
    }
}
