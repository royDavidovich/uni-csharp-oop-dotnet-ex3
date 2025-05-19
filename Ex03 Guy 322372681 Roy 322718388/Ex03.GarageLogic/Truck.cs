namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        protected const int k_NumberOfWheels = 12;
        public bool IsHazardousCargoLoaded { get; set; }
        public float CargoVolume { get; set; }
        public FuelVehicle Engine { get; set; }

        public Truck(string i_LicensePlate, string i_ModelName)
            : base(i_LicensePlate, i_ModelName)
        {

        }

        ///
        protected override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            throw new System.NotImplementedException();
        }
    }
}