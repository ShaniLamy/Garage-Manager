using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        protected Dictionary<string, string> m_VehicleInputValues;
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected float m_PercentageOfRemainingEnergy;
        protected Wheel[] m_Wheels;
        protected int m_NumberOfWheels;
        protected int m_MaxAirPressureForEach;
        protected GenericType m_TypeFeatures;

        public Vehicle(int i_NumberOfWheels) 
        {
            m_VehicleInputValues = new Dictionary<string, string>();
            m_TypeFeatures= new GenericType();
            m_VehicleInputValues.Add("Model Name", null);
            m_VehicleInputValues.Add("Percentage Of Remaining Energy", null);
            AddWheelsInfoToDictionary(i_NumberOfWheels);
        }

        public Dictionary<string, string> VehicleInputValues
        {
            set
            {
                m_VehicleInputValues = value;
            }

            get
            {
                return m_VehicleInputValues;
            }
        }

        public GenericType TypeFeatures
        {
            set
            {
                m_TypeFeatures = value;
            }

            get
            {
                return m_TypeFeatures;
            }
        }

        public virtual void SetValuesFromVehicleDictionary()
        {
            bool isParsed;

            m_ModelName = m_VehicleInputValues["Model Name"];
            isParsed = float.TryParse(m_VehicleInputValues["Percentage Of Remaining Energy"], out m_PercentageOfRemainingEnergy);
            ThrowEceptionIfCantParse(isParsed);

            for (int wheelIndex = 0; wheelIndex < Wheels.Length; wheelIndex++)
            {
                float currentWheelCurrentAirPressure = Wheels[wheelIndex].CurrentAirPressure;
                int displayedWheelIndex = wheelIndex + 1;

                Wheels[wheelIndex].ManufacturerName =
                    m_VehicleInputValues["Manufacturer Name Wheel" + displayedWheelIndex];
                isParsed = float.TryParse(m_VehicleInputValues["Current Air Pressure Wheel" + displayedWheelIndex],
                    out currentWheelCurrentAirPressure);
                ThrowEceptionIfCantParse(isParsed);
                Wheels[wheelIndex].CurrentAirPressure = currentWheelCurrentAirPressure;
            }
        }

        public static void ThrowEceptionIfCantParse(bool i_IsParsed)
        {
            if (!i_IsParsed)
            {
                throw new FormatException("Invalid value in one of the fields! Can't Parse");
            }
        }

        public void AddWheelsInfoToDictionary(int i_NumberOfWheels)
        {
            for(int wheelIndex = 0; wheelIndex < i_NumberOfWheels; wheelIndex++)
            {
                int displayedWheelIndex = wheelIndex + 1;

                m_VehicleInputValues.Add("Manufacturer Name Wheel" + displayedWheelIndex, null);
                m_VehicleInputValues.Add("Current Air Pressure Wheel" + displayedWheelIndex, null);
            }
        }

        public void InitializeWheels(int i_NumberOfWheels, int i_MaxAirPressureForEach)
        {
            m_Wheels = new Wheel[i_NumberOfWheels];
            for (int wheelIndex = 0; wheelIndex < m_Wheels.Length; wheelIndex++)
            {
                m_Wheels[wheelIndex] = new Wheel();
                m_Wheels[wheelIndex].MaxAirPressure = i_MaxAirPressureForEach;
            }
        }

        public string ModelName 
        {
            set
            {
                m_ModelName = value;
            }

            get 
            { 
                return m_ModelName; 
            } 
        }

        public string LicenseNumber
        {
            set
            {
                m_LicenseNumber = value;
            }

            get 
            { 
                return m_LicenseNumber;
            }
        }

        public float PercentOfRemainingEnergy
        {
            set
            {
                if (value > 0 && value <= 100)
                {
                    m_PercentageOfRemainingEnergy = value;
                }
                else
                {
                    const int k_MinValue = 0;
                    const int k_MaxPercentage = 100;

                    throw new ValueOutOfRangeException(k_MinValue, k_MaxPercentage);
                }
            }

            get 
            { 
                return m_PercentageOfRemainingEnergy;
            }
        }

        public Wheel[] Wheels
        {
            set
            {
                m_Wheels = value;
            }

            get 
            { 
                return m_Wheels;
            }
        }

        public override int GetHashCode()
        {
            int licenseNumberInteger;

            licenseNumberInteger = int.Parse(m_LicenseNumber);

            return licenseNumberInteger;
        }

        public override string ToString()
        {
            string vehicleMessage = null;
            vehicleMessage = string.Format(@"Model Name = {0}
License Number = {1}
Percentage Of Remaining Energy = {2}
Wheels: 
", m_ModelName, m_LicenseNumber, m_PercentageOfRemainingEnergy);
            
            for (int wheelIndex = 0; wheelIndex < Wheels.Length; wheelIndex++)
            {
                vehicleMessage += string.Format(@"wheel[{0}] = {1}
",
                    wheelIndex, Wheels[wheelIndex].ToString());
            }

            return vehicleMessage;
        }
    }
}
