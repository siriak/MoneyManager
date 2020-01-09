using System;
using System.Drawing;
using System.Windows.Forms;
using Core;

namespace testforms
{
    internal class MainForm : Form
    {       
        private Button btn;
        private ListBox rtb;

        public MainForm()
        {
            InitializeComponent();
        }
        
        private void InitializeComponent()
        {
            Text = "Money Manager";

            Size = new Size(1000, 800);            
            btn = new Button { Text = "Submit", Location = new Point(10, 100) };
            rtb = new ListBox { Text = "Submit", Location = new Point(10, 150), Width = 1000, Height = 500 };
            btn.Click += Btn_Click;

            Controls.Add(btn);
            Controls.Add(rtb);
        }
        
        async void Btn_Click(object sender, EventArgs e)
        {
            
        }       

        private void RenderCategory(Index index)
        {
            // max and min index
            // changes for average in month
        }
    }
}