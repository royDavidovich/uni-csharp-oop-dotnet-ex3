using System;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        public FuelVehicle MyFuelVehicle { get; set; }

        public override void InitVehicleSpecificInformation(string[] i_VehicleData)
        {
            if (Enum.TryParse(i_VehicleData[(int)eGeneralDataIndicesInFile.FuelType], out FuelVehicle.eGasType gasType))
            {
                FuelVehicle tempFuelVehicle = MyFuelVehicle;
                tempFuelVehicle.GasType = gasType;
                MyFuelVehicle = tempFuelVehicle;
            }
            else
            {
                throw new ArgumentException("Invalid fuel type provided.");
            }
        }
    }
}