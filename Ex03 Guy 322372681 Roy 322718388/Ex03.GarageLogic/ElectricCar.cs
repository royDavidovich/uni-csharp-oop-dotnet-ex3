namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        public ElectricCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        public ElectricVehicle Battery { get; set; }

        //public override void InitVehicleSpecificInformation(string[] i_VehicleData)
        //{
        //}
    }
}