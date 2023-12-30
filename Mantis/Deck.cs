using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static Mantis.Color;

namespace Mantis
{
    public class Deck
    {
        private static readonly ImmutableList<Card> s_cards = ImmutableList.Create
        (
            // Orange
            new Card(Orange, Orange, Green, Purple),
            new Card(Orange, Yellow, Purple, Orange),
            new Card(Orange, Orange, Yellow, Red),
            new Card(Orange, Orange, Purple, Red),
            new Card(Orange, Pink, Green, Orange),
            new Card(Orange, Red, Orange, Pink),
            new Card(Orange, Orange, Blue, Pink),
            new Card(Orange, Green, Yellow, Orange),
            new Card(Orange, Orange, Green, Red),
            new Card(Orange, Orange, Blue, Red),
            new Card(Orange, Pink, Purple, Orange),
            new Card(Orange, Orange, Yellow, Pink),
            new Card(Orange, Blue, Yellow, Orange),
            new Card(Orange, Orange, Purple, Blue),
            new Card(Orange, Green, Orange, Blue),
            // Pink
            new Card(Pink, Red, Orange, Pink),
            new Card(Pink, Blue, Pink, Purple),
            new Card(Pink, Green, Pink, Yellow),
            new Card(Pink, Orange, Yellow, Pink),
            new Card(Pink, Pink, Purple, Orange),
            new Card(Pink, Pink, Purple, Red),
            new Card(Pink, Pink, Green, Purple),
            new Card(Pink, Red, Yellow, Pink),
            new Card(Pink, Blue, Yellow, Pink),
            new Card(Pink, Pink, Green, Orange),
            new Card(Pink, Pink, Green, Red),
            new Card(Pink, Purple, Yellow, Pink),
            new Card(Pink, Orange, Blue, Pink),
            new Card(Pink, Blue, Green, Pink),
            new Card(Pink, Pink, Blue, Red),
            // Red
            new Card(Red, Orange, Yellow, Red),
            new Card(Red, Pink, Purple, Red),
            new Card(Red, Blue, Red, Purple),
            new Card(Red, Orange, Green, Red),
            new Card(Red, Orange, Blue, Red),
            new Card(Red, Purple, Yellow, Red),
            new Card(Red, Blue, Yellow, Red),
            new Card(Red, Pink, Green, Red),
            new Card(Red, Purple, Green, Red),
            new Card(Red, Pink, Blue, Red),
            new Card(Red, Blue, Green, Red),
            new Card(Red, Orange, Purple, Red),
            new Card(Red, Red, Yellow, Pink),
            new Card(Red, Green, Yellow, Red),
            new Card(Red, Orange, Pink, Red),
            // Blue
            new Card(Blue, Blue, Red, Purple),
            new Card(Blue, Pink, Blue, Red),
            new Card(Blue, Blue, Pink, Purple),
            new Card(Blue, Blue, Yellow, Orange),
            new Card(Blue, Blue, Green, Pink),
            new Card(Blue, Blue, Green, Red),
            new Card(Blue, Blue, Yellow, Pink),
            new Card(Blue, Green, Orange, Blue),
            new Card(Blue, Blue, Yellow, Green),
            new Card(Blue, Orange, Blue, Red),
            new Card(Blue, Blue, Yellow, Purple),
            new Card(Blue, Purple, Green, Blue),
            new Card(Blue, Orange, Blue, Pink),
            new Card(Blue, Orange, Purple, Blue),
            new Card(Blue, Blue, Yellow, Red),
            // Yellow
            new Card(Yellow, Blue, Yellow, Red),
            new Card(Yellow, Yellow, Purple, Orange),
            new Card(Yellow, Green, Yellow, Red),
            new Card(Yellow, Red, Yellow, Pink),
            new Card(Yellow, Green, Yellow, Orange),
            new Card(Yellow, Blue, Yellow, Orange),
            new Card(Yellow, Orange, Yellow, Red),
            new Card(Yellow, Blue, Yellow, Green),
            new Card(Yellow, Purple, Yellow, Pink),
            new Card(Yellow, Green, Yellow, Purple),
            new Card(Yellow, Blue, Yellow, Pink),
            new Card(Yellow, Orange, Yellow, Pink),
            new Card(Yellow, Blue, Yellow, Purple),
            new Card(Yellow, Green, Pink, Yellow),
            new Card(Yellow, Purple, Yellow, Red),
            // Green
            new Card(Green, Orange, Green, Red),
            new Card(Green, Green, Yellow, Orange),
            new Card(Green, Pink, Green, Red),
            new Card(Green, Purple, Green, Red),
            new Card(Green, Green, Yellow, Purple),
            new Card(Green, Purple, Green, Blue),
            new Card(Green, Blue, Green, Pink),
            new Card(Green, Green, Pink, Yellow),
            new Card(Green, Green, Orange, Blue),
            new Card(Green, Blue, Green, Red),
            new Card(Green, Orange, Green, Purple),
            new Card(Green, Blue, Yellow, Green),
            new Card(Green, Pink, Green, Purple),
            new Card(Green, Pink, Green, Orange),
            new Card(Green, Green, Yellow, Red),
            // Purple
            new Card(Purple, Blue, Yellow, Purple),
            new Card(Purple, Purple, Green, Red),
            new Card(Purple, Yellow, Purple, Orange),
            new Card(Purple, Purple, Yellow, Pink),
            new Card(Purple, Blue, Red, Purple),
            new Card(Purple, Green, Yellow, Purple),
            new Card(Purple, Blue, Pink, Purple),
            new Card(Purple, Orange, Purple, Blue),
            new Card(Purple, Pink, Purple, Red),
            new Card(Purple, Orange, Green, Purple),
            new Card(Purple, Orange, Purple, Red),
            new Card(Purple, Pink, Green, Purple),
            new Card(Purple, Purple, Yellow, Red),
            new Card(Purple, Purple, Green, Blue),
            new Card(Purple, Pink, Purple, Orange)
        );

        public Stack<Card> Cards { get; } = new(s_cards.Count);

        public Deck()
        {
            var random = new Random();
            var cards = new List<Card>(s_cards);

            while (cards.Count > 0)
            {
                int index = random.Next(cards.Count);
                Cards.Push(cards[index]);
                cards.RemoveAt(index);
            }
        }
    }
}