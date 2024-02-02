using DSharpPlus;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Discord_Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cursor.Position = new Point(0, 0);
            var bot = new Bot();
            bot.Main();
        }

        public void TEST()
        {
            Cursor.Position = new Point(0, 0);
        }
    }
}
