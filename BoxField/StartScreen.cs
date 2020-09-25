using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BoxField
{
    public partial class StartScreen : UserControl
    {
        public StartScreen()
        {
            InitializeComponent();
            Thread.Sleep(3000);
            Form f = this.FindForm();
            f.Controls.Remove(this);
            GameScreen gs = new GameScreen();
            this.Controls.Add(gs);
        }
    }
}
