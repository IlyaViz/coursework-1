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
                if (length > 0 && length <= MazeConstants.MAX_LENGTH)
                {
                    DrawMaze(length);
                    RandomMazeButton.Enabled = true;
                    FindButton.Enabled = true;
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    tp.Show($"ћ≥н≥мальне число = 1, максимальне число = {MazeConstants.MAX_LENGTH}", MazeLengthTextBox, 2500);
                    
                    MazeLengthTextBox.Text = "";
                }
            }
            else if (input != "")
            {
                ToolTip tp = new ToolTip();
                tp.Show("¬вед≥ть число", MazeLengthTextBox, 2500);

                MazeLengthTextBox.Text = "";
                RandomMazeButton.Enabled = false;
                FindButton.Enabled = false;
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

                    bool isWall = random.Next(0, 2) == 1;
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
            bool hasStart = false, hasEnd = false;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.START)
                    {
                        hasStart = true;
                    }
                    else if (input_maze_point_matrix[i][j].State == MazePointStatesEnum.END)
                    {
                        hasEnd = true;
                    }
                }
            }

            if (!hasStart || !hasEnd)
            {
                MessageBox.Show("Ћаб≥ринт не маЇ старту чи к≥нц€");
            }
            else
            {
                MethodsEnum method;

                if (MethodListBox.SelectedItem != null)
                {
                    string text = MethodListBox.GetItemText(MethodListBox.SelectedItem);

                    if (text == "ћетод ƒейкстри")
                    {
                        method = MethodsEnum.Dijkstras;
                    }
                    else
                    {
                        method = MethodsEnum.AStar;
                    }

                    Hide();
                    new ResultForm(this, method, input_maze_point_matrix).Show();
                }
                else
                {
                    MessageBox.Show("—початку вибер≥ть метод");
                }

            }

        }
    }
}
