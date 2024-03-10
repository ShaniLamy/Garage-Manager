using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private static Dictionary <int, VehicleInfoInGarage> s_VehiclesInGarage;

        static VehicleFactory() 
        {
            s_VehiclesInGarage = new Dictionary<int, VehicleInfoInGarage>();
        }

        public static Dictionary<int, VehicleInfoInGarage> VehiclesInGarage
        {
            set
            {
                s_VehiclesInGarage = value;
            }

            get
            {
                return s_VehiclesInGarage;
            }
        }

        public static Vehicle CreateNewVehicle(eVehicleType i_CurrentVehicleType)
        {
            Vehicle vehicle = null;

            if (i_CurrentVehicleType == eVehicleType.FuelCar)
            {
                vehicle = new FuelCar();
            }
            else if (i_CurrentVehicleType == eVehicleType.ElectricCar)
            {
                vehicle = new ElectricCar();
            }
            else if (i_CurrentVehicleType == eVehicleType.FuelMotorcycle)
            {
                vehicle = new FuelMotorcycle();
            }
            else if (i_CurrentVehicleType == eVehicleType.ElectricMotorcycle)
            {
                vehicle = new ElectricMotorcycle();
            }
            else if (i_CurrentVehicleType == eVehicleType.Truck)
            {
                vehicle = new Truck();
            }

            return vehicle;
        }

        public static void AddNewVehicleToGarage(Vehicle i_NewVehicle, VehicleInfoInGarage i_NewVehicleGarageInfo)
        {
            int vehicleKey = i_NewVehicle.GetHashCode();

            s_VehiclesInGarage.Add(vehicleKey, i_NewVehicleGarageInfo);
        }

        public static bool IsVehicleInGarage(int i_LicenseNumber)
        {
            return s_VehiclesInGarage.ContainsKey(i_LicenseNumber);
        }
    }
}
