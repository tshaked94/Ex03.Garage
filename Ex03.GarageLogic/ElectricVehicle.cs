using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricVehicle : Vehicle
    {
        protected float m_BatteryLifeLeft;
        protected float m_MaximumBatteryLife;

        public void Charge(float i_AmountOfHoursToAddToBattery)
        {
            // TODO: add exceptions
            bool isBatteryLifeInRange;

            isBatteryLifeInRange = m_BatteryLifeLeft + i_AmountOfHoursToAddToBattery <= m_MaximumBatteryLife;
            if(isBatteryLifeInRange)
            {
                m_BatteryLifeLeft += i_AmountOfHoursToAddToBattery;
            }
        }

        public float MaximumBatteryLife
        {
            get
            {

                return m_MaximumBatteryLife;
            }

            set
            {
                m_MaximumBatteryLife = value;
            }
        }

        public float BatteryLifeLeft
        {
            get
            {

                return m_BatteryLifeLeft;
            }

            set
            {
                m_BatteryLifeLeft = value;
            }
        }
    }
}
