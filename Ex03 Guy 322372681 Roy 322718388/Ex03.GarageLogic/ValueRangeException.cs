using System;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    internal class ValueRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }

        public ValueRangeException(string i_Message, float i_Min = 0, float i_Max = 0)
        {
            MinValue = i_Min;
            MaxValue = i_Max;
        }
    }
}