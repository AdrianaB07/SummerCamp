using AutoMapper;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Coach, CoachViewModel>().ReverseMap();
            CreateMap<Sponsor, SponsorViewModel>().ReverseMap();
            CreateMap<Team, TeamViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
            CreateMap<Competition, CompetitionViewModel>().ReverseMap();
            CreateMap<CompetitionMatch, CompetitionMatchViewModel>().ReverseMap();
            CreateMap<CompetitionTeam, CompetitionTeamViewModel>().ReverseMap();

        }
    }
}
