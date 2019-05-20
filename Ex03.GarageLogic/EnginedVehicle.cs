using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class EnginedVehicle : Vehicle
    {
        public enum eFuelType { Soler, Octan95, Octan96, Octan98};

        protected float m_CurrentFuelAmount;
        protected float m_MaximumFuelAmount;
        protected eFuelType m_FuelType;

        public void Refuel(float i_AmountOfFuelToAdd, eFuelType i_FuelTypeToAdd)
        {
            // TODO: add exceptions
            bool isFuelTypeFits, isFuelAmountInRange;

            isFuelTypeFits = i_FuelTypeToAdd == m_FuelType;
            isFuelAmountInRange = i_AmountOfFuelToAdd + m_CurrentFuelAmount <= m_MaximumFuelAmount;
            if(isFuelAmountInRange && isFuelTypeFits)
            {
                m_CurrentFuelAmount += i_AmountOfFuelToAdd;
            }
        }
    }
}
