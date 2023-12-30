using System.Collections.Immutable;
using System.Linq;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class AlwaysStealIf2On3 : ILogic
    {
        public string Name => nameof(AlwaysStealIf2On3);

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            Player playerToSteal = null;
            int playerToStealStealableCards = 0;

            foreach (Player otherPlayer in otherPlayers)
            {
                int sharedColors = backCard.Back.Count(x => otherPlayer.Tank[x] > 0);

                if (sharedColors >= 2)
                {
                    int stealableCards = backCard
                        .Back
                        .Aggregate(0, (i, color) => otherPlayer.Tank[color] + i);

                    if (stealableCards > playerToStealStealableCards)
                    {
                        playerToStealStealableCards = stealableCards;
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