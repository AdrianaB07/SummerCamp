using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    public class CompetitionsController : Controller
    {
        private readonly ICompetitionRepository _competitionRepository;
        private readonly IMapper _mapper;
        private readonly ISponsorRepository _sponsorRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICompetitionMatchRepository _competitionMatchRepository;
        private readonly ICompetitionTeamRepository _competitionTeamRepository;
        public CompetitionsController(ICompetitionRepository competitionRepository, IMapper mapper, ICompetitionTeamRepository competitionTeamRepository, ISponsorRepository sponsorRepository, ITeamRepository teamRepository)
        {
            _competitionRepository = competitionRepository;
            _mapper = mapper;
            _sponsorRepository = sponsorRepository;
            _teamRepository = teamRepository;
            _competitionTeamRepository = competitionTeamRepository;
        }

        public IActionResult MultiSelect(int competitionId)
        {
            var allTeams = _teamRepository.GetAll().ToList();
            var availableTeams = new List<Team>();
            var competitionTeamMappings = _competitionTeamRepository.GetAll().ToList();
            foreach (var team in allTeams)
            {
                var map = competitionTeamMappings.FirstOrDefault(x => x.TeamId == team.Id);
                if (map != null)
                {
                    if (map.CompetitionId == competitionId || map.CompetitionId == null)
                    {
                        availableTeams.Add(team);
                    }
                    continue;
                }
                availableTeams.Add(team);

            }
            var competition = _competitionRepository.Get(c => c.Id == competitionId).FirstOrDefault();

            var competitionViewModel = _mapper.Map<CompetitionViewModel>(competition);

            var selectedTeamIds = competition?.CompetitionTeams.Select(c => c.Team.Id).ToList();
            competitionViewModel.SelectedTeamIds = selectedTeamIds;
            competitionViewModel.AvailableTeams = availableTeams;

            return View(competitionViewModel);
        }

        [HttpPost]
        public IActionResult MultiSelect(CompetitionViewModel competitionViewModel)
        {
            var availableTeams = _teamRepository.GetAll().ToList();
            var competition = _competitionRepository.GetById(competitionViewModel.Id);

            if (competition == null)
            {
                return View(competitionViewModel);
            }

            var previouslySelectedCompetitionIds = competition.CompetitionTeams.Select(x => x.TeamId).ToList();
            var notSelectedTeams = competition.CompetitionTeams.Where(ct => !competitionViewModel.SelectedTeamIds.Contains(ct.TeamId ?? 0)).ToList();

            _competitionTeamRepository.RemoveRange(notSelectedTeams);
            if (competitionViewModel.SelectedTeamIds != null)
            {
                foreach (var teamId in competitionViewModel.SelectedTeamIds)
                {
                    if (!previouslySelectedCompetitionIds.Contains(teamId))
                    {
                        var competitionTeam = new CompetitionTeam
                        {
                            TeamId = teamId,
                            CompetitionId = competition.Id,
                        };
                        _competitionTeamRepository.Update(_mapper.Map<CompetitionTeam>(competitionTeam));
                        _competitionTeamRepository.Save();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int competitionId)
        {
            var competition = _competitionRepository.Get(c => c.Id == competitionId).FirstOrDefault();

            if (competition == null)
            {
                return NotFound(); // Returnați o pagină de eroare sau un mesaj corespunzător
            }

            var competitionViewModel = _mapper.Map<CompetitionViewModel>(competition);

            return View(competitionViewModel);
        }

        public IActionResult Index(CompetitionViewModel competitionViewModel)
        {
            var competitions = _competitionRepository.GetAll();
            var result = _mapper.Map<IList<CompetitionViewModel>>(competitions);
            return View(result);
        }

        public IActionResult Add()
        {
            var sponsors = _mapper.Map<List<SponsorViewModel>>(_sponsorRepository.GetAll());
            ViewData["Sponsors"] = sponsors;

            return View();
        }

        [HttpPost]
        public IActionResult Add(CompetitionViewModel competitionViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionToAdd = _mapper.Map<Competition>(competitionViewModel);
                _competitionRepository.Add(competitionToAdd);
                _competitionRepository.Save();
                return RedirectToAction("Index");
            }
            return View(competitionViewModel);
        }

        public IActionResult Edit(int competitionId)
        {
            var competition = _competitionRepository.GetById(competitionId);
            var sponsors = _mapper.Map<List<SponsorViewModel>>(_sponsorRepository.GetAll());
            ViewData["Sponsors"] = sponsors;
            var competitionViewModel = _mapper.Map<CompetitionViewModel>(competition);
            return View(competitionViewModel);
        }

        [HttpPost]
        public IActionResult Edit(CompetitionViewModel competitionViewModel)
        {

            if (ModelState.IsValid)
            {
                var competitionToAdd = _mapper.Map<Competition>(competitionViewModel);
                _competitionRepository.Update(competitionToAdd);
                _competitionRepository.Save();
                return RedirectToAction("Index");
            }
            return View(competitionViewModel);
        }

        public IActionResult Delete(int competitionId)
        {
            var competition = _competitionRepository.GetById(competitionId);
            _competitionRepository.Delete(competition);
            _competitionRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
