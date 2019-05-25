using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class EnginedMotorbike : EnginedVehicle
    {
        private Utilities.eMotorbikeLicenseType m_LicenseType;
        private int m_EngineVolume;
        public const int k_TirePressure = 33;
        public const float k_MaximumFuelAmount = 8f;

        public EnginedMotorbike()
        {
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_FuelType = EnginedMotorbike.eFuelType.Octan95;
            base.m_MaximumFuelAmount = k_MaximumFuelAmount;
        }
        public int EngineVolume
        {
            get
            {

                return m_EngineVolume;
            }

            set
            {
                m_EngineVolume = value;
            }
        }

        public Utilities.eMotorbikeLicenseType LicenseType
        {
            get
            {

                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }
        public override List<KeyValuePair<string, string>> VehicleInformationByType()
        {
            int numberOfAttributes = 5;
            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);
            informationList.Add(new KeyValuePair<string, string>("Fuel type", Enum.GetName(typeof(eFuelType), (int)m_FuelType)));
            informationList.Add(new KeyValuePair<string, string>("Current fuel amount", string.Format("{0} liters", m_CurrentFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("Maximum fuel amount", string.Format("{0} liters", m_MaximumFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("License type", Enum.GetName(typeof(Utilities.eMotorbikeLicenseType), (int)m_LicenseType)));
            informationList.Add(new KeyValuePair<string, string>("Engine volume", m_EngineVolume.ToString()));

            return informationList;
        }

    }
}
