using System;
using System.ComponentModel;
using JetBrains.Annotations;
using strange.extensions.mediation.impl;
using UnityEditor;
using UnityEngine;
using VisualiseR.Common;

namespace VisualiseR.CodeReview
{
    public class CodeReviewScreenMediator : Mediator
    {
        [Inject]
        public CodeReviewScreenView _view { get; set; }

        [Inject]
        public MediumChangedSignal mediumChangedSignal { get; set; }

        [Inject]
        public LoadAndConvertFilesSignal LoadAndConvertFilesSignal { get; set; }

        [Inject]
        public CodePositionChangedSignal _CodePositionChangedSignal { get; set; }

        [Inject]
        public NextCodeSignal nextCodeSignal { get; set; }

        [Inject]
        public PrevCodeSignal prevCodeSignal { get; set; }


        public override void OnRegister()
        {
            mediumChangedSignal.AddListener(OnMediumChanged);
            _CodePositionChangedSignal.AddListener(OnCodePositionChanged);
            _view.nextCodeSignal.AddListener(OnNextCodeSignal);
            _view.prevCodeSignal.AddListener(OnPrevCodeSignal);
        }



        public override void OnRemove()
        {
            mediumChangedSignal.RemoveListener(OnMediumChanged);
            _CodePositionChangedSignal.RemoveListener(OnCodePositionChanged);
            _view.nextCodeSignal.RemoveListener(OnNextCodeSignal);
            _view.nextCodeSignal.RemoveListener(OnPrevCodeSignal);
        }

        public void OnMediumChanged(Medium medium)
        {
            _view._medium = medium;
            _view.SetupMedium();
        }

        private void OnNextCodeSignal(IPlayer player, IMedium medium, int pos)
        {
            nextCodeSignal.Dispatch((Player) player, (Medium) medium, pos);

        }

        private void OnPrevCodeSignal(IPlayer player, IMedium medium, int pos)
        {
            prevCodeSignal.Dispatch((Player) player, (Medium) medium, pos);
        }

        private void OnCodePositionChanged(int pos)
        {
            _view._currPicturePos = pos;
            _view.LoadPictureIntoTexture(pos);
        }
    }
}