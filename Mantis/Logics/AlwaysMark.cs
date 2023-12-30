using System.Collections.Immutable;
using static Mantis.Logics.Action;

namespace Mantis.Logics
{
    public class AlwaysMark : ILogic
    {
        public string Name => nameof(AlwaysMark);

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard)
        {
            return (Mark, null);
        }
    }
}