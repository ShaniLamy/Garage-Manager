using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        protected bool m_IsTransportingHazardousMaterials;
        protected float m_CargoVolume;
        
        public Truck() : base(12)
        {
            m_TypeFeatures = new FuelType();
            m_NumberOfWheels = 12;
            m_MaxAirPressureForEach = 28;
            m_VehicleInputValues.Add("Is Transporting Hazardous Materials", null);
            m_VehicleInputValues.Add("Cargo Volume", null);
            InitializeWheels(m_NumberOfWheels, m_MaxAirPressureForEach);
            (m_TypeFeatures as FuelType).VehicleFuelType = eFuelType.Soler;
            m_TypeFeatures.MaxAmount = 110f;
        }

        public override void SetValuesFromVehicleDictionary()
        {
            bool isParsed;

            base.SetValuesFromVehicleDictionary();
            isParsed = bool.TryParse(m_VehicleInputValues["Is Transporting Hazardous Materials"], out m_IsTransportingHazardousMaterials);
            ThrowEceptionIfCantParse(isParsed);
            isParsed = float.TryParse(m_VehicleInputValues["Cargo Volume"], out m_CargoVolume);
            ThrowEceptionIfCantParse(isParsed);
        }

        public bool IsTransportingHazardousMaterials
        {
            set
            {
                m_IsTransportingHazardousMaterials = value;
            }

            get
            {
                return m_IsTransportingHazardousMaterials;
            }
        }

        public float CargoVolume
        {
            set
            {
                m_CargoVolume = value;
            }

            get
            {
                return m_CargoVolume;
            }
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
            string truckMessage;

            truckMessage = string.Format(@"{0}
Is Transporting Hazardous Materials = {1}
Cargo Volume = {2}
{3}", base.ToString(), m_IsTransportingHazardousMaterials, m_CargoVolume, m_TypeFeatures.ToString());

            return truckMessage ;
        }
    }
}
