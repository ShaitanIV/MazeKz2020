using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        private BusStopRepository busStopRepository;
        private IWebHostEnvironment hostEnvironment;
        private IMapper mapper;

        public BusController(BusRepository busRepository,
            BusRouteRepository busRouteRepository,
            CitizenUserRepository citizenUserRepository,
         CertificateRepository certificateRepository,
         BusOrderRepository busOrderRepository,
         BusWorkerRepository busWorkerRepository,
         BusStopRepository busStopRepository,
        IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            this.busRepository = busRepository;
            this.busRouteRepository = busRouteRepository;
            this.citizenUserRepository = citizenUserRepository;
            this.certificateRepository = certificateRepository;
            this.busOrderRepository = busOrderRepository;
            this.busWorkerRepository = busWorkerRepository;
            this.busStopRepository = busStopRepository;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new BusIndexViewModel();
            viewModel.Buses = mapper.Map<List<BusViewModel>>(busRepository.GetAll());
            viewModel.BusRouteList = mapper.Map<List<BusRouteViewModel>>(busRouteRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(BusIndexViewModel viewModel)
        {
            viewModel.Buses = mapper.Map<List<BusViewModel>>(busRepository.GetAll().Where(x => x.BusRouteId == viewModel.BusRouteId && x.BusWorker != null).ToList());
            viewModel.BusRouteList = mapper.Map<List<BusRouteViewModel>>(busRouteRepository.GetAll());
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "BusManager")]
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
            var busWithSelectedWorker = busRepository.GetBusWithWorker(viewModel.Bus.BusWorkerId);

            if (busWithSelectedWorker != null)
            {
                busWithSelectedWorker.BusWorker = null;
                busRepository.Save(busWithSelectedWorker);
            }
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
        [Authorize(Roles = "BusManager")]
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
            var busWithSelectedWorker = busRepository.GetBusWithWorker(viewModel.BusWorkerId);
            if (busWithSelectedWorker!=null)
            {
                busWithSelectedWorker.BusWorker = null;
                busRepository.Save(busWithSelectedWorker);
            }
            busRepository.Save(bus);
            return Redirect("/Bus/ManageBus");
        }

        [HttpGet]
        public IActionResult ViewBusRoutes()
        {
            var viewModel = new ViewBusRouteViewModel();
            viewModel.Routes = mapper.Map<List<BusRouteViewModel>>(busRouteRepository.GetAll().ToList());
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ViewBusStops()
        {
            var viewModel = new ViewBusStopViewModel();
            viewModel.BusStops = mapper.Map<List<BusStopViewModel>>(busStopRepository.GetAll().ToList());
            var BusRouteList = new List<BusRouteViewModel>();
            BusRouteList = mapper.Map<List<BusRouteViewModel>>(busRouteRepository.GetAll().ToList());
            foreach (var busStop in viewModel.BusStops)
            {
                foreach (var busRoute in BusRouteList)
                {
                    if (busRoute.Route.Contains(busStop.Name))
                    {
                        busStop.RouteIds.Add(busRoute.Id);
                    }
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "BusManager")]
        public IActionResult ManageBusWorker()
        {
            var viewmodel = new ManageBusWorkerViewModel();
            viewmodel.BusWorkers = busWorkerRepository.GetAll();
            return View(viewmodel);
        }

        [HttpPost]
        public IActionResult ManageBusWorker(ManageBusWorkerViewModel viewModel)
        {
            var busWorker = new BusWorker();
            busWorker.Certificate = certificateRepository.Get(viewModel.LicenseId);
            busWorker.CitizenUser = citizenUserRepository.GetUserByLogin(viewModel.CitizenLogin);
            busWorkerRepository.Save(busWorker);
            viewModel.BusWorkers = busWorkerRepository.GetAll();
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
