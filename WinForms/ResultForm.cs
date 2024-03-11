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
        private List<List<MazePoint>> output_maze_point_matrix;

        public ResultForm(MainForm mainForm, MethodsEnum selectedMethod, List<List<MazePoint>> inputMazePointMatrix)
        {
            InitializeComponent();

            main_form = mainForm;
            method = selectedMethod;
            input_maze_point_matrix = inputMazePointMatrix;
        }

        public void ResultForm_Load(object sender, EventArgs e)
        {
            try
            {
                output_maze_point_matrix = GetOutputMazePointMatrix(input_maze_point_matrix, method);
                DrawMaze(output_maze_point_matrix);
            }
            catch (PathNotFoundException)
            {
                MessageBox.Show("Шляху не існує, повернення...");
                Close();
            }
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

        public List<List<MazePoint>> GetOutputMazePointMatrix(List<List<MazePoint>> inputMazePointMatrix, MethodsEnum method) 
        {
            (List<MazeVertex>, List<MazeVertex>) verticesTuple = FindPath(inputMazePointMatrix, method);
            List<MazeVertex> initVertices = verticesTuple.Item1;
            List<MazeVertex> resVertices = verticesTuple.Item2;

            List<List<MazePoint>> outputMazePointMatrix = new List<List<MazePoint>>();

            int length = inputMazePointMatrix.Count;
            Point topElementLocation = ReturnButton.Location;

            for (int i = 0; i < length; i++)
            {
                outputMazePointMatrix.Add(new List<MazePoint>());

                for (int j = 0; j < length; j++)
                {
                    MazePoint mazePoint = new MazePoint();
                    mazePoint.Location = new Point(MazeConstants.MAZE_MARGIN_LEFT + j * MazeConstants.SPACE_BETWEEN_CELLS, topElementLocation.Y + MazeConstants.MAZE_MARGIN_TOP + i * MazeConstants.SPACE_BETWEEN_CELLS);
                    mazePoint.Enabled = false;

                    if (resVertices.Contains(initVertices[i * length + j]))
                    {
                        if (inputMazePointMatrix[i][j].State == MazePointStatesEnum.START || inputMazePointMatrix[i][j].State == MazePointStatesEnum.END)
                        {
                            mazePoint.State = inputMazePointMatrix[i][j].State;
                        } else
                        {
                            mazePoint.State = MazePointStatesEnum.FOUND_PATH;
                        }
                    }
                    else
                    {
                        mazePoint.State = inputMazePointMatrix[i][j].State;
                    }

                    outputMazePointMatrix[i].Add(mazePoint);
                }
            }

            return outputMazePointMatrix;
        }

        public void DrawMaze(List<List<MazePoint>> outputMazePointMatrix)
        {
            int length = outputMazePointMatrix.Count;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Controls.Add(outputMazePointMatrix[i][j]);
                }
            }
        }

        private (List<MazeVertex>, List<MazeVertex>) FindPath(List<List<MazePoint>> inputMazePointMatrix, MethodsEnum method)
        {
            (List<MazeVertex>, MazeVertex, MazeVertex) verticesTuple = GetVerticesFromMazePointMatrix(inputMazePointMatrix);
            List<MazeVertex> initVertices = verticesTuple.Item1;
            MazeVertex start = verticesTuple.Item2;
            MazeVertex end = verticesTuple.Item3;

            MazeGraph graph = new MazeGraph(initVertices);

            SearchAlgorithm algorithm;
            if (method == MethodsEnum.Dijkstra)
            {
                algorithm = new DijkstraSearchAlgorithm(graph);
            }
            else
            {
                algorithm = new AStarSearchAlgorithm(graph);
            }

            return (initVertices, MazeSolver.FindPath(algorithm, start, end));
        }

        private (List<MazeVertex>, MazeVertex, MazeVertex) GetVerticesFromMazePointMatrix(List<List<MazePoint>> inputMazePointMatrix)
        {
            int length = inputMazePointMatrix.Count;

            List<MazeVertex> vertices = new List<MazeVertex>();
            MazeVertex start = null;
            MazeVertex end = null;

            MazeVertex temp;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    temp = new MazeVertex();
                    temp.x = j;
                    temp.y = i;
                    vertices.Add(temp);
                }
            }

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (inputMazePointMatrix[i][j].State != MazePointStatesEnum.WALL)
                    {
                        if (inputMazePointMatrix[i][j].State == MazePointStatesEnum.START)
                        {
                            start = vertices[i * length + j];
                        }
                        else if (inputMazePointMatrix[i][j].State == MazePointStatesEnum.END)
                        {
                            end = vertices[i * length + j];
                        }

                        if (j - 1 >= 0 && inputMazePointMatrix[i][j - 1].State != MazePointStatesEnum.WALL)
                        {
                            vertices[i * length + j].AddNeighbour(vertices[i * length + (j - 1)]);
                        }
                        if (j + 1 < length && inputMazePointMatrix[i][j + 1].State != MazePointStatesEnum.WALL)
                        {
                            vertices[i * length + j].AddNeighbour(vertices[i * length + (j + 1)]);
                        }
                        if (i - 1 >= 0 && inputMazePointMatrix[i - 1][j].State != MazePointStatesEnum.WALL)
                        {
                            vertices[i * length + j].AddNeighbour(vertices[(i - 1) * length + j]);
                        }
                        if (i + 1 < length && inputMazePointMatrix[i + 1][j].State != MazePointStatesEnum.WALL)
                        {
                            vertices[i * length + j].AddNeighbour(vertices[(i + 1) * length + j]);
                        }
                    }
                }
            }
            return (vertices, start, end);
        }
    }
}
