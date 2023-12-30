using System.Collections.Immutable;
using System.Linq;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class MarkIfElseStealIfElseMark : ILogic
    {
        private readonly int m_scoreIf;
        private readonly int m_stealIf;

        public string Name => $"{nameof(MarkIfElseStealIfElseMark)} {m_scoreIf} {m_stealIf}";

        public MarkIfElseStealIfElseMark(int scoreIf, int stealIf)
        {
            m_scoreIf = scoreIf;
            m_stealIf = stealIf;
        }

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            // Mark if the target objective of back card's colors is attained in bank of current player
            if (backCard.Back.Count(x => currentPlayer.Tank[x] > 0) >= m_scoreIf)
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
