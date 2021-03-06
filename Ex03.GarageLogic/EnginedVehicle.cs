﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class EnginedVehicle : Vehicle
    {
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        protected float m_CurrentFuelAmount;
        protected float m_MaximumFuelAmount;
        protected eFuelType m_FuelType;

        public eFuelType FuelType
        {
            get
            {

                return m_FuelType;
            }

            set
            {
                m_FuelType = value;
            }
        }

        public float MaximumFuelAmount
        {
            get
            {

                return m_MaximumFuelAmount;
            }

            set
            {
                m_MaximumFuelAmount = value;
            }
        }

        public float CurrentFuelAmount
        {
            get
            {

                return m_CurrentFuelAmount;
            }

            set
            {
                m_CurrentFuelAmount = value;
            }
        }

        public void Refuel(float i_AmountOfFuelToAdd, eFuelType i_FuelTypeToAdd)
        {
            // this method gets a fuel type and amount of fuel to add and refuel the car if the fuel type fits and amount is in range.
            bool isFuelTypeFits, isFuelAmountInRange;

            isFuelTypeFits = i_FuelTypeToAdd == m_FuelType;
            isFuelAmountInRange = (i_AmountOfFuelToAdd >= 0) && (i_AmountOfFuelToAdd + m_CurrentFuelAmount <= m_MaximumFuelAmount);
            if(isFuelAmountInRange && isFuelTypeFits)
            {
                m_CurrentFuelAmount += i_AmountOfFuelToAdd;
                m_EnergyPercentage = (m_CurrentFuelAmount / m_MaximumFuelAmount) * 100;
            }
            else
            {
                throw new ValueOutOfRangeException(m_MaximumFuelAmount - m_CurrentFuelAmount, 0);
            }
        }
    }
}
