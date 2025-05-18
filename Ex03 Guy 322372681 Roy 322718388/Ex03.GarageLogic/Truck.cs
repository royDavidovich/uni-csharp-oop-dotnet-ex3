namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        public bool IsHazardousCargoLoaded { get; set; }
        public float CargoVolume { get; set; }

        public override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {

        }
    }
}