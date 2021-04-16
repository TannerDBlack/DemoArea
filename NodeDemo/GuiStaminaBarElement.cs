using System;
using System.Drawing;
using System.Windows.Forms;

namespace NodeDemo
{
    public class GuiStaminaBarElement : GraphicBarElement
    {
        private int _staminaValue;
        private readonly int _max;
        private bool _needsUpdate;
        private Label _guiElem;

        public Label GuiElem
        {
            get => _guiElem;
            set => _guiElem = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void Draw()
        {
            if (_staminaValue >= Math.Round(_max * (0.75), MidpointRounding.ToZero)) //75
            {
                _guiElem.BackColor = Color.Green;
            }
            else if (_staminaValue >= Math.Round(_max * (0.50), MidpointRounding.ToZero)) //50
            {
                _guiElem.BackColor = Color.Orange;
            }
            else if (_staminaValue >= Math.Round(_max * (0.25), MidpointRounding.ToZero)) //25
            {
                _guiElem.BackColor = Color.Red;
            }
            else if (_staminaValue > 0)
            {
                _guiElem.BackColor = Color.Black;
            }
            _needsUpdate = false;
            
        }
        #region ctor
        private static int xPos = 30;
        public GuiStaminaBarElement(int max)
        {
            _staminaValue = max;
            _max = max;
            _guiElem = new Label
            {
                Location = new System.Drawing.Point(xPos, 25),
                Size = new System.Drawing.Size(30, 10),
                Visible = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Green
            };

            xPos += 35;

        }
        #endregion
        #region SetGet
        public int GetValue()
        {
            return _staminaValue;
        }

        public void SetValue(int val)
        {
            _staminaValue = val;
            _needsUpdate = true;
        }

        public bool NeedsUpdate()
        {
            return _needsUpdate;
        }
        #endregion
        
    }


}