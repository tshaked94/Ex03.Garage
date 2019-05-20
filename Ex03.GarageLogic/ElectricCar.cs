using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricCar : ElectricVehicle
    {
        private Utilities.eCarColor m_CarColor;
        private int m_NumOfDoors;

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
    }
}
