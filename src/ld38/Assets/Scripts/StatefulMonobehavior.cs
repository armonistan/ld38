using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class StatefulMonobehavior<TEnum> : MonoBehaviour where TEnum : struct, IConvertible, IFormattable
    {
        private TEnum _state;
        public TEnum State
        {
            get { return _state; }
            set {
                if (!_state.Equals(value))
                {
                    Counter = 0;
                    _state = value;
                }
            }
        }

        public int Counter { get; private set; }

        protected void IncrementCounter(int amount = 1)
        {
            Counter += amount;
        }
    }
}
