﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebMaze.DbStuff.Model.Police;
using WebMaze.DbStuff.Repository;
using WebMaze.Models.Police.Violation;

namespace WebMaze.Controllers
{
    [Authorize(AuthenticationSchemes = Startup.PoliceAuthMethod)]
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ViolationRepository violationRepo;
        private readonly CitizenUserRepository userRepo;

        public ViolationController(
            IMapper mapper,
            ViolationRepository violationRepo,
            CitizenUserRepository userRepo)
        {
            this.mapper = mapper;
            this.violationRepo = violationRepo;
            this.userRepo = userRepo;
        }

        [HttpGet("{max?}")]
        public IEnumerable<ViolationItemViewModel> Get(int max = 10)
        {
            return mapper.Map<ViolationItemViewModel[]>(violationRepo.GetAll(max));
        }

        [HttpGet("SearchUsers/{word}")]
        public IEnumerable<FoundUsersViewModel> GetUsersByName(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return new FoundUsersViewModel[] { new FoundUsersViewModel { Login = string.Empty, Name = string.Empty } };
            }

            return mapper.Map<IEnumerable<FoundUsersViewModel>>(userRepo.GetFamiliarUserNames(word));
        }

        [HttpPost("Search")]
        public ActionResult<ViolationSearchItems> GetSearchItems(ViolationSearchItems searchItem)
        {
            var item = violationRepo.GetByGivenSettings(searchItem.SearchWord, searchItem.DateFrom,
                searchItem.DateTo, searchItem.Order, searchItem.ShowStatus, out int foundCount, searchItem.CurrentPage);

            searchItem.Violations = mapper.Map<ViolationItemViewModel[]>(item);
            searchItem.FoundCount = foundCount;
            searchItem.FoundOnThisPage = searchItem.Violations.Length;

            return searchItem;
        }

        [HttpPost("Declaration")]
        public ActionResult<ViolationDeclarationViewModel> AddViolationDeclaration(ViolationDeclarationViewModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var violationDecl = mapper.Map<Violation>(item);
            if (!violationRepo.AddViolation(violationDecl, item.UserLogin, item.BlamedUserLogin))
            {
                return BadRequest(item);
            }

            item.RedirectLink = Url.Action("Account", "Police");
            return Ok(item);
        }
    }
}
