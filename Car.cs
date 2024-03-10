using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        protected eCarColor m_CarColor;
        protected eNumberOfDoors m_NumberOfDoors;

        public Car() : base(5)
        {
            m_NumberOfWheels = 5;
            m_MaxAirPressureForEach = 30;
            m_VehicleInputValues.Add("Car Color", null);
            m_VehicleInputValues.Add("Number Of Doors", null);
            InitializeWheels(m_NumberOfWheels, m_MaxAirPressureForEach);
        }

        public override void SetValuesFromVehicleDictionary()
        {
            bool isParsed;

            base.SetValuesFromVehicleDictionary();
            isParsed = Enum.TryParse(m_VehicleInputValues["Car Color"], out m_CarColor);
            ThrowEceptionIfCantParse(isParsed);
            isParsed = Enum.TryParse(m_VehicleInputValues["Number Of Doors"], out m_NumberOfDoors);
            ThrowEceptionIfCantParse(isParsed);
        }

        public eCarColor CarColor
        {
            set
            {
                m_CarColor = value;
            }

            get
            {
                return m_CarColor;
            }
        }

        public eNumberOfDoors NumberOfDoors
        {
            set
            {
                m_NumberOfDoors = value;
            }

            get
            {
                return m_NumberOfDoors;
            }
        }

        public override string ToString()
        {
            string carMessage;

            carMessage = string.Format(@"{0}
Car Color = {1}
Number Of Doors = {2}", base.ToString(), m_CarColor.ToString(), m_NumberOfDoors.ToString());

            return carMessage;
        }
    }
}
