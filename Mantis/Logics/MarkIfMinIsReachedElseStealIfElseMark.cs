using System.Collections.Immutable;
using System.Linq;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class MarkIfMinIsReachedElseStealIfElseMark : ILogic
    {
        private readonly int m_minScore;
        private readonly int m_minMin;
        private readonly int m_stealIf;

        public string Name => $"{nameof(MarkIfMinIsReachedElseStealIfElseMark)} {m_minScore} {m_minMin} {m_stealIf}";

        public MarkIfMinIsReachedElseStealIfElseMark(int minScore, int minMin, int stealIf)
        {
            m_minScore = minScore;
            m_minMin = minMin;
            m_stealIf = stealIf;
        }

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            // Mark if there is a chance of winning by doing it
            if (backCard.Back.Any(x => currentPlayer.Tank[x] > 0 && currentPlayer.Tank[x] + currentPlayer.Score >= 10))
            {
                return (Mark, null);
            }

            // Mark if there is a chance to get the target min score
            if (backCard.Back.Count(x => currentPlayer.Tank[x] >= m_minScore) >= m_minMin)
            {
                return (Mark, null);
            }

            // Steal if the target objective of back card's colors is attained in bank of another player
            Player playerToSteal = null;
            int playerToStealCards = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                if (backCard.Back.Count(x => otherPlayer.Tank[x] > 0) >= m_stealIf)
                {
                    // If multiple players fill the objective, steal the one with the most potential cards to steal
                    int average = backCard.Back.Aggregate(0, (i, color) => otherPlayer.Tank[color] + i);

                    if (average > playerToStealCards)
                    {
                        playerToStealCards = average;
                        playerToSteal = otherPlayer;
                    }
                }
            }

            if (playerToSteal != null)
            {
                return (Steal, playerToSteal);
            }

            return (Mark, null);
        }
    }
}
