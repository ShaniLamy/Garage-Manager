using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        protected string m_ManufacturerName = null;
        protected float m_CurrentAirPressure = 0.0f;
        protected float m_MaxAirPressure = 0.0f;

        public Wheel() 
        {
        }
        
        public void InflateWheel(float i_AirToAdd) 
        {
            if(m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure)
            {
                const int k_MinValue = 0;

                throw new ValueOutOfRangeException(m_MaxAirPressure, k_MinValue);
            }
            else
            {
                m_CurrentAirPressure = m_CurrentAirPressure + i_AirToAdd;
            }
        }

        public string ManufacturerName
        {
            set
            {
                m_ManufacturerName = value;
            }

            get 
            {
                return m_ManufacturerName;
            }
        }
        
        public float CurrentAirPressure
        {
            set 
            {
                if (value > 0 && value <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    const int k_MinValue = 0;

                    throw new ValueOutOfRangeException(m_MaxAirPressure, k_MinValue);
                }
            }

            get 
            {
                return m_CurrentAirPressure;
            }
        }

        public float MaxAirPressure
        {
            set
            {
                m_MaxAirPressure = value;
            }

            get 
            {
                return m_MaxAirPressure;
            }
        }

        public override string ToString()
        {
            string wheelMessage = null;

            wheelMessage=string.Format(@"Manufacturer Name = {0}
Current Air Pressure = {1}
Max Air Pressure = {2}",m_ManufacturerName, m_CurrentAirPressure, m_MaxAirPressure);

            return wheelMessage;
        }
    }
}
