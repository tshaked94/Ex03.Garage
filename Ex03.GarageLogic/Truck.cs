using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : EnginedVehicle
    {
        private bool m_IsContainingDangerousMaterials;
        private float m_CargoVolume;

        public bool IsContainingDangerousMaterials
        {
            get
            {

                return m_IsContainingDangerousMaterials;
            }

            set
            {
                m_IsContainingDangerousMaterials = value;
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
