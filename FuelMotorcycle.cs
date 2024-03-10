using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        public FuelMotorcycle()
        {
            m_TypeFeatures = new FuelType();
            (m_TypeFeatures as FuelType).VehicleFuelType = eFuelType.Octan98;
            m_TypeFeatures.MaxAmount = 5.8f;
        }

        public FuelType TypeFeatures
        {
            set
            {
                m_TypeFeatures = value;
            }

            get
            {
                return (m_TypeFeatures as FuelType);
            }
        }

        public override string ToString()
        {
            string FuelMotorcycleMessage;

            FuelMotorcycleMessage = string.Format(@"{0}
{1}", base.ToString(), m_TypeFeatures.ToString());

            return FuelMotorcycleMessage;
        }
    }
}
