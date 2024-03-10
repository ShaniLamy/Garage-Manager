using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelType : GenericType
    {
        private eFuelType m_VehicleFuelType;

        public FuelType()
        {
            m_TypeInputFields.Remove("Current Amount");
            m_TypeInputFields.Add("Current Fuel Amount", null);
        }

        public eFuelType VehicleFuelType
        {
            set
            {
                m_VehicleFuelType = value;
            }

            get 
            { 
                return m_VehicleFuelType;
            }
        }

        public override void SetValuesFromTypeDictionary()
        {
            bool isParsed;

            isParsed = float.TryParse(m_TypeInputFields["Current Fuel Amount"], out m_CurrentAmount);
            Vehicle.ThrowEceptionIfCantParse(isParsed);
        }
        public void RefuelingVehicle(float i_LitersToAdd, eFuelType i_FuelTypeToAdd)
        {
            float MaxFuelAmountToAdd = m_MaxAmount - m_CurrentAmount;

            if (i_FuelTypeToAdd != m_VehicleFuelType)
            {
                throw new ArgumentException(string.Format("An error occured while " +
                    "trying to refuel {0} fuel in a vehicle that recieve {1} fuel", i_FuelTypeToAdd, m_VehicleFuelType));
            }

            CheckIfExceedMaxAmountAndAdd(i_LitersToAdd);
        }

        public override string ToString()
        {
            string FuelMaessage = null;

            FuelMaessage=  string.Format(@"Fuel Type = {0}
Current Fuel Amount = {1}
Max Fuel Amount = {2}", m_VehicleFuelType.ToString(),m_CurrentAmount.ToString(),
m_MaxAmount.ToString());

            return FuelMaessage;
        }
    }
}
