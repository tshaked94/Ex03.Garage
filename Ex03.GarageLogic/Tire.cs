using System;
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
            // tire c'tor.
            m_MaximumPressure = i_MaximumTirePressure;
        }

        public string ManufacaturerName
        {
            get
            {

                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float MaximumPressure
        {
            get
            {

                return m_MaximumPressure;
            }

            set
            {
                m_MaximumPressure = value;
            }
        }

        public float CurrentPressure
        {
            get
            {

                return m_CurrentPressure;
            }

            set
            {
                m_CurrentPressure = value;
            }
        }

        public void Inflate(float i_AmountOfAirToAdd)
        {
            // this method get amount of air to add and inflating the tire.
            bool isAirPressureInRange;

            isAirPressureInRange = (m_CurrentPressure + i_AmountOfAirToAdd) <= m_MaximumPressure;
            if(isAirPressureInRange)
            {
                m_CurrentPressure += i_AmountOfAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaximumPressure, 0);
            }
        }
    }
}
