using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private Utilities.eCarColor m_CarColor;
        private int m_NumOfDoors;
        public const int k_TirePressure = 31;
        public const float k_MaximumBatteryLife = 1.8f;

        public ElectricCar()
        {
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_VehicleTires.Add(new Tire(k_TirePressure));
            base.m_MaximumBatteryLife = k_MaximumBatteryLife;
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

        public override List<KeyValuePair<string, string>> VehicleInformationByType()
        {
            int numberOfAttributes = 4;
            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);

            informationList.Add(new KeyValuePair<string, string>("Battery life left", string.Format("{0} hours", m_BatteryLifeLeft)));
            informationList.Add(new KeyValuePair<string, string>("Maximum battery lifetime", string.Format("{0} hours", m_MaximumBatteryLife)));
            informationList.Add(new KeyValuePair<string, string>("Car color", Enum.GetName(typeof(Utilities.eCarColor), (int)m_CarColor)));
            informationList.Add(new KeyValuePair<string, string>("Number of doors", m_NumOfDoors.ToString()));

            return informationList;
        }

    }
}
