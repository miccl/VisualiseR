﻿using strange.extensions.mediation.impl;

namespace VisualiseR.Main
{
    public class SettingsMediator : Mediator
    {
        [Inject]
        public SettingsView _view { get; set; }


        public override void OnRegister()
        {
        }

        public override void OnRemove()
        {
        }
    }
}