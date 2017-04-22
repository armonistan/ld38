using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class StatefulMonobehavior<TEnum> : MonoBehaviour where TEnum : struct, IConvertible, IFormattable
    {
        public Dictionary<TEnum, int?> StateCounters { get; set; }
        public TEnum State;

        public StatefulMonobehavior()
        {
            StateCounters = new Dictionary<TEnum, int?>();

            foreach (TEnum state in Enum.GetValues(typeof(TEnum)))
            {
                StateCounters.Add(state, 0);
            }
        }

        protected void IncrementCounter(TEnum state, int amount = 1)
        {
            if (StateCounters[state] != null)
            {
                StateCounters[state] += amount;
            }
        }

        protected void ResetCounter(TEnum state)
        {
            if (StateCounters[state] != null)
            {
                StateCounters[state] = 0;
            }
        }
    }
}
