using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInfoInGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatusInGarage m_VehicleStatusInGarage;
        private Vehicle m_Vehicle;

        public string OwnerName
        {
            set
            {
                m_OwnerName = value;
            }

            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            set
            {
                m_OwnerPhoneNumber = value;
            }

            get
            {
                return m_OwnerPhoneNumber;
            }
        }
        
        public eVehicleStatusInGarage VehicleStatusInGarage
        {
            set
            {
                m_VehicleStatusInGarage = value;
            }

            get
            {
                return m_VehicleStatusInGarage;
            }
        }

        public Vehicle Vehicle
        {
            set
            {
                m_Vehicle = value;
            }

            get
            {
                return m_Vehicle;
            }
        }

        public override string ToString()
        {
            string vehicleInfoMessage;

            vehicleInfoMessage = string.Format(@"{0}
Owner Name = {1}
Owner Phone Number = {2}
Vehicle Status In Garage = {3}", m_Vehicle.ToString(), m_OwnerName,
m_OwnerPhoneNumber, m_VehicleStatusInGarage.ToString());

            return vehicleInfoMessage;
        }
    }
}
