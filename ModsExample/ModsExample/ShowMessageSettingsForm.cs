﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModsExample
{
    public partial class ShowMessageSettingsForm : Form
    {
        public ShowMessageSettingsForm()
        {
            InitializeComponent();
        }

        public string Message {
            get
            {
                return tbMessage.Text;
            }
            set
            {
                tbMessage.Text = value;
            }
        }
    }
}
