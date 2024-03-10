using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car 
    {
        public ElectricCar()
        {
            m_TypeFeatures = new ElectricType();
            m_TypeFeatures.MaxAmount = 4.8f;
        }

        public ElectricType TypeFeatures
        {
            set
            {
                m_TypeFeatures = value;
            }

            get
            {
                return (m_TypeFeatures as ElectricType);
            }
        }

        public override string ToString()
        {
            string electricCarMessage;

            electricCarMessage = string.Format(@"{0}
{1}", base.ToString(), m_TypeFeatures.ToString());

            return electricCarMessage;
        }
    }
}
