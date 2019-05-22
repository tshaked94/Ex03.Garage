using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleAllocator
    {
        private static List<string> m_VehicleTypesNames =
        new List<string>
        (new string[]
        {
        "Engined Car",
        "Electric Car",
        "Engined Motorbike",
        "Electric Motorbike",
        "Truck"
        });

        public enum eVehicleTypes
        {
            EnginedCar = 1,
            ElectricCar,
            EnginedMotorbike,
            ElectricMotorbike,
            Truck
        }

        public static List<string> VehicleTypes
        {
            get
            {

                return m_VehicleTypesNames;
            }
        }

        public static Vehicle MakeNewVehicle(eVehicleTypes vehicleType)
        {
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