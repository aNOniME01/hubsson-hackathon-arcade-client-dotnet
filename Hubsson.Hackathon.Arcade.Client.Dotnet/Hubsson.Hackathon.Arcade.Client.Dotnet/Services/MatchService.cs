using Hubsson.Hackathon.Arcade.Client.Dotnet.Contracts;
using Hubsson.Hackathon.Arcade.Client.Dotnet.Domain;
using System;
using System.Dynamic;
using System.Numerics;
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

            matchRepository.Height = gameState.height;
            matchRepository.Width = gameState.width;

            matchRepository.Players = gameState.players;
            matchRepository.GetPlayer("JIF-PT34728");

            matchRepository.CheckWaypoints();
            //ShortestRoute((int)matchRepository.Waypoints[0].X, (int)matchRepository.Waypoints[0].Y);

            matchRepository.CanMove(gameState.height, gameState.width);


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

        private List<Direction> ShortestRoute(/*ClientGameState gameState,*/ int x, int y)
        {
            MatchRepository matchRepository = _matchRepository;

            List<Direction> shortestRoute = new List<Direction>();

            //matchRepository.Players = gameState.players;
            //matchRepository.CanMove(gameState.height, gameState.width);

            Coordinate start = _matchRepository.Player.coordinates.Last();
            //Coordinate destination = _matchRepository.Players.FirstOrDefault(x => x.playerId != _matchRepository.Player.playerId).coordinates.Last();

            int distanceBetweenHardcoded_X = x - start.x;
            int distanceBetweenHardcoded_Y = y - start.y;

            //int distanceBetweenStar_End_X = destination.x - start.x;
            //int distanceBetweenStar_End_Y = destination.y - start.y;

            //if (distanceBetweenStar_End_Y < 0)
            //{
            //    for (int i = 0; i < Math.Abs(distanceBetweenStar_End_Y); i++)
            //    {
            //        shortestRoute.Add(Direction.Left);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < Math.Abs(distanceBetweenStar_End_Y); i++)
            //    {
            //        shortestRoute.Add(Direction.Right);
            //    }
            //}
            //Console.WriteLine(shortestRoute.Last());
            //Console.WriteLine($"start x: {start.y}, dest x: {destination.y}, distance: {distanceBetweenStar_End_Y}");
            //if (distanceBetweenStar_End_X < 0)
            //{
            //    for (int i = 0; i < Math.Abs(distanceBetweenStar_End_X); i++)
            //    {
            //        shortestRoute.Add(Direction.Down);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < distanceBetweenStar_End_X; i++)
            //    {
            //        shortestRoute.Add(Direction.Up);
            //    }
            //}

            if (distanceBetweenHardcoded_X != 0 && distanceBetweenHardcoded_Y == 0)
            {
                if (distanceBetweenHardcoded_X < 0)
                {
                    for (int i = 0; i < Math.Abs(distanceBetweenHardcoded_X); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Left);
                    }
                }
                if (distanceBetweenHardcoded_X > 0)
                {
                    for (int i = 0; i < distanceBetweenHardcoded_X; i++)
                    {
                        shortestRoute.Add(Domain.Direction.Right);
                    }
                }
                if (distanceBetweenHardcoded_Y < 0)
                {
                    for (int i = 0; i < Math.Abs(distanceBetweenHardcoded_Y); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Right);
                    }
                }
                if (distanceBetweenHardcoded_Y > 0)
                {
                    for (int i = 0; i < (distanceBetweenHardcoded_Y); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Left);
                    }
                }
            }
            else
            {
                if (distanceBetweenHardcoded_X < 0)
                {
                    for (int i = 0; i < Math.Abs(distanceBetweenHardcoded_X); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Down);
                    }
                }
                if (distanceBetweenHardcoded_X > 0)
                {
                    for (int i = 0; i < distanceBetweenHardcoded_X; i++)
                    {
                        shortestRoute.Add(Domain.Direction.Up);
                    }
                }
                if (distanceBetweenHardcoded_Y < 0)
                {
                    for (int i = 0; i < Math.Abs(distanceBetweenHardcoded_Y); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Up);
                    }
                }
                if (distanceBetweenHardcoded_Y > 0)
                {
                    for (int i = 0; i < (distanceBetweenHardcoded_Y); i++)
                    {
                        shortestRoute.Add(Domain.Direction.Down);
                    }
                }
            }

            Console.WriteLine(shortestRoute.Last());
            Console.WriteLine($"start x: {start.x}, dest x: {x}, distance: {distanceBetweenHardcoded_X}");

            //if (distanceBetweenHardcoded_Y != 0 && distanceBetweenHardcoded_X == 0)
            //{

            //}
            //else
            //{
            //    if (distanceBetweenHardcoded_Y < 0)
            //    {
            //        for (int i = 0; i < Math.Abs(distanceBetweenHardcoded_Y); i++)
            //        {
            //            shortestRoute.Add(Domain.Direction.Right);
            //        }
            //    }
            //    if (distanceBetweenHardcoded_Y > 0)
            //    {
            //        for (int i = 0; i < (distanceBetweenHardcoded_Y); i++)
            //        {
            //            shortestRoute.Add(Domain.Direction.Left);
            //        }
            //    }
            //}

            Console.WriteLine(shortestRoute.Last());
            Console.WriteLine($"start y: {start.y}, dest y: {y}, distance: {distanceBetweenHardcoded_Y}");


            return shortestRoute;

        }
        private class MatchRepository
        {
            // Write your data fields here what you would like to store between the match rounds
            public PlayerCoordinates[] Players { get; set; }
            public PlayerCoordinates Player { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public bool[] AvalibleDirections { get; set; } // 0 = up, 1 = down, 2 = left, 3 = right
            public List<Direction> ShortestRoute { get; set; }

            public List<Vector2> Waypoints { get; set; }

            private Random rnd = new Random();
            public MatchRepository()
            {
                Waypoints = new List<Vector2>();
                Waypoints.Add(new Vector2(rnd.Next(0, 50), rnd.Next(0, 50)));
                Waypoints.Add(new Vector2(rnd.Next(0, 50), rnd.Next(0, 50)));
                Waypoints.Add(new Vector2(rnd.Next(0, 50), rnd.Next(0, 50)));
            }


            public void CheckWaypoints()
            {
                foreach (var player in Players)
                {
                    foreach (var cord in player.coordinates)
                    {
                        if (Waypoints.Count > 0)
                        {
                            if (Waypoints[0].X == cord.x && Waypoints[0].Y == cord.y)
                            {
                                Waypoints.Remove(Waypoints[0]);
                            }
                        }
                    }
                }
            }

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