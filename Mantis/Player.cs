using System.Collections.Immutable;
using Mantis.Logics;
using static Mantis.Color;

namespace Mantis
{
    public class Player
    {
        private readonly ILogic m_logic;

        public ImmutableDictionary<Color, byte> Tank { get; private set; } = ImmutableDictionary<Color, byte>
            .Empty
            .Add(Orange, 0)
            .Add(Pink, 0)
            .Add(Red, 0)
            .Add(Blue, 0)
            .Add(Yellow, 0)
            .Add(Green, 0)
            .Add(Purple, 0);

        public byte Score { get; private set; }

        public Player(Deck deck, ILogic logic)
        {
            m_logic = logic;

            for (int i = 0; i < 4; i++)
            {
                Card card = deck.Cards.Pop();
                Color color = card.Front;
                Tank = Tank.SetItem(color, (byte)(Tank[color] + 1));
            }
        }

        public void PlayTurn(Deck deck, ImmutableList<Player> players)
        {
            (Action action, Player player) = m_logic.GetAction(this, players.Remove(this), deck.Cards.Peek());

            if (action == Action.Mark)
            {
                Mark(deck);
            }
            else if (action == Action.Steal)
            {
                Steal(deck, player);
            }
        }

        private void Mark(Deck deck)
        {
            Card card = deck.Cards.Pop();

            if (Tank[card.Front] > 0)
            {
                Score += (byte)(Tank[card.Front] + 1);
                Tank = Tank.SetItem(card.Front, 0);
            }
            else
            {
                Tank = Tank.SetItem(card.Front, 1);
            }
        }

        private void Steal(Deck deck, Player player)
        {
            Card card = deck.Cards.Pop();
            Color color = card.Front;
            int numberOfCardsToSteal = player.Tank[color];

            if (numberOfCardsToSteal == 0)
            {
                player.Tank = player.Tank.SetItem(color, 1);
            }
            else
            {
                Tank = Tank.SetItem(color, (byte)(Tank[color] + numberOfCardsToSteal));
                player.Tank = player.Tank.SetItem(color, 0);
            }
        }
    }
}