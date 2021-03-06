﻿using System;
using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    /// <summary>
    /// Command to construct a <see cref="IRoom"/> with the choosen <see cref="RoomType"/> and <see cref="IPictureMedium"/>.
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
        public ShowWindowMessageSignal ShowWindowMessageSignal { get; set; }

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

            PlayerPrefsUtil.SaveObject(PlayerPrefsUtil.ROOM_KEY, Room);
            ShowWindowMessageSignal.Dispatch(new Message(MessageType.Info, "Sucess",
                string.Format("Room {0} was sucessfully created", _RoomName)));
            LoadScene();
        }

        private void LoadScene()
        {
            if (!_RoomType.Equals(RoomType.Presentation) && !_RoomType.Equals(RoomType.CodeReview) &&
                !_RoomType.Equals(RoomType.Showroom))
            {
                ShowWindowMessageSignal.Dispatch(new Message(MessageType.Info, "Not implemenented yet",
                    "Scene coming soon (TM)"));
                return;
            }

            UnityUtil.LoadScene(_RoomType);
        }

        private bool RoomAlreadyExists()
        {
            //TODO depending on room type 

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
                Logger.Info("Error:" + errorMessage);
                ShowWindowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }
            if (!_RoomType.Equals(RoomType.Showroom) && _medium.IsEmpty())
            {
                string errorMessage = "PictureMedium wasn't choosen yet";
                Logger.Info("Error:" + errorMessage);
                ShowWindowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }

            if (RoomAlreadyExists())
            {
                string errorMessage = string.Format("Room with name {0} already exists", _RoomName);
                Logger.Info("Error:" + errorMessage);
                ShowWindowMessageSignal.Dispatch(new Message(MessageType.Error, "Error", errorMessage));
                return false;
            }
            return true;
        }
    }
}