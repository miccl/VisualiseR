﻿using System;
using strange.extensions.command.impl;

namespace VisualiseR.CodeReview
{
    public class RemoveCodeCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(RemoveCodeCommand));


        [Inject]
        public Code code { get; set; }

        public override void Execute()
        {
            Logger.InfoFormat("Code {0} was removed", code);
            throw new NotImplementedException();
        }
    }
}