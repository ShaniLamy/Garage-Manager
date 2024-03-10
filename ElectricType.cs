using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricType : GenericType
    {
        public ElectricType()
        {
            m_TypeInputFields.Remove("Current Amount");
            m_TypeInputFields.Add("Remaining Battery Time", null);
        }

        public override void SetValuesFromTypeDictionary()
        {
            bool isParsed;

            isParsed = float.TryParse(m_TypeInputFields["Remaining Battery Time"], out m_CurrentAmount);
            Vehicle.ThrowEceptionIfCantParse(isParsed);
        }

        public void ChargeBattery(float i_HoursToAdd) 
        {
            CheckIfExceedMaxAmountAndAdd(i_HoursToAdd);
        }

        public override string ToString()
        {
            string electricMessage = null;

            electricMessage = string.Format(@"Max Battery Time = {0}
Remaining Battery Time = {1}", m_MaxAmount, m_CurrentAmount);

            return electricMessage;
        }
    }
}
