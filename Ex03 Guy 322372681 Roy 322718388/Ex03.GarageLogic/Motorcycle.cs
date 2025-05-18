namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        protected enum eRegistration
        {
            A,
            A2,
            AB,
            B2
        }

        protected eRegistration e_Registration;
        public int EngineVolume { get; set; }

    }
}