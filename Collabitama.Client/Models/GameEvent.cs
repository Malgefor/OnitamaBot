using Collabitama.Client.Enums;

namespace Collabitama.Client.Models
{
    public abstract class Move
    {
        private Move(CardType usedCard)
        {
            this.UsedCard = usedCard;
        }

        public CardType UsedCard { get; set; }

        public class Pass : Move
        {
            public Pass(CardType usedCard)
                : base(usedCard)
            {
            }
        }

        public class Play : Move
        {
            public Play(CardType usedCard, Position from, Position to)
                : base(usedCard)
            {
                this.From = from;
                this.To = to;
            }

            public Position From { get; set; }

            public Position To { get; set; }
        }
    }
}
