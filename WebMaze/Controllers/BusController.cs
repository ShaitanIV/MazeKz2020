using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Bus;

namespace WebMaze.Controllers
{
    public class BusController : Controller
    {
        private BusRepository busRepository;
        private BusRouteRepository busRouteRepository;
        private CitizenUserRepository citizenUserRepository;
        private CertificateRepository certificateRepository;
        private BusOrderRepository busOrderRepository;
        private BusWorkerRepository busWorkerRepository;
        private IWebHostEnvironment hostEnvironment;
        private IMapper mapper;

        public BusController(BusRepository busRepository,
            BusRouteRepository busRouteRepository,
            CitizenUserRepository citizenUserRepository,
         CertificateRepository certificateRepository,
         BusOrderRepository busOrderRepository,
         BusWorkerRepository busWorkerRepository,
        IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            this.busRepository = busRepository;
            this.busRouteRepository = busRouteRepository;
            this.citizenUserRepository = citizenUserRepository;
            this.certificateRepository = certificateRepository;
            this.busOrderRepository = busOrderRepository;
            this.busWorkerRepository = busWorkerRepository;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BusViewModel viewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageBus()
        {
            var buses = busRepository.GetAll();
            var viewModel = new BusManageViewModel();
            foreach(var bus in buses)
            {
                var model = mapper.Map<BusViewModel>(bus);
                viewModel.Buses.Add(model);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ManageBus(BusManageViewModel viewModel)
        {
            var bus = mapper.Map<Bus>(viewModel.Bus);
            bus.BusWorker = busWorkerRepository.Get((long)bus.BusWorkerId);
            busRepository.Save(bus);


            return RedirectToAction("ManageBus","Bus");
        }

        [HttpGet]
        public IActionResult OrderBus()
        {
            var viewModel = new BusOrderViewModel();
            viewModel.OrderDate = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult OrderBus(BusOrderViewModel viewModel)
        {
            var busOrder = mapper.Map<BusOrder>(viewModel);
            busOrderRepository.Save(busOrder);
            viewModel.OrderDate = DateTime.Now;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditBus(long BusId)
        {
            var bus = busRepository.Get(BusId);
            var busviewModel = mapper.Map<BusViewModel>(bus);
            return View(busviewModel);
        }

        [HttpPost]
        public IActionResult EditBus(BusViewModel viewModel)
        {
            var bus = mapper.Map<Bus>(viewModel);
            bus.BusWorker = busWorkerRepository.Get((long)bus.BusWorkerId);
            busRepository.Save(bus);
            return Redirect("/Bus/ManageBus");
        }

        [HttpGet]
        public IActionResult ViewBusRoute()
        {
            var viewModel = new ViewBusRouteViewModel();
            viewModel.Routes = busRouteRepository.GetAll().ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ViewBusRouteTime()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageBusWorker()
        {
            var viewmodel = new ManageBusWorkerViewModel();
            viewmodel.CitizenUsers = citizenUserRepository.GetAll();
            viewmodel.BusWorkers = busWorkerRepository.GetAll();
            viewmodel.Certificates = certificateRepository.GetAll().Where(x => x.IssuedBy == "Bus").ToList();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult ManageBusWorker(ManageBusWorkerViewModel viewModel)
        {
            var busWorker = new BusWorker();
            busWorker.Certificate = certificateRepository.Get(viewModel.CertificateId);
            busWorker.CitizenUser = citizenUserRepository.Get(viewModel.CitizenUserId);
            busWorkerRepository.Save(busWorker);
            viewModel.CitizenUsers = citizenUserRepository.GetAll();
            viewModel.BusWorkers = busWorkerRepository.GetAll();
            viewModel.Certificates = certificateRepository.GetAll().Where(x => x.IssuedBy == "Bus").ToList();
            return View(viewModel);
        }

        public IActionResult BusAdminPartial()
        {
            return PartialView();
        }

        public IActionResult BusUserPartial()
        {
            return PartialView();
        }

    }
}

/*
 
Автобусы. Просмотр доступных маршрутов, работников, транспорта и запрос на склад. Остановка.
Статус автобуса и оповещение стоящих на остановке о том, что автобус переполнен и когда будет доступен следующий. 
Прием заказов на использование автобуса компаниями.
*/