using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        protected enum eCarColor
        {
            Yellow,
            Black,
            White,
            Silver
        }

        protected eCarColor m_Color;
        protected int m_NumberOfDoors;
        public int NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set
            {
                if (value < 2 || value > 5)
                {
                    throw new ValueRangeException("Car door number must be between 2 and 5.");
                }

                m_NumberOfDoors = value;
            }
        }
    }
}