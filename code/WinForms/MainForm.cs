using ClassLibrary;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private List<List<MazePoint>> input_maze_point_matrix = new List<List<MazePoint>>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MazeLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            int length;
            string input = MazeLengthTextBox.Text;

            if (int.TryParse(input, out length))
            {
                if (length >= MazeConstants.MIN_LENGTH && length <= MazeConstants.MAX_LENGTH)
                {
                    MazeLengthTextBox.ForeColor = Color.Black;

                    DrawMaze(length);

                    RandomMazeButton.Enabled = true;
                    FindButton.Enabled = true;
                    SaveCurrentMazeButton.Enabled = true;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show($"Мінімальне число = {MazeConstants.MIN_LENGTH}, максимальне число = {MazeConstants.MAX_LENGTH}", MazeLengthTextBox, 2500);

                    MazeLengthTextBox.ForeColor = Color.Red;
                }
            }
            else if (input != "")
            {
                ToolTip tp = new ToolTip();
                tp.Show("Введіть ціле число", MazeLengthTextBox, 2500);

                MazeLengthTextBox.ForeColor = Color.Red;
            }
        }

        private void DrawMaze(int length)
        {
            int prevLength = input_maze_point_matrix.Count;
            Point topElementLocation = FindButton.Location;

            for (int i = 0; i < prevLength; i++)
            {
                for (int j = 0; j < prevLength; j++)
                {
                    Controls.Remove(input_maze_point_matrix[i][j]);
                }
            }

            input_maze_point_matrix.Clear();

            for (int i = 0; i < length; i++)
            {
                input_maze_point_matrix.Add(new List<MazePoint>());

                for (int j = 0; j < length; j++)
                {
                    MazePoint mazePoint = new MazePoint();
                    mazePoint.Location = new Point(MazeConstants.MAZE_MARGIN_LEFT + j * MazeConstants.SPACE_BETWEEN_CELLS, topElementLocation.Y + MazeConstants.MAZE_MARGIN_TOP + i * MazeConstants.SPACE_BETWEEN_CELLS);

                    input_maze_point_matrix[i].Add(mazePoint);
                    Controls.Add(mazePoint);
                }
            }
        }

        private void RandomMazeButton_Click(object sender, EventArgs e)
        {
            int length = input_maze_point_matrix.Count;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    input_maze_point_matrix[i][j].State = MazePointStatesEnum.PATH;

                    bool isWall = random.Next(0, 3) == 1;
                    if (isWall)
                    {
                        input_maze_point_matrix[i][j].State = MazePointStatesEnum.WALL;
                    }
                }
            }

            int startI, startJ, endI, endJ;
            do
            {
                startI = random.Next(0, length);
                startJ = random.Next(0, length);
                endI = random.Next(0, length);
                endJ = random.Next(0, length);
            } while (startI == endI && startJ == endJ);

            input_maze_point_matrix[startI][startJ].State = MazePointStatesEnum.START;
            input_maze_point_matrix[endI][endJ].State = MazePointStatesEnum.END;
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            int length = input_maze_point_matrix.Count;
            bool hasStart = false, hasEnd = false, hasOnlyOneStartAndEnd = true;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.START)
                    {
                        if (hasStart)
                        {
                            hasOnlyOneStartAndEnd = false;
                        }
                        hasStart = true;
                    }
                    else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                    {
                        if (hasEnd)
                        {
                            hasOnlyOneStartAndEnd = false;
                        }
                        hasEnd = true;
                    }
                }
            }

            if (!hasStart || !hasEnd)
            {
                MessageBox.Show("Лабіринт не має старту чи кінця");
            }
            else if (!hasOnlyOneStartAndEnd)
            {
                MessageBox.Show("Лабіринт має більше ніж один старт чи один кінець");
            }
            else
            {
                MethodsEnum method;
                int index = MethodListBox.SelectedIndex;

                if (index != -1)
                {
                    if (index == 0)
                    {
                        method = MethodsEnum.DIJSKTRA;
                    }
                    else if (index == 1)
                    {
                        method = MethodsEnum.A_STAR_MANHATTAN;
                    }
                    else
                    {
                        method = MethodsEnum.A_STAR_EUCLIDEAN;
                    }

                    Hide();
                    new ResultForm(this, method, input_maze_point_matrix).Show();
                }
                else
                {
                    MessageBox.Show("Спочатку виберіть метод");
                }

            }

        }

        private void SaveCurrentMazeButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                int length = input_maze_point_matrix.Count;
                char[,] charMatrix = new char[length, length];

                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.START)
                        {
                            charMatrix[i, j] = 's';
                        }
                        else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                        {
                            charMatrix[i, j] = 'e';
                        }
                        else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.PATH)
                        {
                            charMatrix[i, j] = '0';
                        }
                        else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.WALL)
                        {
                            charMatrix[i, j] = '1';
                        }
                    }
                }

                try
                {
                    MatrixFileSaver.SaveMatrixWithAutoName(dialog.SelectedPath, charMatrix);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Помилка... Спробуйте інший диск");
                }
            }
            else
            {
                MessageBox.Show("Помилка... Спробуйте іншу папку");
            }
        }
    }
}
