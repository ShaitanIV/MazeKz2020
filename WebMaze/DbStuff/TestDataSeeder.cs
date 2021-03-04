using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.UserAccount;
using WebMaze.DbStuff.Repository;

namespace WebMaze.DbStuff
{
    public class TestDataSeeder
    {
        private CitizenUserRepository citizenUserRepository;
        private RoleRepository roleRepository;
        private BusRouteRepository busRouteRepository;
        private CertificateRepository certificateRepository;
        private BusWorkerRepository busWorkerRepository;
        private BusRepository busRepository;
        private BusRouteTimeRepository busRouteTimeRepository;

        public TestDataSeeder(IServiceScope scope)
        {
            citizenUserRepository = scope.ServiceProvider.GetService<CitizenUserRepository>();
            roleRepository = scope.ServiceProvider.GetService<RoleRepository>();
            busRouteRepository = scope.ServiceProvider.GetService<BusRouteRepository>();
            busRepository = scope.ServiceProvider.GetService<BusRepository>();
            certificateRepository = scope.ServiceProvider.GetService<CertificateRepository>();
            busWorkerRepository = scope.ServiceProvider.GetService<BusWorkerRepository>();
            busRouteTimeRepository = scope.ServiceProvider.GetService<BusRouteTimeRepository>();

            if (citizenUserRepository == null || roleRepository == null)
            {
                throw new Exception("Cannot get services from ServiceProvider.");
            }
        }

        public void SeedData()
        {
            AddDoctors();
            AddPolicemen();
            AddBusManagers();
            AddRegularUsers();

            AddCertificates();

            AddBusWorkers();
            AddBusRoutes();
            AddBusRouteTimes();
            AddBuses();
        }

        private void AddBusManagers()
        {
            var busManagers = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "BusManager",
                    Password = "123",
                    Balance = 9999999,
                    RegistrationDate = new DateTime(2020, 12, 30),
                    LastLoginDate = new DateTime(2020, 12, 30),
                    FirstName = "Johnny",
                    LastName = "Silverhand",
                    Gender = Gender.Male,
                    Email = "CyberPrank@bug.com",
                    PhoneNumber = "77777777",
                    BirthDate = new DateTime(1988, 11, 16)
                }
            };
            AddIfNotExistUsersWithRole(busManagers, "BusManager");
        }

        private void AddDoctors()
        {
            var doctors = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Tsoi",
                    Password = "123",
                    Balance = 300000,
                    RegistrationDate = new DateTime(2020, 10, 28),
                    LastLoginDate = new DateTime(2020, 10, 28),
                    FirstName = "Alexey",
                    LastName = "Tsoi",
                    Gender = Gender.Male,
                    Email = "AlexeyTsoi@example.com",
                    PhoneNumber = "3333333333",
                    BirthDate = new DateTime(1977, 4, 2)
                }
            };
            AddIfNotExistUsersWithRole(doctors, "Doctor");
        }

        private void AddPolicemen()
        {
            var policemen = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Chuck",
                    Password = "123",
                    Balance = 6000000,
                    RegistrationDate = new DateTime(2020, 12, 30),
                    LastLoginDate = new DateTime(2020, 12, 30),
                    FirstName = "Chuck",
                    LastName = "Norris",
                    Gender = Gender.Male,
                    Email = "ChuckNorris@example.com",
                    PhoneNumber = "4444444444",
                    BirthDate = new DateTime(1940, 3, 10)
                }
            };
            AddIfNotExistUsersWithRole(policemen, "Policeman");
        }

        private void AddRegularUsers()
        {
            var regularUsers = new List<CitizenUser>()
            {
                new CitizenUser
                {
                    Login = "Ivan",
                    Password = "123",
                    Balance = 1000,
                    RegistrationDate = new DateTime(2021, 1, 12),
                    LastLoginDate = new DateTime(2021, 1, 12),
                    FirstName = "Ivan",
                    LastName = "Sokolov",
                    Gender = Gender.Male,
                    Email = "IvanSokolov@example.com",
                    PhoneNumber = "5555555555",
                    BirthDate = new DateTime(1980, 5, 17)
                },
                new CitizenUser
                {
                    Login = "Anastasia",
                    Password = "123",
                    Balance = 30000,
                    RegistrationDate = new DateTime(2021, 1, 15),
                    LastLoginDate = new DateTime(2021, 1, 15),
                    FirstName = "Anastasia",
                    LastName = "Kuznecova",
                    Gender = Gender.Female,
                    Email = "AnastasiaKuznecova@example.com",
                    PhoneNumber = "66666666",
                    BirthDate = new DateTime(1990, 11, 22)
                },
                new CitizenUser
                {
                    Login = "Arnold",
                    Password = "123",
                    Balance = 0,
                    RegistrationDate = new DateTime(2021, 1, 16),
                    LastLoginDate = new DateTime(2021, 1, 16),
                    FirstName = "Arnold",
                    LastName = "Goldenberg",
                    Gender = Gender.Male,
                    Email = "ArnoldGoldenberg@example.com",
                    PhoneNumber = "77777777",
                    BirthDate = new DateTime(1977, 5, 10)
                },
                new CitizenUser
                {
                    Login = "Aigerim",
                    Password = "123",
                    Balance = 8000,
                    RegistrationDate = new DateTime(2021, 1, 17),
                    LastLoginDate = new DateTime(2021, 1, 17),
                    FirstName = "Aigerim",
                    LastName = "Alieva",
                    Gender = Gender.Female,
                    Email = "AigerimAlieva@example.com",
                    PhoneNumber = "8888888888",
                    BirthDate = new DateTime(1983, 8, 3)
                },
                new CitizenUser
                {
                    Login = "Dias",
                    Password = "123",
                    Balance = 15000,
                    RegistrationDate = new DateTime(2021, 1, 19),
                    LastLoginDate = new DateTime(2021, 1, 19),
                    FirstName = "Dias",
                    LastName = "Karimov",
                    Gender = Gender.Male,
                    Email = "DiasKarimov@example.com",
                    PhoneNumber = "9999999999",
                    BirthDate = new DateTime(2005, 10, 5)
                },
                new CitizenUser
                {
                    Login = "BusWorker1",
                    Password = "123",
                    Balance = 1000,
                    RegistrationDate = new DateTime(2021, 1, 19),
                    LastLoginDate = new DateTime(2021, 1, 19),
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Gender = Gender.Male,
                    Email = "BusWorker1@example.com",
                    PhoneNumber = "+77777777777",
                    BirthDate = new DateTime(1980, 1, 1)
                }
            };

            AddIfNotExistUsersWithRole(regularUsers);
        }

        private void AddIfNotExistUsersWithRole(List<CitizenUser> users, string roleName = null)
        {
            foreach (var user in users.Where(u => !citizenUserRepository.UserExists(u.Login)))
            {
                if (roleName != null)
                {
                    var role = roleRepository.GetRoleByName(roleName);
                    user.Roles.Add(role);
                }

                citizenUserRepository.Save(user);
            }
        }

        private void AddCertificates()
        {
            var allCitizens = citizenUserRepository.GetUsersAsQueryable();

            // Ensure that all citizens have a birth certificate.
            AddIfNotExistCertificateToCitizens(allCitizens, "Birth Certificate");

            // Ensure that 5 citizens have a diploma.
            var citizenLoginsWithDiploma = new List<string> { "Bill", "Musk", "Stroustrup", "Tsoi", "Chuck" };
            var citizenWithDiploma = citizenUserRepository.GetUsersByLogins(citizenLoginsWithDiploma);
            AddIfNotExistCertificateToCitizens(citizenWithDiploma, "Diploma of Higher Education");

            // Ensure that citizens have a policeman certificate.
            var citizenLoginsWithPoliceCertificate = new List<string> { "Chuck" };
            var policemen = citizenUserRepository.GetUsersByLogins(citizenLoginsWithPoliceCertificate);
            AddIfNotExistCertificateToCitizens(policemen, "Policeman Certificate");

            // Ensure that citizens have a doctor certificate.
            var citizenLoginsWithDoctorCertificate = new List<string> { "Tsoi" };
            var doctors = citizenUserRepository.GetUsersByLogins(citizenLoginsWithDoctorCertificate);
            AddIfNotExistCertificateToCitizens(doctors, "Doctor Certificate");

            // Ensure that citizens have a bus driver license.
            var citizenLoginsWithBusDriverLicense = new List<string> { "BusWorker1" };
            var busWorkers = citizenUserRepository.GetUsersByLogins(citizenLoginsWithBusDriverLicense);
            AddIfNotExistCertificateToCitizens(busWorkers, "Bus driver license");
        }

        private void AddIfNotExistCertificateToCitizens(IQueryable<CitizenUser> citizens, string certificateName)
        {
            foreach (var citizen in citizens.Where(user => user.Certificates.All(certificate => certificate.Name != certificateName)).ToList())
            {
                var certificate = GenerateCertificate(certificateName, citizen);
                citizen.Certificates.Add(certificate);
                citizenUserRepository.Save(citizen);
            }
        }

        private Certificate GenerateCertificate(string certificateName, CitizenUser owner)
        {
            var certificate = new Certificate
            {
                Name = certificateName,
                Owner = owner
            };

            switch (certificateName)
            {
                case "Diploma of Higher Education":
                    certificate.Description =
                        "The document certifies that the person completed a course of study in a university";
                    certificate.IssuedBy = "University";
                    certificate.IssueDate = owner.BirthDate + TimeSpan.FromDays(22 * 365);
                    certificate.ExpiryDate = DateTime.MaxValue;
                    break;
                case "Birth Certificate":
                    certificate.Description = "The certificate documents the birth of the person";
                    certificate.IssuedBy = "Hospital";
                    certificate.IssueDate = owner.BirthDate;
                    certificate.ExpiryDate = DateTime.MaxValue;
                    break;
                case "Policeman Certificate":
                    certificate.Description = "The document assure qualification to work as a policeman";
                    certificate.IssuedBy = "Police";
                    certificate.IssueDate = new DateTime(2021, 1, 28);
                    certificate.ExpiryDate = new DateTime(2022, 1, 28);
                    break;
                case "Doctor Certificate":
                    certificate.Description = "The document assure qualification to work as a doctor";
                    certificate.IssuedBy = "Health Department";
                    certificate.IssueDate = new DateTime(2020, 5, 3);
                    certificate.ExpiryDate = new DateTime(2021, 5, 3);
                    break;
                case "Bus driver license":
                    certificate.Description = "Official document, permitting a specific individual to operate bus.";
                    certificate.IssuedBy = "Interior Ministry";
                    certificate.IssueDate = new DateTime(2020, 5, 3);
                    certificate.ExpiryDate = new DateTime(2030, 5, 3);
                    break;
            }

            return certificate;
        }

        private void AddBusRoutes()
        {
            var busRoutes = new List<BusRoute>()
            {
                new BusRoute
                {
                    Route = "Central-Park-Lake-Cinema"
                }
            };

            foreach (var busRoute in busRoutes.Where(x => !busRouteRepository.RouteExists(x.Route)))
            {
                busRouteRepository.Save(busRoute);
            }
        }

        private void AddBusWorkers()
        {
            var busWorkers = new List<BusWorker>()
            {
                new BusWorker
                {
                    CitizenUser = citizenUserRepository.GetUserByLogin("BusWorker1"),
                    Certificate = certificateRepository.GetByUserAndType(citizenUserRepository.GetUserByLogin("BusWorker1"),"Bus driver license")
                }
            };

            foreach (var busWorker in busWorkers.Where(x=> !busWorkerRepository.WorkerExists(x.CitizenUser)))
            {
                busWorkerRepository.Save(busWorker);
            }
        }

        private void AddBuses()
        {
            var Buses = new List<Bus>()
            {
                new Bus
                {
                    BusModel = "Kamaz",
                    BusRouteId=1,
                    Capacity=60,
                    RegistrationPlate = "123AB12",
                    BusWorker = busWorkerRepository.GetByCitizenUserId(citizenUserRepository.GetUserByLogin("BusWorker1").Id),
                    CurrentLocation ="",
                    CurrentOccupation =0,
                    ReversedDirection=false
                }
            };

            foreach (var bus in Buses.Where(x => !busRepository.BusExists(x.RegistrationPlate)))
            {
                busRepository.Save(bus);
            }

        }

        private void AddBusRouteTimes()
        {
            var BusRouteTimes = new List<BusRouteTime>()
            {
                new BusRouteTime
                {
                    StartingPoint = "Park",
                    EndingPoint = "Central",
                    Minutes = 10
                },
                new BusRouteTime
                {
                    StartingPoint = "Park",
                    EndingPoint = "Lake",
                    Minutes = 10
                },
                new BusRouteTime
                {
                    StartingPoint = "Lake",
                    EndingPoint = "Park",
                    Minutes = 10
                },
               new BusRouteTime
                {
                    StartingPoint = "Central",
                    EndingPoint = "Park",
                    Minutes = 10
                },
                new BusRouteTime
                {
                    StartingPoint = "Lake",
                    EndingPoint = "Cinema",
                    Minutes = 10
                },
                new BusRouteTime
                {
                    StartingPoint = "Cinema",
                    EndingPoint = "Lake",
                    Minutes = 10
                }
            };

            foreach (var busRouteTime in BusRouteTimes.Where(x => !busRouteTimeRepository.BusRouteTimeExists(x.StartingPoint,x.EndingPoint)))
            {
                busRouteTimeRepository.Save(busRouteTime);
            }
        }
    }
}

