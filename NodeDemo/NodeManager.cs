using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace NodeDemo
{
    internal struct SegmentCount
    {
        public int whole;
        public int remainder;
    }

    public delegate void PerformOperation(GraphicBarElement v);

    public class NodeManager
    {
        private LinkedList<GraphicBarElement> list = new LinkedList<GraphicBarElement>();
        private int _currentVal;
        private readonly int _maxPerSegment;

        public int CurrentVal
        {
            get => _currentVal;
            set => _currentVal = value;
        }


        public NodeManager(int statMaximum = 1000, int maxPerSegment = 100)
        {
            this._currentVal = statMaximum;
            this._maxPerSegment = maxPerSegment;
            var nodeCount = statMaximum / maxPerSegment;

            for (var idx = 0; idx <= nodeCount; idx++)
            {
                list.AddLast(new StaminaBarElement(maxPerSegment));
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
    }

    public interface GraphicBarElement
    {
        int GetValue();
        void SetValue(int val);
        void Subtract(int val);
        void Add(int val);

        bool NeedsUpdate();
        
        void Draw();
    }

    public class StaminaBarElement : GraphicBarElement
    {
        private int _staminaValue;
        private int _max;
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

        public void Subtract(int val)
        {
            _staminaValue -= val;
            _needsUpdate = true;

        }

        public void Add(int val)
        {
            _staminaValue += val;
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