using strange.extensions.command.impl;
using UnityEngine;
using UnityEngine.TestTools;

namespace VisualiseR.CodeReview
{
    public class StartCommand : Command
    {
        public override void Execute()
        {
            Debug.Log("HEYOOOO!!!");
        }
    }
}