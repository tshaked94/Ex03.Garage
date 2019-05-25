using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Utilities
    {
        public enum eMotorbikeLicenseType { A, A1, A2, B};
        public enum eCarColor { Red, Blue, Black, Gray};

        public enum userChoice
        {
            AddNewVehicle = 1,
            ShowListOfLicenseNumbers = 2,
            ChangeVehicleStatus = 3,
            InflateCarTiresToMaximum = 4,
            FuelEnginedCar = 5,
            ChargeElectricCar = 6,
            ShowAllVehicleData = 7
        }
    }
}
