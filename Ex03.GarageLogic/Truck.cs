using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : EnginedVehicle
    {
        private bool v_IsContainingDangerousMaterials;
        private float m_CargoVolume;
        public const int k_TirePressure = 26;
        public const float k_MaximumFuelAmount = 110f;

        public Truck()
        {
            base.m_FuelType = EnginedVehicle.eFuelType.Soler;
            base.m_MaximumFuelAmount = k_MaximumFuelAmount;
            for (int i = 1; i <= 12; i++) 
            {
                base.m_VehicleTires.Add(new Tire(k_TirePressure));
            }
        }

        public bool IsContainingDangerousMaterials
        {
            get
            {

                return v_IsContainingDangerousMaterials;
            }

            set
            {
                v_IsContainingDangerousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {

                return m_CargoVolume;
            }

            set
            {
                m_CargoVolume = value;
            }
        }
    }
}
