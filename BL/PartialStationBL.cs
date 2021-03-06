using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL : BLApi.IBL
    {
        /// <summary>
        /// Functions for adding a station to DAL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public void AddStationDal(int id, int name, Location location, int chargeSlots)
        {
            DO.Station station = new DO.Station();
            station.ID = id;
            station.Name = name;
            station.Longitude = location.Longitude;
            station.Latitude = location.Latitude;
            station.ChargeSlots = chargeSlots;
            dalObj.AddStation(station);
        }

        /// <summary>
        /// Functions for adding a station to BL,
        /// If no exception are thrown the function will call the function: AddStationDal
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="location"></param>
        /// <param name="chargeSlots"></param>
        public string AddStationBL(int id, int name, Location location, int chargeSlots)
        {
            if (dalObj.GetStations().Any(s => s.ID == id))
            {
                throw new ObjectAlreadyExistException("Station", id);
            }
            BO.Station station = new BO.Station();
            try
            {
                station.ID = id;
                station.Name = name;

                station.Location = location;
                station.AveChargeSlots = chargeSlots;
            }
            catch (InvalidObjException e) { throw e; }
            AddStationDal(id, name, location, chargeSlots);
            return "Station added successfully!";
        }

        /// <summary>
        /// Function for update the station data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="ChargeSlots"></param>
        public string UpdateStationData(int id, string name = null, string ChargeSlots =null)
        {
            DO.Station station = dalObj.GetSpesificStation(id);
            if (name !=null && name!= "")
                station.Name = int.Parse(name);
            if (ChargeSlots != null && ChargeSlots != "")
                station.ChargeSlots = int.Parse(ChargeSlots);
            dalObj.UpdateStation(station);
            return "The update was successful!";
        }

        /// <summary>
        /// Convert from BL station to DAL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DO.Station ConvertBLStationToDAL(BO.Station s)
        {
            return new DO.Station
            {
                ID = s.ID,
                ChargeSlots = (int)s.AveChargeSlots,
                Latitude = s.Location.Latitude,
                Longitude = s.Location.Longitude,
                Name = s.Name
            };
        }

        /// <summary>
        /// Convert from DAL station to BL station
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public BO.Station ConvertDalStationToBL(DO.Station s)
        {
            return new BO.Station
            {
                ID = s.ID,
                Name = s.Name,
                Location = new Location { Latitude = s.Latitude, Longitude = s.Longitude },
                AveChargeSlots = s.ChargeSlots
            };
        }

        /// <summary>
        /// Returning a specific station by ID number
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public BO.Station GetSpesificStationBL(int stationId)
        {
            try
            {
                return ConvertDalStationToBL(dalObj.GetSpesificStation(stationId));
            }
            catch (ObjectDoesNotExist e)
            {
                throw new ObjectNotExistException(e.Message);
            }
        }

        /// <summary>
        /// Returning the station list
        /// </summary>
        /// <returns></returns>
        public List<BO.Station> GetStationsBL()
        {
            List<DO.Station> stationsDal = dalObj.GetStations();
            List<BO.Station> stationsBL = new List<BO.Station>();
            stationsDal.ForEach(s => stationsBL.Add(ConvertDalStationToBL(s)));
            return stationsBL;
        }

        /// <summary>
        /// Returns stations with available charge slots
        /// </summary>
        /// <returns></returns>
        public List<BO.Station> GetAvailableStationsList()
        {
            List<BO.Station> stationsBL = new List<BO.Station>();
            foreach (DO.Station station in dalObj.GetStationLists())
            {
                if (station.ChargeSlots > 0)
                {
                    stationsBL.Add(ConvertDalStationToBL(station));
                }
            }
            return stationsBL;
        }

        /// <summary>
        /// Returns the station in the location closest to the received location
        /// </summary>
        /// <param name="Targlocation"></param>
        /// <returns></returns>
        public BO.Station GetNearestAvailableStation(Location Targlocation)
        {
            BO.Station station = null;
            List<BO.Station> stations = GetStationsBL();

            double minDistance = Distance(stations[0].Location, Targlocation);
            foreach (BO.Station currentStation in stations)
            {
                if (currentStation.AveChargeSlots > 0 && Distance(currentStation.Location, Targlocation) <= minDistance)
                {
                    minDistance = Distance(currentStation.Location, Targlocation);
                    station = currentStation;

                }
            }
            if (station == null)
            {
                throw new ThereAreNoAvelableChargeSlotsException();
            }
            return station;
        }

        /// <summary>
        /// Returns the station list with StationToList
        /// </summary>
        /// <returns></returns>
        public List<BO.StationToList> GetStationsListBL()
        {
            List<BO.Station> stations = GetStationsBL();
            List<BO.StationToList> stationToList = new List<BO.StationToList>();
            foreach (BO.Station station in stations)
            {
                stationToList.Add(new BO.StationToList(station, dalObj));
            }
            return stationToList;
        }
    }
}
