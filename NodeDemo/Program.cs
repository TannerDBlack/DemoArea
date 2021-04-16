using System;
using System.Windows.Controls;
using System.Windows.Forms;

namespace NodeDemo
{
    class Program
    {
        [STAThread]

        static void Main(string[] args)
        {

           Application.EnableVisualStyles();
            Application.Run(new MainForm()); 
            
        }
        
    }

}