using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    public class CompetitionTeamsController : Controller
    {
        private readonly ICompetitionTeamRepository _competitionTeamRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        public CompetitionTeamsController(ITeamRepository teamRepository, IMapper mapper, ICompetitionTeamRepository competitionTeamRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _competitionTeamRepository = competitionTeamRepository;
        }

        public IActionResult Index()
        {
            var teams = _competitionTeamRepository.GetAll();
            var result = _mapper.Map<IList<CompetitionTeamsController>>(teams);
            return View(result);
        }

        public IActionResult Add()
        {
            var teams = _mapper.Map<List<TeamViewModel>>(_teamRepository.Get(x => x.CompetitionTeams.Count() == 0));
            ViewData["Teams"] = teams;
            return View();
        }

        [HttpPost]
        public IActionResult Add(CompetitionTeamViewModel competitionTeamViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionTeamToAdd = _mapper.Map<CompetitionTeam>(competitionTeamViewModel);
                _competitionTeamRepository.Add(competitionTeamToAdd);
                _competitionTeamRepository.Save();
                return RedirectToAction("Index");
            }
            return View(competitionTeamViewModel);
        }

        public IActionResult Edit(int competitionTeamId)
        {
            var competitionTeam = _competitionTeamRepository.GetById(competitionTeamId);
            var teams = _mapper.Map<List<TeamViewModel>>(_teamRepository.GetAll());
            ViewData["Teams"] = teams;
            var competitionTeamViewModel = _mapper.Map<CompetitionTeamViewModel>(competitionTeam);
            return View(competitionTeamViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CompetitionTeamViewModel competitionTeamViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionTeamToAdd = _mapper.Map<CompetitionTeam>(competitionTeamViewModel);
                _competitionTeamRepository.Update(competitionTeamToAdd);
                _competitionTeamRepository.Save();
                return RedirectToAction("Index");
            }
            return View(competitionTeamViewModel);
        }

        public IActionResult Delete(int competitionTeamId)
        {
            var competitionTeam = _competitionTeamRepository.GetById(competitionTeamId);
            _competitionTeamRepository.Delete(competitionTeam);
            _competitionTeamRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
