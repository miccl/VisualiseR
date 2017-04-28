using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class NextCodeCommand : Command
    {
        [Inject]
        public Player player{get;set;}

        public override void Execute()
        {
            if (AcessList.NavigateSlidesRight.Contains(player.Type))
            {

            }
        }
    }
}