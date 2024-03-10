using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GenericType
    {
        protected Dictionary<string, string> m_TypeInputFields;
        protected float m_MaxAmount;
        protected float m_CurrentAmount;

        public GenericType()
        {
            m_TypeInputFields = new Dictionary<string, string>();
            m_TypeInputFields.Add("Current Amount", null);
        }

        public Dictionary<string, string> TypeInputFields
        {
            set
            {
                m_TypeInputFields = value;
            }

            get
            {
                return m_TypeInputFields;
            }
        }

        public float CurrentAmount
        {
            set
            {
                if (value > 0 && value <= m_MaxAmount)
                {
                    m_CurrentAmount = value;
                }
                else
                {
                    const int k_MinValue = 0;

                    throw new ValueOutOfRangeException(m_MaxAmount, k_MinValue);
                }
            }

            get
            {
                return m_CurrentAmount;
            }
        }

        public float MaxAmount
        {
            set
            {
                m_MaxAmount = value;
            }

            get
            {
                return m_MaxAmount;
            }
        }

        public virtual void SetValuesFromTypeDictionary()
        {
            bool isParsed;

            isParsed = float.TryParse(m_TypeInputFields["Current Amount"], out m_CurrentAmount);
            Vehicle.ThrowEceptionIfCantParse(isParsed);
        }

        public void CheckIfExceedMaxAmountAndAdd(float i_AmountToAdd)
        {
            float MaxAmountToAdd = m_MaxAmount - m_CurrentAmount;

            if (MaxAmountToAdd < i_AmountToAdd)
            {
                const int k_MinValue = 0;

                throw new ValueOutOfRangeException(m_MaxAmount, k_MinValue);
            }
            else
            {
                m_CurrentAmount += i_AmountToAdd;
            }
        }
    }
}
