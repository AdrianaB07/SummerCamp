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
                var competitionMatchTeams = _competitionTeamRepository.Get(x => x.TeamId == competitionMatchViewModel.AwayTeamId
                || x.TeamId == competitionMatchViewModel.HomeTeamId);
                var competitionMatchToAdd = _mapper.Map<CompetitionMatch>(competitionMatchViewModel);
                _competitionMatchRepository.Add(competitionMatchToAdd);
                _competitionMatchRepository.Save();

                if (competitionMatchToAdd.HomeTeamGoals == competitionMatchToAdd.AwayTeamGoals)
                {
                    foreach (var team in competitionMatchTeams)
                    {
                        team.TotalPoints += 1;
                    }
                }
                else if (competitionMatchToAdd.HomeTeamGoals > competitionMatchToAdd.AwayTeamGoals)
                {
                    var HomeTeam = competitionMatchTeams.FirstOrDefault(x => x.TeamId == competitionMatchToAdd.HomeTeamId);
                    HomeTeam.TotalPoints += 3;
                }
                else if (competitionMatchToAdd.HomeTeamGoals < competitionMatchToAdd.AwayTeamGoals)
                {
                    var AwayTeam = competitionMatchTeams.FirstOrDefault(x => x.TeamId == competitionMatchToAdd.AwayTeamId);
                    AwayTeam.TotalPoints += 3;
                }
                foreach (var team in competitionMatchTeams)
                {
                    _competitionTeamRepository.Update(team);
                }
                _competitionTeamRepository.Save();
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

            if (competitionMatch.HomeTeamGoals == competitionMatch.AwayTeamGoals)
            {
                competitionMatch.HomeTeam.CompetitionTeams.FirstOrDefault(t => t.CompetitionId == competitionMatch.CompetitionId).TotalPoints -= 1;
                competitionMatch.AwayTeam.CompetitionTeams.FirstOrDefault(t => t.CompetitionId == competitionMatch.CompetitionId).TotalPoints -= 1;

            }
            else if (competitionMatch.HomeTeamGoals > competitionMatch.AwayTeamGoals)
            {
                competitionMatch.HomeTeam.CompetitionTeams.FirstOrDefault(t => t.CompetitionId == competitionMatch.CompetitionId).TotalPoints -= 3;
            }
            else if (competitionMatch.HomeTeamGoals < competitionMatch.AwayTeamGoals)
            {
                competitionMatch.AwayTeam.CompetitionTeams.FirstOrDefault(t => t.CompetitionId == competitionMatch.CompetitionId).TotalPoints -= 3;

            }

            _competitionMatchRepository.Delete(competitionMatch);
            _competitionMatchRepository.Save();
            return RedirectToAction("Index", "Competitions");
        }
    }
}
