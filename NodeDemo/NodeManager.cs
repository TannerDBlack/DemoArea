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
}