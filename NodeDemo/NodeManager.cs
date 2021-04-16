using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace NodeDemo
{

    public class NodeManager
    {
        private LinkedList<GraphicBarElement> list = new LinkedList<GraphicBarElement>();
        private int _currentVal;
        private readonly int _maxPerSegment;
        private readonly int _statMaximum;
        public int CurrentVal
        {
            get => _currentVal;
            set
            {
                if (value > _statMaximum)
                {
                    _currentVal = _statMaximum;
                }
                else if (value < 0)
                {
                    _currentVal = 0;
                }
                else
                {
                    _currentVal = value;
                }
            }
        }


        public NodeManager(int statMaximum = 1000, int maxPerSegment = 100)
        {
            this._currentVal = statMaximum;
            this._maxPerSegment = maxPerSegment;
            this._statMaximum = statMaximum;
            var nodeCount = statMaximum / maxPerSegment;

            for (var idx = 0; idx <= nodeCount; idx++)
            {
                list.AddLast(new GuiStaminaBarElement(maxPerSegment));
            }
        }
        private void Update()
        {
            var wholeSegments = _currentVal / _maxPerSegment;
            var sizeOfLast = _currentVal % _maxPerSegment;

            var currNode = list.First;

            while (currNode?.Next != null)
            {
                switch (wholeSegments)
                {
                    case > 0:
                        if(currNode.Value.GetValue() != _maxPerSegment)
                            currNode.Value.SetValue(_maxPerSegment);
                        break;
                    case 0:
                        if(currNode.Value.GetValue() != sizeOfLast)
                            currNode.Value.SetValue(sizeOfLast);
                        break;
                    case < 0:
                        if(currNode.Value.GetValue() != 0)
                            currNode.Value.SetValue(0);
                        break;
                }

                wholeSegments--;


                if (currNode.Next != null)
                    currNode = currNode.Next;
            }
        }
        public void Draw()
        {
            Update();
            DoOperaionOnSet(DrawOp);
            Console.WriteLine("  ");
        }
        private void DrawOp(GraphicBarElement e)
        {
            if(e.NeedsUpdate())
                e.Draw();
            Console.Write(" ");
        }
        private void DoOperaionOnSet(PerformOperation performOperation)
        {
            var currNode = list.First;

            while (currNode?.Next != null)
            {
                if (currNode != null)
                    performOperation(currNode.Value);

                if (currNode.Next != null)
                    currNode = currNode.Next;
            }
        }
        public void DoAddToUI(AddToUI addToUi)
        {
            var currNode = list.First;

            while (currNode?.Next != null)
            {
                if (currNode.Value is GuiStaminaBarElement v)
                    addToUi(v.GuiElem);

                if (currNode?.Next != null)
                    currNode = currNode.Next;
            }        
        }


    }

    public interface GraphicBarElement
    {
        int GetValue();
        void SetValue(int val);
        bool NeedsUpdate();
        void Draw();
    }

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
                Location = new System.Drawing.Point(xPos, 25), Size = new System.Drawing.Size(30, 10)
            };
            
            _guiElem.Visible = true;
            _guiElem.BorderStyle = BorderStyle.FixedSingle;
            _guiElem.BackColor = Color.Green;
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