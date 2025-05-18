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

        protected eRegistration registration;
        public int EngineVolume { get; set; }

    }
}