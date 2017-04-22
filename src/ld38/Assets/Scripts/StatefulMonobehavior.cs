using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class StatefulMonoBehavior<TEnum> : MonoBehaviour where TEnum : struct, IConvertible, IFormattable
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

        protected int Counter { get; private set; }

        protected void IncrementCounter(int amount = 1)
        {
            Counter += amount;
        }
    }
}
