using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class EnginedCar : EnginedVehicle
    {
        private Utilities.eCarColor m_CarColor;
        private int m_NumOfDoors;
        public const int k_TirePressure = 31;
        public const float k_MaximumFuelAmount = 55f;

        public EnginedCar()
        {
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_FuelType = EnginedVehicle.eFuelType.Octan96;
            base.m_MaximumFuelAmount = m_MaximumFuelAmount;
        }

        public int NumberOfDoors
        {
            get
            {

                return m_NumOfDoors;
            }

            set
            {
                m_NumOfDoors = value;
            }
        }

        public Utilities.eCarColor CarColor
        {
            get
            {

                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }
    }
}
