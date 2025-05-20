using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    internal class ValueRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }

        public ValueRangeException(string i_Message, float i_Min, float i_Max)
            : base(i_Message)
        {
            MinValue = i_Min;
            MaxValue = i_Max;
        }
    }
}