using strange.extensions.mediation.impl;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    /// <summary>
    /// Mediator for the <see cref="SimpleCodeReviewScreenView"/>
    /// </summary>
    public class SimpleCodeReviewScreenMediator : Mediator
    {
        [Inject]
        public SimpleCodeReviewScreenView _view { get; set; }

        [Inject]
        public CodeSelectedSignal CodeSelectedSignal { get; set; }
        
        
        public override void OnRegister()
        {
            _view.CodeSelectedSignal.AddListener(OnCodeSelected);
        }

        public override void OnRemove()
        {
            _view.CodeSelectedSignal.RemoveListener(OnCodeSelected);

        }

        private void OnCodeSelected(ICode code, IPlayer player)
        {
            CodeSelectedSignal.Dispatch((Code) code, (Player) player);
        }
    }
}