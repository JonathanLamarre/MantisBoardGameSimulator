using System.Collections.Immutable;
using System.Linq;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class StealHighestScoreElseMark : ILogic
    {
        private readonly int m_stealIf;

        public string Name => $"{nameof(StealHighestScoreElseMark)} {m_stealIf}";

        public StealHighestScoreElseMark(int stealIf)
        {
            m_stealIf = stealIf;
        }

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            // Mark if there is a chance of winning by doing it
            if (backCard.Back.Any(x => currentPlayer.Tank[x] > 0 && currentPlayer.Tank[x] + currentPlayer.Score >= 10))
            {
                return (Mark, null);
            }

            // Steal the player with current highest score if higher than current.
            // In case of equality, steal the one with most cards to steal.
            Player playerToSteal = otherPlayers
                .Where(x => x.Score >= currentPlayer.Score)
                .Where(x => backCard.Back.Count(y => x.Tank[y] > 0) >= m_stealIf)
                .OrderBy(x => x.Score)
                .ThenBy(x => backCard.Back.Aggregate(0, (i, color) => x.Tank[color] + i))
                .LastOrDefault();

            if (playerToSteal != null)
            {
                return (Steal, playerToSteal);
            }

            return (Mark, null);
        }
    }
}