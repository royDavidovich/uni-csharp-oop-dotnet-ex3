namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        public FuelVehicle FuelVehicle { get; set; }

        public override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            this.RegistrationNumber = i_VehicleData[(int)eGeneralDataIndicesInFile.FuelType];
            this.RegistrationNumber = i_VehicleData[(int)eGeneralDataIndicesInFile.CurrFuelAmount];
        }
    }
}