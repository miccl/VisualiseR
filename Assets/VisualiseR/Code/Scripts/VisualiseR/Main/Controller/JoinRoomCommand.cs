using System;
using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// Command to join a room.
    /// Tests if input is valid and the room exists.
    /// </summary>
    public class JoinRoomCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(JoinRoomCommand));

        [Inject]
        public string _roomName { get; set; }
        
        [Inject]
        public IRoom Room { get; set; }

        [Inject]
        public MessageSignal MessageSignal { get; set; }

        public override void Execute()
        {
            if (!IsInputValid())
            {
                return;
            }

            ConstructRoom();

            PlayerPrefsUtil.saveObject(PlayerPrefsUtil.ROOM_KEY, Room);
            UnityUtil.LoadScene(Room.Type);
        }

        private void ConstructRoom()
        {
            Room.Name = _roomName;
            
            //TODO let the user choose the room type
            Room.Type = RoomType.Presentation;
            
        }

        private bool IsInputValid()
        {
            if (String.IsNullOrEmpty(_roomName))
            {
                string errorMessage = "Room name wasn't choosen yet";
                Logger.Error(errorMessage);
                MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }

            if (!RoomExists(_roomName))
            {
                string errorMessage = string.Format("Room with name '{0}' doesnt exist", _roomName);
                Logger.Error(errorMessage);
                MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }

            return true;
        }

        private bool RoomExists(string roomName)
        {
            //TODO check if room exists (similar to CreateRoomCommand)
            return true;
        }
    }
}