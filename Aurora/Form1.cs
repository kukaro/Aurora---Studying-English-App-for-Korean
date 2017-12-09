using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Aurora
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gkh = new GlobalKeyboardHook();
        GlobalMouseHook gmh = new GlobalMouseHook();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new Thread(new StateRenewThread().Run).Start();
        }
    }
}
