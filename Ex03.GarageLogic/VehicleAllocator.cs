using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleAllocator
    {
        public enum eVehicleTypes
        {
            EnginedCar = 1,
            ElectricCar,
            EnginedMotorbike,
            ElectricMotorbike,
            Truck
        }

        public Vehicle MakeNewVehicle(eVehicleTypes vehicleType)
        {
            // this method get a vehicle type and return a new vehicle according to the type recieved.
            Vehicle newVehicle = null;

            switch (vehicleType)
            {
                case eVehicleTypes.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case eVehicleTypes.ElectricMotorbike:
                    newVehicle = new ElectricMotorbike();
                    break;
                case eVehicleTypes.EnginedCar:
                    newVehicle = new EnginedCar();
                    break;
                case eVehicleTypes.EnginedMotorbike:
                    newVehicle = new EnginedMotorbike();
                    break;
                case eVehicleTypes.Truck:
                    newVehicle = new Truck();
                    break;
            }

            return newVehicle;
        }
    }
}