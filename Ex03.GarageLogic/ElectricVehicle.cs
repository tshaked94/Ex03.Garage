﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        protected float m_BatteryLifeLeft;
        protected float m_MaximumBatteryLife;

        public void Charge(float i_AmountOfHoursToAddToBattery)
        {
            // this method gets amount of hours to add to the battery and charging it if its not out of range
            bool isBatteryLifeInRange;

            isBatteryLifeInRange = (i_AmountOfHoursToAddToBattery >= 0) && (m_BatteryLifeLeft + i_AmountOfHoursToAddToBattery <= m_MaximumBatteryLife);
            if(isBatteryLifeInRange)
            {
                m_BatteryLifeLeft += i_AmountOfHoursToAddToBattery;
                m_EnergyPercentage = (m_BatteryLifeLeft / m_MaximumBatteryLife) * 100;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaximumBatteryLife - m_BatteryLifeLeft, 0);
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
