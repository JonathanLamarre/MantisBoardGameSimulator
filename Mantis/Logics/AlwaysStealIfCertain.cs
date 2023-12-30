using System.Collections.Immutable;
using System.Linq;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class AlwaysStealIfCertain : ILogic
    {
        public string Name => nameof(AlwaysStealIfCertain);

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            Player playerToSteal = null;
            double playerToStealAverage = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                if (backCard.Back.All(x => otherPlayer.Tank[x] > 0))
                {
                    double average = backCard.Back.Aggregate(0, (i, color) => otherPlayer.Tank[color] + i) / 3.0;

                    if (average > playerToStealAverage)
                    {
                        playerToStealAverage = average;
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