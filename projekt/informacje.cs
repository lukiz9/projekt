﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt
{
    public partial class informacje : Form
    {
        public informacje()
        {
            InitializeComponent();
        }

        private void informacje_Load(object sender, EventArgs e)
        {
            label1.Text = ("Notepad #");
            label2.Text = string.Format("{0}", Application.ProductVersion + " Tygodniowa, wersja rozwojowa");
            label3.Text = ("Twórca Łukasz Zawiśliński");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}