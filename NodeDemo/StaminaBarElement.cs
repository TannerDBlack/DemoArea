using System;

namespace NodeDemo
{
    public class StaminaBarElement : GraphicBarElement
    {
        private int _staminaValue;
        private readonly int _max;
        private bool _needsUpdate;
        public StaminaBarElement(int max)
        {
            _staminaValue = max;
            _max = max;
        }

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

        public void Draw()
        {

            if (_staminaValue > 0)
            {
                Console.Write("X");
            }

            if (_staminaValue >= Math.Round(_max * (0.25), MidpointRounding.ToZero)) //25
            {
                Console.Write("X");
            }

            if (_staminaValue >= Math.Round(_max * (0.50), MidpointRounding.ToZero)) //50
            {
                Console.Write("X");
            }

            if (_staminaValue >= Math.Round(_max * (0.75), MidpointRounding.ToZero)) //75
            {
                Console.Write("X");
            }
            _needsUpdate = false;
            
        }
        
    }

}