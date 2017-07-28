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
        public ShowMessageSignal ShowMessageSignal { get; set; }

        public override void Execute()
        {

            ConstructRoom();

            PlayerPrefsUtil.SaveObject(PlayerPrefsUtil.ROOM_KEY, Room);
            ShowMessageSignal.Dispatch(new Message(MessageType.Info, "Sucess", string.Format("Room {0} was sucessfully joined", _roomName)));
            UnityUtil.LoadScene(Room.Type);
        }

        private void ConstructRoom()
        {
            Room.Name = _roomName;
            
            //TODO let the user choose the room type
            Room.Type = RoomType.Presentation;
            
        }
    }
}