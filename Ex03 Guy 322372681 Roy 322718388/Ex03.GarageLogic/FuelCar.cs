namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        public FuelVehicle Engine { get; set; }

        public FuelCar(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {
        }

        //public override void InitVehicleSpecificInformation(string[] i_VehicleData)
        //{
        //}
    }
}