using System;
using strange.extensions.command.impl;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    public class JoinRoomCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(JoinRoomCommand));

        [Inject]
        public string RoomName { get; set; }

        [Inject]
        public MessageSignal MessageSignal { get; set; }

        [Inject]
        public IPlayer _player { get; set; }

        [Inject]
        public IRoom _room { get; set; }

        public override void Execute()
        {
            if (IsInputValid())
            {
                //TODO

                Logger.InfoFormat("Player {0} joined Room {}", _player, _room);
            }
        }

        private bool IsInputValid()
        {
            //TODO check if room exists.
            if (String.IsNullOrEmpty(RoomName))
            {
                if (String.IsNullOrEmpty(RoomName))
                {
                    string errorMessage = "Room name wasn't choosen yet";
                    Logger.Error(errorMessage);
                    MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                    return false;
                }
            }

            return true;
        }
    }
}