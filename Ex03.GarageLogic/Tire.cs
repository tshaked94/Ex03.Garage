﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufacturerName;
        private float m_CurrentPressure;
        private float m_MaximumPressure;

        public Tire(float i_MaximumTirePressure)
        {
            m_MaximumPressure = i_MaximumTirePressure;
        }

        public void Inflate(float i_AmountOfAirToAdd)
        {
            // TODO: add out of range exception.
            bool isAirPressureInRange;

            isAirPressureInRange = m_CurrentPressure + i_AmountOfAirToAdd <= m_MaximumPressure;
            if(isAirPressureInRange)
            {
                m_CurrentPressure += i_AmountOfAirToAdd;
            }
        }
    }
}