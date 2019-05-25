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
            base.m_MaximumFuelAmount = k_MaximumFuelAmount;
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
        public override List<KeyValuePair<string, string>> Inforamtion()
        {
            int numberOfAttributes = 5;

            List<KeyValuePair<string, string>> informationList = new List<KeyValuePair<string, string>>(numberOfAttributes);
            informationList.Add(new KeyValuePair<string, string>("Fuel type", Enum.GetName(typeof(eFuelType), (int)m_FuelType)));
            informationList.Add(new KeyValuePair<string, string>("Current fuel amount", string.Format("{0} liters", m_CurrentFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("Maximum fuel amount", string.Format("{0} liters", m_MaximumFuelAmount)));
            informationList.Add(new KeyValuePair<string, string>("Car color", Enum.GetName(typeof(Utilities.eCarColor), (int)m_CarColor)));
            informationList.Add(new KeyValuePair<string, string>("Number of doors", m_NumOfDoors.ToString()));

            return informationList;
        }

    }
}
