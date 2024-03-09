using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WinForms
{
    public partial class MazePoint : Button
    {
        private MazePointStatesEnum state = MazePointStatesEnum.PATH;
        private Dictionary<MazePointStatesEnum, Color> color_dict = new Dictionary<MazePointStatesEnum, Color>()
        {
            {MazePointStatesEnum.PATH, Color.White},
            {MazePointStatesEnum.WALL, Color.Black},
            {MazePointStatesEnum.START, Color.Green},
            {MazePointStatesEnum.END, Color.Red}
        };

        public MazePointStatesEnum State { 
            get { return state; } 
            set
            {
                state = value;
                ChangeColor();
            }
        }

        public MazePoint()
        {
            InitializeComponent();

            Width = MazeConstants.CELL_WIDTH;
            Height = MazeConstants.CELL_HEIGHT;
            Click += MazePoint_OnClick;
        }

        private void MazePoint_OnClick(object sender, EventArgs e)
        {
            ChangeToNextState();
        }

        private void ChangeToNextState()
        {
            switch (state)
            {
                case MazePointStatesEnum.PATH:
                    state = MazePointStatesEnum.WALL; 
                    break;
                case MazePointStatesEnum.WALL:
                    state = MazePointStatesEnum.START;
                    break;
                case MazePointStatesEnum.START:
                    state = MazePointStatesEnum.END;
                    break;
                case MazePointStatesEnum.END:
                    state = MazePointStatesEnum.PATH;
                    break;
            }
            ChangeColor();
        }

        private void ChangeColor()
        {
            BackColor = color_dict[state];
        }

    }
}
