using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Mantis.Logics;

namespace Mantis
{
    public class Program
    {
        private const int NUMBER_OF_PLAYERS_BY_GAME = 4;

        public static void Main(string[] _)
        {
            var availableLogics = new List<ILogic>
            {
                new MarkIfElseStealIfElseMark(0, 0),
                new MarkIfElseStealIfElseMark(1, 1),
                new MarkIfElseStealIfElseMark(1, 2),
                new MarkIfElseStealIfElseMark(1, 3),
                new MarkIfElseStealIfElseMark(2, 1),
                new MarkIfElseStealIfElseMark(2, 2),
                new MarkIfElseStealIfElseMark(2, 3),
                new MarkIfElseStealIfElseMark(3, 1),
                new MarkIfElseStealIfElseMark(3, 2),
                new MarkIfElseStealIfElseMark(3, 3),
                new MarkIfElseStealIfElseMark(4, 1),
                new MarkIfElseStealIfElseMark(4, 2),
                new MarkIfElseStealIfElseMark(4, 3),
                new StealHighestScoreElseMark(1),
                new StealHighestScoreElseMark(2),
                new StealHighestScoreElseMark(3),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 1, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 1, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 1, 3),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 2, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 2, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 2, 3),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 3, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 3, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(2, 3, 3),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 1, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 1, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 1, 3),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 2, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 2, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 2, 3),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 3, 1),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 3, 2),
                new MarkIfMinIsReachedElseStealIfElseMark(3, 3, 3),
            };

            var logicNameToNumberOfWins = new Dictionary<string, int>();

            foreach (ILogic availableLogic in availableLogics)
            {
                logicNameToNumberOfWins[availableLogic.Name] = 0;
            }

            var positionToNumberOfWins = new Dictionary<int, int>();

            for (int i = 0; i < NUMBER_OF_PLAYERS_BY_GAME; i++)
            {
                positionToNumberOfWins[i] = 0;
            }

            var @lock = new object();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Parallel.For(0, 10000000, _ =>
            {
                var logics = new List<ILogic>();
                var random = new Random();

                for (int i = 0; i < NUMBER_OF_PLAYERS_BY_GAME; i++)
                {
                    logics.Add(availableLogics[random.Next(availableLogics.Count)]);
                }

                var game = new Game(logics.ToImmutableList());

                lock (@lock)
                {
                    logicNameToNumberOfWins[game.Winner.Name]++;
                    positionToNumberOfWins[game.WinnerPosition]++;
                }
            });
            
            stopwatch.Stop();

            foreach (KeyValuePair<string, int> kv in logicNameToNumberOfWins.OrderBy(x => x.Value))
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }

            for (int i = 0; i < NUMBER_OF_PLAYERS_BY_GAME; i++)
            {
                Console.WriteLine($"{i}: {positionToNumberOfWins[i]}");
            }

            Console.WriteLine($"{stopwatch.Elapsed}");
        }
    }

    public class Game
    {
        private const int SCORE_TO_WIN = 10;

        public ILogic Winner { get; }

        public int WinnerPosition { get; }

        public Game(ImmutableList<ILogic> logics)
        {
            var deck = new Deck();
            ImmutableList<Player> players = logics.Select(x => new Player(deck, x)).ToImmutableList();
            int indexOfPlayerPlaying = 0;

            while (true)
            {
                if (deck.Cards.Count == 0)
                {
                    Player winningPlayer = players
                        .OrderBy(x => x.Score)
                        .ThenBy(x => x.Tank.Aggregate(0, (i, y) => i + y.Value))
                        .Last();

                    int indexOfWinningPlayer = players.IndexOf(winningPlayer);
                    Winner = logics[indexOfWinningPlayer];
                    WinnerPosition = indexOfWinningPlayer;

                    break;
                }

                Player activePlayer = players[indexOfPlayerPlaying];
                activePlayer.PlayTurn(deck, players);

                if (activePlayer.Score >= SCORE_TO_WIN)
                {
                    int indexOfWinningPlayer = players.IndexOf(activePlayer);
                    Winner = logics[indexOfWinningPlayer];
                    WinnerPosition = indexOfWinningPlayer;

                    break;
                }

                indexOfPlayerPlaying = (indexOfPlayerPlaying + 1) % players.Count;
            }
        }
    }
}