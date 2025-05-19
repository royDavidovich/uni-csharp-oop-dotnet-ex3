namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        protected const int k_NumberOfWheels = 5;
        protected eCarColor m_Color;
        protected int m_NumberOfDoors;

        protected enum eCarColor
        {
            Yellow,
            Black,
            White,
            Silver
        }

        public Car(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
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