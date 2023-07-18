using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    public class CompetitionMatchesController : Controller
    {
        private readonly ICompetitionMatchRepository _competitionMatchRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly ICompetitionTeamRepository _competitionTeamRepository;
        public CompetitionMatchesController(ICompetitionMatchRepository competitionMatchRepository, IMapper mapper, ITeamRepository teamRepository, ICompetitionTeamRepository competitionTeamRepository)
        {
            _competitionMatchRepository = competitionMatchRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _competitionTeamRepository = competitionTeamRepository;
        }

        public IActionResult Index(int competitionId)
        {
            var competitionMatches = _competitionMatchRepository.Get(x => x.CompetitionId == competitionId);
            var result = _mapper.Map<IList<CompetitionMatchViewModel>>(competitionMatches);
            ViewData["competitionId"] = competitionId;
            return View(result);
        }
        public IActionResult Add(int competitionId)
        {
            var competitionTeams = _competitionTeamRepository.Get(x => x.CompetitionId == competitionId);
            var teams = _mapper.Map<List<TeamViewModel>>(competitionTeams.Select(x => x.Team));
            ViewData["Teams"] = teams;
            return View(new CompetitionMatchViewModel
            {
                CompetitionId = competitionId
            });
        }

        [HttpPost]
        public IActionResult Add(CompetitionMatchViewModel competitionMatchViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionMatchToAdd = _mapper.Map<CompetitionMatch>(competitionMatchViewModel);
                _competitionMatchRepository.Add(competitionMatchToAdd);
                _competitionMatchRepository.Save();
                return RedirectToAction("Index", new { competitionId = competitionMatchViewModel.CompetitionId });
            }
            var competitionTeams = _competitionTeamRepository.Get(x => x.CompetitionId == competitionMatchViewModel.CompetitionId);
            var teams = _mapper.Map<List<TeamViewModel>>(competitionTeams.Select(x => x.Team));
            ViewData["Teams"] = teams;
            return View(competitionMatchViewModel);
        }

        public IActionResult Edit(int competitionMatchId)
        {
            var coach = _competitionMatchRepository.GetById(competitionMatchId);
            return View(_mapper.Map<CompetitionMatchViewModel>(coach));
        }

        [HttpPost]
        public IActionResult Edit(CompetitionMatchViewModel competitionMatchViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionMatchToAdd = _mapper.Map<CompetitionMatch>(competitionMatchViewModel);
                _competitionMatchRepository.Update(competitionMatchToAdd);
                _competitionMatchRepository.Save();
                return RedirectToAction("Index");
            }
            return View(competitionMatchViewModel);
        }

        public IActionResult Delete(int competitionMatchId)
        {
            var competitionMatch = _competitionMatchRepository.GetById(competitionMatchId);

            if (competitionMatch == null)
            {
                return NotFound();
            }

            _competitionMatchRepository.Delete(competitionMatch);
            _competitionMatchRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
