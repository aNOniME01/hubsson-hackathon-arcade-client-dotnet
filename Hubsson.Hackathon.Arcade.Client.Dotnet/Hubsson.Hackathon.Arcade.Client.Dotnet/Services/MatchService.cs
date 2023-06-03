using Hubsson.Hackathon.Arcade.Client.Dotnet.Contracts;
using System;
using ClientGameState = Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.ClientGameState;

namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Services
{
    public class MatchService
    {
        private MatchRepository _matchRepository;
        private ArcadeSettings _arcadeSettings;
        
        public MatchService(ArcadeSettings settings)
        {
            _matchRepository = new MatchRepository();
            _arcadeSettings = settings;
        }
        
        public void Init()
        {
            // On Game Init
            throw new NotImplementedException();
        }

        public Hubsson.Hackathon.Arcade.Client.Dotnet.Domain.Action Update(ClientGameState gameState)
        {
            // On Each Frame Update return an Action for your player

            MatchRepository matchRepository = _matchRepository;
            matchRepository.PlayerCords = gameState.players;
            
            return new Domain.Action() { direction= Domain.Direction.Right, iteration = gameState.iteration };
        }

        private class MatchRepository
        {
            // Write your data fields here what you would like to store between the match rounds
            public PlayerCoordinates[] PlayerCords { get; set; }

        }
    }
}