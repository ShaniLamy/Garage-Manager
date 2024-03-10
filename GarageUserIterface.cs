using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    class GarageUserIterface
    {
        public static void GarageManager()
        {
            string menuMessage, chosenActionStr = null;

            System.Console.WriteLine("Welcome to the garage!");
            menuMessage = string.Format(@"Press '1' to add a new vehicle to the garage 
Press '2' to view license numbers of the vehicles in the garage 
Press '3' to change status for a vehicle in the garage 
Press '4' to inflate wheels to max air pressure for a specific vehicle 
Press '5' to refuel a vehicle in the garage 
Press '6' to charge a vehicle in the garage 
Press '7' to view info about a vehicle in the garage 
Press 'q' to exit");
            while (chosenActionStr != "q")
            {
                System.Console.WriteLine(menuMessage);
                chosenActionStr = System.Console.ReadLine();
                try
                {
                    if (chosenActionStr == "1")
                    {
                        RequestVehicleToAddToGarage();
                    }
                    else if (chosenActionStr == "2")
                    {
                        PrintLincenseNumberInGarageByStatus();
                    }
                    else if (chosenActionStr == "3")
                    {
                        ChangeStatusOfVehicleInGarage();
                    }
                    else if (chosenActionStr == "4")
                    {
                        InflateWheelsToMax();
                    }
                    else if (chosenActionStr == "5")
                    {
                        RefuelVehicleInGarage();
                    }
                    else if (chosenActionStr == "6")
                    {
                        ChargeVehicleInGarage();
                    }
                    else if (chosenActionStr == "7")
                    {
                        PrintVehicleInfo();
                    }
                    else if(chosenActionStr != "q")
                    {
                        System.Console.WriteLine("Invalid input!");
                    }

                    System.Console.WriteLine("");
                }
                catch(Exception exception)
                {
                    System.Console.WriteLine(exception.Message);
                }
            }
        }

        public static int RequestAndCheckLicenseNumber(string i_RequestMessage, out string o_LicenseNumberStr)
        {
            int licenseNumber = -1;

            System.Console.WriteLine(i_RequestMessage);
            o_LicenseNumberStr = System.Console.ReadLine();
            
            if (!int.TryParse(o_LicenseNumberStr, out licenseNumber))
            {
                throw new FormatException("Invalid license number!");
            }

            return licenseNumber;
        }

        public static void RequestVehicleToAddToGarage()
        {
            string licenseNumberStr, vehicleTypeStr, requestMessage;
            int licenseNumber;
            eVehicleType vehicleType;
            VehicleInfoInGarage newVehicleInGarage;

            requestMessage = "Please enter the license number of your vehicle";
            licenseNumber = RequestAndCheckLicenseNumber(requestMessage, out licenseNumberStr);
            if (licenseNumber != -1)
            {
                if (VehicleFactory.IsVehicleInGarage(licenseNumber))
                {
                    PrintMessageAndUpdateVehicleInGarage(licenseNumberStr, licenseNumber, out newVehicleInGarage);
                }
                else
                {
                    System.Console.WriteLine("Please enter your vehicle type (without spaces)");
                    vehicleTypeStr = System.Console.ReadLine();
                    if (!(Enum.TryParse(vehicleTypeStr, out vehicleType)))
                    {
                        throw new FormatException("Invalid vehicle type!");
                    }
                    else
                    {
                        newVehicleInGarage = new VehicleInfoInGarage();
                        newVehicleInGarage.Vehicle = VehicleFactory.CreateNewVehicle(vehicleType);
                        RequestAndAddOwnerDetails(licenseNumberStr, newVehicleInGarage);
                        RequestValuesForVehicleAndAddToDictionary(newVehicleInGarage.Vehicle.VehicleInputValues);
                        RequestValuesForVehicleAndAddToDictionary(newVehicleInGarage.Vehicle.TypeFeatures.TypeInputFields);
                        newVehicleInGarage.Vehicle.SetValuesFromVehicleDictionary();
                        newVehicleInGarage.Vehicle.TypeFeatures.SetValuesFromTypeDictionary();

                        VehicleFactory.VehiclesInGarage.Add(licenseNumber, newVehicleInGarage);
                    }
                }
            }
        }

        public static void RequestAndAddOwnerDetails(string i_LicenseNumber, VehicleInfoInGarage i_NewVehicleInGarage)
        {
            string ownerName, ownerPhoneNumber;

            i_NewVehicleInGarage.Vehicle.LicenseNumber = i_LicenseNumber;
            System.Console.WriteLine("Enter owner's name ");
            ownerName = System.Console.ReadLine();
            i_NewVehicleInGarage.OwnerName = ownerName;
            System.Console.WriteLine("Enter owner's phone number ");
            ownerPhoneNumber = System.Console.ReadLine();
            i_NewVehicleInGarage.OwnerPhoneNumber = ownerPhoneNumber;
        }

        public static void RequestValuesForVehicleAndAddToDictionary(Dictionary<string, string> i_VehicleInputDictionary)
        {
            bool sameValueToEachWheel = false, firstTimeAsk = true;
            string wheelRequestMessage, pressedKey, manufacturerNameForAll = null,
                currentAirPressureForAllStr= null;
            
            foreach (var key in i_VehicleInputDictionary.Keys.ToList())
            {
                if (key.Contains("Wheel"))
                {
                    if (firstTimeAsk)
                    {
                        firstTimeAsk = false;
                        wheelRequestMessage = string.Format(@"Would you like to add the wheels values all at once?
press '1' if you do
press any other key otherwise");
                        System.Console.WriteLine(wheelRequestMessage);
                        pressedKey = System.Console.ReadLine();
                        sameValueToEachWheel = pressedKey == "1" ? true : false;
                        if (sameValueToEachWheel)
                        {
                           currentAirPressureForAllStr = GetCurrentAirPressureAndManufacturerNameForAll(out manufacturerNameForAll);
                        }
                    }

                    if(sameValueToEachWheel)
                    {
                        if (key.Contains("Current Air Pressure"))
                        {
                            i_VehicleInputDictionary[key] = currentAirPressureForAllStr;
                        }
                        else if (key.Contains("Manufacturer Name"))
                        {
                            i_VehicleInputDictionary[key] = manufacturerNameForAll;
                        }
                    }
                    else
                    {
                        RequestValueForKeyAndAddToDictionary(key,ref i_VehicleInputDictionary);
                    }
                }
                else
                {
                    RequestValueForKeyAndAddToDictionary(key, ref i_VehicleInputDictionary);
                }       
            }
        }

        private static void RequestValueForKeyAndAddToDictionary(string key, ref Dictionary<string, string> io_InputDictionary)
        {
            string keyMessage, inputValueStr;

            keyMessage = string.Format("Enter the {0}", key);
            System.Console.WriteLine(keyMessage);
            inputValueStr = System.Console.ReadLine();
            io_InputDictionary[key] = inputValueStr;
        }

        public static string GetCurrentAirPressureAndManufacturerNameForAll(out string o_ManufacturerNameForAll)
        {
            string currentAirPressureForAll;

            System.Console.WriteLine("Enter the current air pressure for all wheels");
            currentAirPressureForAll = System.Console.ReadLine();
            System.Console.WriteLine("Enter the manufacturer name for all wheels");
            o_ManufacturerNameForAll = System.Console.ReadLine();

            return currentAirPressureForAll;
        }

        public static void PrintMessageAndUpdateVehicleInGarage(string i_LicenseNumberStr, int licenseNumber,
            out VehicleInfoInGarage o_VehicleInfoInGarage)
        {
            string vehicleInGarageMessage = String.Format("The vehicle attached to license number {0} " +
                            "is already in the garage", i_LicenseNumberStr);

            System.Console.WriteLine(vehicleInGarageMessage);
            o_VehicleInfoInGarage = VehicleFactory.VehiclesInGarage[licenseNumber];
            o_VehicleInfoInGarage.VehicleStatusInGarage = eVehicleStatusInGarage.BeingRepaired;
        }

        public static void PrintLincenseNumberInGarageByStatus()
        {
            string filterMessage, pressedKey;
            eVehicleStatusInGarage? requestedStatus;

            filterMessage = String.Format(@"Enter the requested filter of vehicle status in garage:
Press 1 to filter by being repaired status.
Press 2 to filter by fixed status.
Press 3 to filter by paid status.
Or press any other key to view all");
            System.Console.WriteLine(filterMessage);
            pressedKey = System.Console.ReadLine();
            switch (pressedKey)
            {
                case "1":
                    requestedStatus = eVehicleStatusInGarage.BeingRepaired;
                    break;

                case "2":
                    requestedStatus = eVehicleStatusInGarage.Fixed;
                    break;

                case "3":
                    requestedStatus = eVehicleStatusInGarage.Paid;
                    break;

                default:
                    requestedStatus = null;
                    break;
            }

            foreach(VehicleInfoInGarage vehicleInfo in VehicleFactory.VehiclesInGarage.Values)
            {
                if (vehicleInfo.VehicleStatusInGarage == requestedStatus || requestedStatus == null)
                {
                    System.Console.WriteLine(vehicleInfo.Vehicle.LicenseNumber);
                }
            }
        }

        public static void ChangeStatusOfVehicleInGarage()
        {
            string licenseNumberStr, newStatusMessage, pressedKey, requestMessage;
            int licenseNumber = 0;
            VehicleInfoInGarage vehicleToChangeStatus;

            requestMessage = "Enter the license number of the vehicle to change its status";
            licenseNumber = RequestAndCheckLicenseNumber(requestMessage, out licenseNumberStr);
            ThrowExceptionIfVehicleIsNotInGarage(licenseNumber);
            vehicleToChangeStatus = VehicleFactory.VehiclesInGarage[licenseNumber];
            newStatusMessage = String.Format(@"Enter the new status of the vehicle:
Press 1 to change to being repaired status.
Press 2 to change to fixed status.
Or press any other key to change to paid status.");
            System.Console.WriteLine(newStatusMessage);
            pressedKey = System.Console.ReadLine();
            switch (pressedKey)
            {
                case "1":
                    vehicleToChangeStatus.VehicleStatusInGarage = eVehicleStatusInGarage.BeingRepaired;
                    break;

                case "2":
                    vehicleToChangeStatus.VehicleStatusInGarage = eVehicleStatusInGarage.Fixed;
                    break;

                default:
                    vehicleToChangeStatus.VehicleStatusInGarage = eVehicleStatusInGarage.Paid;
                    break;
            }
        }

        public static void InflateWheelsToMax()
        {
            string licenseNumberStr, requestMessage;
            int licenseNumber = 0;
            VehicleInfoInGarage vehicleToInflateWheels;

            requestMessage = "Enter the license number of the vehicle to inflate its wheels";
            licenseNumber = RequestAndCheckLicenseNumber(requestMessage, out licenseNumberStr);
            ThrowExceptionIfVehicleIsNotInGarage(licenseNumber);
            vehicleToInflateWheels = VehicleFactory.VehiclesInGarage[licenseNumber];
            foreach(Wheel wheelToInflate in vehicleToInflateWheels.Vehicle.Wheels)
            {
                float maxAirToAdd = wheelToInflate.MaxAirPressure - wheelToInflate.CurrentAirPressure;
                wheelToInflate.InflateWheel(maxAirToAdd);
            }
        }

        public static void RefuelVehicleInGarage()
        {
            string licenseNumberStr, fuelTypeMessage, pressedKey, amountOfFuelToAddStr, requestMessage;
            float amountOfFuelToAdd = 0;
            int licenseNumber = 0;
            bool isParsed;
            eFuelType requestedFuelType;
            VehicleInfoInGarage vehicleToRefuel;

            requestMessage = "Enter the license number of the vehicle to refuel";
            licenseNumber = RequestAndCheckLicenseNumber(requestMessage, out licenseNumberStr);
            ThrowExceptionIfVehicleIsNotInGarage(licenseNumber);
            vehicleToRefuel = VehicleFactory.VehiclesInGarage[licenseNumber];

            fuelTypeMessage = String.Format(@"Enter the fuel type of the vehicle:
Fuel types: Octan98, Octan96, Octan95, Soler.");
            System.Console.WriteLine(fuelTypeMessage);
            pressedKey = System.Console.ReadLine();
            isParsed = Enum.TryParse(pressedKey, out requestedFuelType);

            System.Console.WriteLine("Enter the amount of fuel you want to refuel");
            amountOfFuelToAddStr = System.Console.ReadLine();
            if (!isParsed)
            {
                throw new FormatException("Invalid fuel type!");
            }

            if (!float.TryParse(amountOfFuelToAddStr, out amountOfFuelToAdd))
            {
                throw new FormatException("Invalid number of amount!");
            }

            if(vehicleToRefuel.Vehicle.TypeFeatures is FuelType)
            {
                (vehicleToRefuel.Vehicle.TypeFeatures as FuelType).RefuelingVehicle(amountOfFuelToAdd, requestedFuelType);
            }
            else
            {
                throw new ArgumentException("Can't refuel this type of vehicle!");
            }
        }

        public static void ChargeVehicleInGarage()
        {
            string licenseNumberStr, minutesToAddStr, requestMessage;
            float minutesToAdd = 0;
            float hoursToAdd = 0;
            int licenseNumber = 0;
            VehicleInfoInGarage vehicleToCharge;

            requestMessage = "Enter the license number of the vehicle to charge";
            licenseNumber = RequestAndCheckLicenseNumber(requestMessage, out licenseNumberStr);
            ThrowExceptionIfVehicleIsNotInGarage(licenseNumber);
            vehicleToCharge = VehicleFactory.VehiclesInGarage[licenseNumber];

            System.Console.WriteLine("Enter the amount of minutes you want to charge");
            minutesToAddStr = System.Console.ReadLine();
            if (!float.TryParse(minutesToAddStr, out minutesToAdd))
            {
                throw new FormatException("Invalid number of amount!");
            }

            hoursToAdd = minutesToAdd / 60;
            if (vehicleToCharge.Vehicle.TypeFeatures is ElectricType)
            {
                (vehicleToCharge.Vehicle.TypeFeatures as ElectricType).ChargeBattery(hoursToAdd);
            }
            else
            {
                throw new ArgumentException("Can't charge this type of vehicle!");
            }
        }

        public static void PrintVehicleInfo()
        {
            int licenseNumber;
            string licenseNumberStr, requestInfoMessage;
            VehicleInfoInGarage vehicleToView;

            requestInfoMessage = "Enter the license number of the vehicle to view its info";
            licenseNumber = RequestAndCheckLicenseNumber(requestInfoMessage, out licenseNumberStr);
            ThrowExceptionIfVehicleIsNotInGarage(licenseNumber);
            vehicleToView = VehicleFactory.VehiclesInGarage[licenseNumber];
            System.Console.WriteLine(vehicleToView.ToString());
        }

        public static void ThrowExceptionIfVehicleIsNotInGarage(int i_LicenseNumber)
        {
            if (!VehicleFactory.IsVehicleInGarage(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not in garage!");
            }
        }
    }
}
