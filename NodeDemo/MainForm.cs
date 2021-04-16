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
        readonly NodeManager _nm = new NodeManager();
        private readonly Timer _uiHealthTimer = new Timer();
        
        private readonly Timer _subHealthTimer = new Timer();
        private readonly Label _healthValue;

        public MainForm()
        {
            _nm.DoAddToUI(this.Controls.Add);
            _uiHealthTimer.Interval = 30;
            _uiHealthTimer.Tick += Update;
            _uiHealthTimer.Start();

            _subHealthTimer.Interval = 250;
            _subHealthTimer.Tick += RemoveHealth;
            _subHealthTimer.Start();
            
            _healthValue = new Label
            {
                Location = new System.Drawing.Point(30, 40), Size = new System.Drawing.Size(30, 25), Visible = true
            };

            Button resetButton = new Button()
            {
                Location = new System.Drawing.Point(50, 80), Size = new System.Drawing.Size(65, 25), Visible = true, Text = "Add 100"

            };
            resetButton.Click += AddHealthClick;
            
            Controls.Add(_healthValue);
            Controls.Add(resetButton);

            InitializeComponent();

        }

        private void AddHealthClick(object? sender, EventArgs e)
        {
            _nm.CurrentVal += 100;

        }
        private void RemoveHealth(object? sender, EventArgs e)
        {
            
            _nm.CurrentVal -= 10;
            _healthValue.Text = _nm.CurrentVal.ToString();

        }

        private void Update(object? sender, EventArgs e)
        {
            _nm.Draw();

        }
        
    }
}