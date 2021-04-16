using System;
using System.Data;
using System.Windows.Controls;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using Label = System.Windows.Forms.Label;

namespace NodeDemo
{
    public partial class MainForm : Form
    {
        NodeManager nm = new NodeManager();
        private Timer uiHealthTimer = new Timer();
        
        private Timer subHealthTimer = new Timer();
        private Label HealthValue;
        private Button resetButton;
        public MainForm()
        {
            nm.DoAddToUI(this.Controls.Add);
            uiHealthTimer.Interval = 30;
            uiHealthTimer.Tick += Update;
            uiHealthTimer.Start();

            subHealthTimer.Interval = 250;
            subHealthTimer.Tick += RemoveHealth;
            subHealthTimer.Start();
            
            HealthValue = new Label
            {
                Location = new System.Drawing.Point(30, 40), Size = new System.Drawing.Size(30, 25), Visible = true
            };

            resetButton = new Button()
            {
                Location = new System.Drawing.Point(50, 80), Size = new System.Drawing.Size(65, 25), Visible = true, Text = "Add 100"

            };
            resetButton.Click += AddHealthClick;
            
            Controls.Add(HealthValue);
            Controls.Add(resetButton);

            InitializeComponent();

        }

        private void AddHealthClick(object? sender, EventArgs e)
        {
            nm.CurrentVal += 100;

        }

        Random rand = new Random();
        private void RemoveHealth(object? sender, EventArgs e)
        {
            
            nm.CurrentVal -= 10;
            HealthValue.Text = nm.CurrentVal.ToString();

        }

        private void Update(object? sender, EventArgs e)
        {
            nm.Draw();

        }
        
    }
}