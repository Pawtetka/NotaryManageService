using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NotaryDatabaseDLL.Classes
{
    public class DbController
    {
        private NotaryOfficeContext context;
        private CitiesService citiesService;
        private OfficeService officeService;
        private WorkerService workerService;
        private ReceptionService receptionService;
        public DbController()
        {
            context = new NotaryOfficeContext();
            citiesService = new CitiesService();
            officeService = new OfficeService();
            workerService = new WorkerService();
            receptionService = new ReceptionService();
        }

        public List<City> GetAllCities()
        {
            return citiesService.GetAllCities(context);
        }

        public List<City> GetCity(string name)
        {
            return citiesService.GetCity(name, context);
        }

        public void AddNewCity(string name, string type)
        {
            citiesService.AddNewCity(name, type, context);
        }

        public List<Office> GetAllOffices()
        {
            return officeService.GetAllOffices(context);
        }

        public List<Office> GetOffice(string name)
        {
            return officeService.GetOffice(name, context);
        }

        public void AddNewOffice(string name, string type)
        {
            officeService.AddNewOffice(name, type, context);
        }

        public List<Worker> GetAllWorkers()
        {
            return workerService.GetAllWorkers(context);
        }

        public List<Worker> GetWorker(string name)
        {
            return workerService.GetWorker(name, context);
        }

        public void AddNewWorker(string name)
        {
            workerService.AddNewWorker(name, context);
        }

        public List<Reception> GetAllReceptions()
        {
            return receptionService.GetAllReceptions(context);
        }

        public List<Reception> GetReception(string name)
        {
            return receptionService.GetReception(name, context);
        }

        public void AddNewReception(string date)
        {
            receptionService.AddNewReception(date, context);
        }
    }
}
