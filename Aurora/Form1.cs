using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Aurora
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
        }

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Up\t" + e.KeyCode.ToString());
            //e.Handled = true;
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Down\t" + e.KeyCode.ToString());
            //e.Handled = true;
        }
    }
}
