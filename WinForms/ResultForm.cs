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
            DrawMaze();
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

        public void Solve()
        {
            // TODO
        }

        private void DrawMaze()
        {
            int length = input_maze_point_matrix.Count;
            Point TopElementLocation = ReturnButton.Location;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    MazePoint mazePoint = new MazePoint();
                    mazePoint.Location = new Point(MazeConstants.MAZE_MARGIN_LEFT + j * MazeConstants.SPACE_BETWEEN_CELLS, TopElementLocation.Y + MazeConstants.MAZE_MARGIN_TOP + i * MazeConstants.SPACE_BETWEEN_CELLS);
                    mazePoint.State = input_maze_point_matrix[i][j].State;
                    mazePoint.Enabled = false;

                    Controls.Add(mazePoint);
                }
            }
        }
    }
}
