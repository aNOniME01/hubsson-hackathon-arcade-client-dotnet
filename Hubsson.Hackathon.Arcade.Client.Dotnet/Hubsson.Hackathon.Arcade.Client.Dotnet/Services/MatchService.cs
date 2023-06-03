using Hubsson.Hackathon.Arcade.Client.Dotnet.Contracts;
using Hubsson.Hackathon.Arcade.Client.Dotnet.Domain;
using System;
using System.Dynamic;
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
            

            matchRepository.Players = gameState.players;
            matchRepository.GetPlayer("JIF-PT34728");
            matchRepository.CanMove(gameState.height, gameState.width);
            ShortestRoute(gameState);


            if (matchRepository.AvalibleDirections[0])
            {
                return new Domain.Action() { direction = Domain.Direction.Up, iteration = gameState.iteration };
            }
            else if (matchRepository.AvalibleDirections[1])
            {
                return new Domain.Action() { direction = Domain.Direction.Down, iteration = gameState.iteration };
            }
            else if (matchRepository.AvalibleDirections[2])
            {
                return new Domain.Action() { direction = Domain.Direction.Left, iteration = gameState.iteration };
            }
            else if (matchRepository.AvalibleDirections[3])
            {
                return new Domain.Action() { direction = Domain.Direction.Right, iteration = gameState.iteration };
            }

            throw new NotImplementedException();
        }
        private List<Direction> ShortestRoute(ClientGameState gameState)
        {
            MatchRepository matchRepository = _matchRepository;

            List<Direction> shortestRoute = new List<Direction>();

            matchRepository.Players = gameState.players;
            matchRepository.GetPlayer("JIF-PT34728");
            matchRepository.CanMove(gameState.height, gameState.width);

            Coordinate start = _matchRepository.Player.coordinates.Last();
            Coordinate destination = _matchRepository.Players.FirstOrDefault(x => x.playerId != _matchRepository.Player.playerId).coordinates.Last();

            int distanceBetweenStar_End_X = destination.x - start.x;
            int distanceBetweenStar_End_Y = destination.y - start.y;
            if (distanceBetweenStar_End_X < 0)
            {
                for (int i = 0; i < Math.Abs(distanceBetweenStar_End_X); i++)
                {
                    shortestRoute.Add(Direction.Down);
                }
            }
            else
            {
                for (int i = 0; i < distanceBetweenStar_End_X; i++)
                {
                    shortestRoute.Add(Direction.Up);
                }
            }

            if (distanceBetweenStar_End_Y < 0)
            {
                for (int i = 0; i < Math.Abs(distanceBetweenStar_End_Y); i++)
                {
                    shortestRoute.Add(Direction.Left);
                }
            }
            else
            {
                for (int i = 0; i < Math.Abs(distanceBetweenStar_End_Y); i++)
                {
                    shortestRoute.Add(Direction.Right);
                }
            }
            Console.WriteLine(shortestRoute.Last());
            Console.WriteLine($"start x: {start.x}, dest x: {destination.x}, distance: {distanceBetweenStar_End_X}");
            return shortestRoute;
            
        }

        private class MatchRepository
        {
            // Write your data fields here what you would like to store between the match rounds
            public PlayerCoordinates[] Players { get; set; }
            public PlayerCoordinates Player { get; set; }
            public bool[] AvalibleDirections { get; set; } // 0 = up, 1 = down, 2 = left, 3 = right
            public List<Direction> ShortestRoute { get; set; }
            public void GetPlayer(string pId) => Player = Players.FirstOrDefault(x => x.playerId == pId);

            #region CanMove

            public void CanMove(int height, int width)
            {
                AvalibleDirections = new bool[] { false, false, false, false };
                if (CanGoUp())
                {
                    AvalibleDirections[0] = true;
                }
                if (CanGoDown(height))
                {
                    AvalibleDirections[1] = true;
                }
                if (CanGoLeft())
                {
                    AvalibleDirections[2] = true;
                }
                if (CanGoRight(width))
                {
                    AvalibleDirections[3] = true;
                }
            }
            private bool CanGoUp()
            {
                bool up = true;
                if (Player.coordinates.Last().y - 1 <= -1)
                {
                    up = false;
                }

                foreach (var player in Players)
                {
                    foreach (var cord in player.coordinates)
                    {
                        if (cord.x == Player.coordinates.Last().x && cord.y == Player.coordinates.Last().y - 1)
                        {
                            up = false;
                        }
                    }
                }

                return up;

            }
            private bool CanGoDown(int height)
            {
                bool down = true;
                if (Player.coordinates.Last().y +1 >= height)
                {
                    down = false;
                }

                foreach (var player in Players)
                {
                    foreach (var cord in player.coordinates)
                    {
                        if (cord.x == Player.coordinates.Last().x && cord.y == Player.coordinates.Last().y + 1)
                        {
                            down = false;
                        }
                    }
                }

                return down;

            }
            private bool CanGoLeft()
            {
                bool left = true;
                if (Player.coordinates.Last().x -1 <= -1)
                {
                    left = false;
                }

                foreach (var player in Players)
                {
                    foreach (var cord in player.coordinates)
                    {
                        if (cord.x == Player.coordinates.Last().x - 1 && cord.y == Player.coordinates.Last().y)
                        {
                            left = false;
                        }
                    }
                }

                return left;

            }
            private bool CanGoRight(int width)
            {
                bool right = true;
                if (Player.coordinates.Last().x +1 >= width)
                {
                    right = false;
                }

                foreach (var player in Players)
                {
                    foreach (var cord in player.coordinates)
                    {
                        if (cord.x == Player.coordinates.Last().x + 1 && cord.y == Player.coordinates.Last().y)
                        {
                            right = false;
                        }
                    }
                }

                return right;

            }

            #endregion

        }
    }
}