﻿using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace VisualiseR.Presentation
{
    public class PresentationContextMenuMediator : Mediator
    {
        [Inject]
        public PresentationContextMenuView _view { get; set; }

        [Inject]
        public ContextMenuCanceledSignal ContextMenuCanceledSignal { get; set; }

        [Inject]
        public ChangeTimerStatusSignal ChangeTimerStatusSignal { get; set; }

        [Inject]
        public SetTimerSignal SetTimerSignal { get; set; }

        [Inject]
        public TimerRunDownSignal TimerRunDownSignal { get; set; }

        [Inject]
        public ShowAllSignal ShowAllSignal{ get; set; }
        
        [Inject]
        public ShowTimeSignal ShowTimeSignal { get; set; }
        
        [Inject(ContextKeys.CONTEXT_VIEW)]
        public GameObject _contextView { get; set; }

        public override void OnRegister()
        {
            _view.OnContextMenuCanceled.AddListener(OnContextMenuCanceled);
            _view.ChangeTimerStatusSignal.AddListener(OnTimerStatusChanged);
            _view.SetTimerSignal.AddListener(SetTimer);
            _view.ShowAllSignal.AddListener(OnShowAll);
            _view.ShowTimerSignal.AddListener(OnShowTimerSignal);
            TimerRunDownSignal.AddListener(TimerRunDown);
            _view.Init(_contextView);
        }

        public override void OnRemove()
        {
            _view.OnContextMenuCanceled.RemoveListener(OnContextMenuCanceled);
            _view.ChangeTimerStatusSignal.RemoveListener(OnTimerStatusChanged);
            _view.SetTimerSignal.RemoveListener(SetTimer);
            _view.ShowAllSignal.RemoveListener(OnShowAll);
            _view.ShowTimerSignal.RemoveListener(OnShowTimerSignal);
            TimerRunDownSignal.RemoveListener(TimerRunDown);
        }

        private void OnShowTimerSignal(bool show)
        {
            ShowTimeSignal.Dispatch(show);
        }

        private void OnContextMenuCanceled()
        {
            ContextMenuCanceledSignal.Dispatch();
            Destroy(gameObject);
        }

        private void OnTimerStatusChanged(TimerTypes timerTypes)
        {
            ChangeTimerStatusSignal.Dispatch(timerTypes);
        }

        private void SetTimer(float timeInSeconds)
        {
            SetTimerSignal.Dispatch(timeInSeconds);
        }

        private void TimerRunDown()
        {
            _view.RefreshStartStopButtonText();
        }

        private void OnShowAll()
        {
            ShowAllSignal.Dispatch();
        }
    }
}