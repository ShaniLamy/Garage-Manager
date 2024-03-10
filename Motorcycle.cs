using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        protected eLicenseType m_LicenseType;
        protected int m_EngineVolume;
        
        public Motorcycle() : base(2)
        {
            m_NumberOfWheels = 2;
            m_MaxAirPressureForEach = 29;
            m_VehicleInputValues.Add("License Type", null);
            m_VehicleInputValues.Add("Engine Volume", null);
            InitializeWheels(m_NumberOfWheels, m_MaxAirPressureForEach);
        }

        public override void SetValuesFromVehicleDictionary()
        {
            bool isParsed;

            base.SetValuesFromVehicleDictionary();
            isParsed = Enum.TryParse(m_VehicleInputValues["License Type"], out m_LicenseType);
            ThrowEceptionIfCantParse(isParsed);
            isParsed = int.TryParse(m_VehicleInputValues["Engine Volume"], out m_EngineVolume);
            ThrowEceptionIfCantParse(isParsed);
        }

        public eLicenseType LicenseType
        {
            set
            {
                m_LicenseType = value;
            }

            get
            {
                return m_LicenseType;
            }
        }

        public int EngineVolume
        {
            set
            {
                m_EngineVolume = value;
            }

            get
            {
                return m_EngineVolume;
            }
        }

        public override string ToString()
        {
            string motorcycleMessage;

            motorcycleMessage = string.Format(@"{0}
License Type = {1}
Engine Volume = {2}", base.ToString(), m_LicenseType, m_EngineVolume) ;

            return motorcycleMessage;
        }
    }
}
