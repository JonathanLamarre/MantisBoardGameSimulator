using System.Collections.Immutable;

namespace Mantis
{
    public interface IBackCard
    {
        ImmutableHashSet<Color> Back { get; }
    }

    public class Card : IBackCard
    {
        public Color Front { get; }

        public ImmutableHashSet<Color> Back { get; }

        public Card(Color front, Color back1, Color back2, Color back3)
        {
            Front = front;
            Back = ImmutableHashSet.Create(back1, back2, back3);
        }
    }
}