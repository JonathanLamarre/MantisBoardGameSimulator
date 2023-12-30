using System.Collections.Immutable;

namespace Mantis.Logics
{
    public enum Action
    {
        Mark,
        Steal
    }

    public interface ILogic
    {
        public string Name { get; }

        public (Action, Player) GetAction(Player currentPlayer, ImmutableList<Player> otherPlayers, IBackCard backCard);
    }
}