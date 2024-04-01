using System.Collections.Immutable;
using System.Linq;
using Mantis.Logics;

namespace Mantis
{
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
