using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly ICoachRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;
        public TeamsController(ITeamRepository teamRepository, IMapper mapper, ICoachRepository coachRepository, IPlayerRepository playerRepository)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _coachRepository = coachRepository;
            _playerRepository = playerRepository;
        }

        public IActionResult Details(int teamId)
        {
            var team = _teamRepository.Get(x => x.Id == teamId).FirstOrDefault();

            var teamViewModel = _mapper.Map<TeamViewModel>(team);

            var players = _playerRepository.Get(p => p.TeamId == teamId);
            teamViewModel.SelectedPlayers = _mapper.Map<List<PlayerViewModel>>(players);

            return View(teamViewModel);
        }

        public IActionResult MultiSelect(int teamId)
        {
            TeamViewModel model = _mapper.Map<TeamViewModel>(_teamRepository.GetById(teamId));

            var players = _playerRepository.Get(p => p.TeamId == null || p.TeamId == teamId);
            model.SelectedPlayerIds = players.Where(p => p.TeamId == teamId).Select(p => p.Id).ToList();

            model.AvailablePlayers = _mapper.Map<List<PlayerViewModel>>(players);

            return View(model);
        }

        [HttpPost]
        public IActionResult MultiSelect(TeamViewModel teamViewModel)
        {
            var availablePlayers = _playerRepository.Get(p => p.TeamId == null || p.TeamId == teamViewModel.Id);
            foreach (var player in availablePlayers)
            {
                if (teamViewModel.SelectedPlayerIds.Contains(player.Id))
                {
                    player.TeamId = teamViewModel.Id;
                }
                else
                {
                    player.TeamId = null;
                }

            }
            _playerRepository.Save();
            //teamViewModel.SelectedPlayers = selectedPlayers;

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Index(TeamViewModel teamViewModel)
        {
            var teams = _teamRepository.GetAll();
            var result = _mapper.Map<IList<TeamViewModel>>(teams);
            return View(result);
        }

        public IActionResult Add()
        {
            var coaches = _mapper.Map<List<CoachViewModel>>(_coachRepository.GetAll());
            ViewData["Coaches"] = coaches;
            return View();
        }

        [HttpPost]
        public IActionResult Add(TeamViewModel teamViewModel)
        {

            if (ModelState.IsValid)
            {
                var teamToAdd = _mapper.Map<Team>(teamViewModel);
                _teamRepository.Add(teamToAdd);
                _teamRepository.Save();
                return RedirectToAction("Index");
            }
            return View(teamViewModel);
        }

        public IActionResult Edit(int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            var coaches = _mapper.Map<List<CoachViewModel>>(_coachRepository.GetAll());
            ViewData["Coaches"] = coaches;
            var teamViewModel = _mapper.Map<TeamViewModel>(team);
            return View(teamViewModel);
        }

        [HttpPost]
        public IActionResult Edit(TeamViewModel teamViewModel)
        {

            if (ModelState.IsValid)
            {
                var teamToAdd = _mapper.Map<Team>(teamViewModel);
                _teamRepository.Update(teamToAdd);
                _teamRepository.Save();
                return RedirectToAction("Index");
            }
            return View(teamViewModel);
        }

        public IActionResult Delete(int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            _teamRepository.Delete(team);
            _teamRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
