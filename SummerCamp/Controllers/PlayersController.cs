using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Enums;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        public PlayersController(IPlayerRepository playerRepository, IMapper mapper, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public IActionResult Index()
        {

            var players = _playerRepository.GetAll();
            var result = _mapper.Map<IList<PlayerViewModel>>(players);
            return View(result);
        }

        public IActionResult Add()
        {
            var teams = _mapper.Map<List<TeamViewModel>>(_teamRepository.GetAll());
            ViewData["Teams"] = teams;
            ViewData["Positions"] = GetPositions();

            return View();
        }

        [HttpPost]
        public IActionResult Add(PlayerViewModel playerViewModel)
        {

            if (ModelState.IsValid)
            {
                var playerToAdd = _mapper.Map<Player>(playerViewModel);
                _playerRepository.Add(playerToAdd);
                _playerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(playerViewModel);
        }

        public IActionResult Edit(int playerId)
        {
            var player = _playerRepository.GetById(playerId);
            var teams = _mapper.Map<List<TeamViewModel>>(_teamRepository.GetAll());
            ViewData["Teams"] = teams;
            ViewData["Positions"] = GetPositions();
            var playerViewModel = _mapper.Map<PlayerViewModel>(player);
            return View(playerViewModel);
        }

        [HttpPost]
        public IActionResult Edit(PlayerViewModel playerViewModel)
        {

            if (ModelState.IsValid)
            {
                var playerToAdd = _mapper.Map<Player>(playerViewModel);
                _playerRepository.Update(playerToAdd);
                _playerRepository.Save();
                return RedirectToAction("Index");
            }
            return View(playerViewModel);
        }

        public IActionResult Delete(int playerId)
        {
            var player = _playerRepository.GetById(playerId);
            _playerRepository.Delete(player);
            _playerRepository.Save();
            return RedirectToAction("Index");
        }

        private SelectList GetPositions()
        {
            var positions = from PositionEnum position in Enum.GetValues(typeof(PositionEnum))
                            select new
                            {
                                Id = (int)position,
                                Name = position.ToString()
                            };
            return new SelectList(positions, "Id", "Name");
        }
    }
}
