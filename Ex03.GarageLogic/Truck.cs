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
        public override List<KeyValuePair<string, string>> VehicleInformationByType()
        {
            int numberOfAttributes = 5;
            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);
            string containsDangerousMaterials;

            containsDangerousMaterials = v_IsContainingDangerousMaterials == true ? "Yes" : "No";
            informationList.Add(new KeyValuePair<string, string>("Vehicle type", GetType().Name));
            informationList.Add(new KeyValuePair<string, string>("Fuel type", Enum.GetName(typeof(eFuelType), (int)m_FuelType)));
            informationList.Add(new KeyValuePair<string, string>("Current fuel amount", string.Format("{0} liters", m_CurrentFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("Maximum fuel amount", string.Format("{0} liters", m_MaximumFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("Contains dangerous materials?", containsDangerousMaterials));

            return informationList;
        }

    }
}
