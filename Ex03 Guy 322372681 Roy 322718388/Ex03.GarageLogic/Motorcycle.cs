namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        protected enum ePermitTypes
        {
            A,
            A2,
            AB,
            B2
        }

        protected enum eSpecificDataIndicesInFile
        {
            PermitType = 9,
            EngineVolume = 10
        }

        protected const int k_NumberOfWheels = 2;
        protected ePermitTypes m_PermitType;
        public int EngineVolume { get; set; }

        protected Motorcycle(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }
    }
}