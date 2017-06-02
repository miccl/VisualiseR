using System;
using strange.examples.multiplecontexts.main;
using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// Creates a <see cref="IRoom"/>.
    /// Constructs a Room with the choosen <see cref="RoomType"/> and <see cref="IPictureMedium"/>.
    /// Adds the <see cref="IPlayer"/> to the Room .
    /// </summary>
    public class CreateRoomCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CreateRoomCommand));


        [Inject]
        public string _RoomName { get; set; }

        [Inject]
        public RoomType _RoomType { get; set; }

        [Inject]
        public PictureMedium _medium { get; set; }

        [Inject]
        public IRoom Room { get; set; }

        [Inject]
        public IPlayer Player { get; set; }

        [Inject]
        public MessageSignal MessageSignal { get; set; }

        public override void Execute()
        {
            Logger.DebugFormat("RoomName: {0}", _RoomName);
            Logger.DebugFormat("RoomType: {0}", _RoomType);
            Logger.DebugFormat("pictureMedium: {0}", _medium);
            if (!IsInputValid())
            {
                return;
            }

            ConstructRoom();

            PlayerPrefsUtil.saveObject(PlayerPrefsUtil.ROOM_KEY, Room);
            LoadScene();
        }

        private void LoadScene()
        {
            if (!_RoomType.Equals(RoomType.Presentation) && !_RoomType.Equals(RoomType.CodeReview))
            {
                MessageSignal.Dispatch(new Message(MessageType.Info, "Not implemenented yet", "Scene coming soon (TM)"));
                return;
            }
            
            UnityUtil.LoadScene(_RoomType);
        }

        private bool RoomAlreadyExists()
        {
            //TODO machen

            if (_RoomType.Equals(RoomType.Presentation))
            {
//                return PhotonNetwork.CreateRoom(_RoomName);
            }
            return false;
        }


        private void ConstructRoom()
        {
            Room.Name = _RoomName;
            Room.Type = _RoomType;
            Room.Medium = _medium;
            AddHostPlayer();
            Logger.InfoFormat("Room {0} was created", Room);
        }

        private void AddHostPlayer()
        {
            var playerName = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            Player.Name = playerName;
            Player.Type = PlayerType.Host;
            Room.AddPlayer(Player);
        }

        /// <summary>
        /// Test, if all needed information are choosen.
        /// If not, a error message is displayed with
        /// </summary>
        private bool IsInputValid()
        {
//TODO vielleicht andere Validierungen hinzufügen (keine Sonderzeichen...)
//TODO Photon ist der Raumname schon vergeben???
            if (String.IsNullOrEmpty(_RoomName))
            {
                string errorMessage = "Room name wasn't choosen yet";
                Logger.Error(errorMessage);

                MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }
            if (_medium.IsEmpty())
            {
                string errorMessage = "PictureMedium wasn't choosen yet";
                Logger.Error(errorMessage);
                MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }

            if (RoomAlreadyExists())
            {
                string errorMessage = string.Format("Room with name {0} already exists", _RoomName);
                Logger.Error(errorMessage);
                MessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }
            return true;
        }
    }
}