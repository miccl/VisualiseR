﻿using System;
using strange.extensions.command.impl;
using VisualiseR.Common;
using VisualiseR.Util;

namespace VisualiseR.Main
{
    public class CreateRoomCommand : Command
    {
        private static readonly JCsLogger Logger = new JCsLogger(typeof(CreateRoomCommand));


        [Inject]
        public string _roomName { get; set; }

        [Inject]
        public RoomType _roomType { get; set; }

        [Inject]
        public PictureMedium _medium { get; set; }

        [Inject]
        public IRoom room { get; set; }

        [Inject]
        public IPlayer player { get; set; }

        [Inject]
        public MessageSignal MessageSignal { get; set; }

        public override void Execute()
        {
            Logger.DebugFormat("roomName: {0}", _roomName);
            Logger.DebugFormat("roomType: {0}", _roomType);
            Logger.DebugFormat("pictureMedium: {0}", _medium);
            if (IsInputValid())
            {
                ConstructRoom();
                //TODO Photon Raum erstellen...
                //TODO Entsprechende Szene laden...
                //TODO
                Logger.InfoFormat("Room {0} was created", room);

                PlayerPrefsUtil.saveObject(PlayerPrefsUtil.ROOM_KEY, room);
                UnityUtil.LoadScene(_roomType.ToString());
//                SceneManager.LoadScene(1);
            }
        }

        private void ConstructRoom()
        {
            room.Name = _roomName;
            room.Type = _roomType;
            room.Medium = _medium;
            AddPlayer();
            //TODO wie kreiere ich die Spieler ?!?!?!
        }

        private void AddPlayer()
        {
            var playerName = PlayerPrefsUtil.RetrieveValue(PlayerPrefsUtil.PLAYER_NAME_KEY);
            player.Name = playerName;
            player.Type = PlayerType.Host;
            room.AddPlayer(player);
        }

        /// <summary>
        /// Test, if all needed information are choosen.
        /// If not, a error message is displayed with
        /// </summary>
        private bool IsInputValid()
        {
//TODO Die Validerung sollte wohl woanders gemacht werden, wahrscheinlich im Model?! zumindestens die formale Validierung?
//TODO Fehlernachricht anzeigen
//TODO vielleicht andere Validierungen hinzufügen (keine Sonderzeichen...)
//TODO Photon ist der Raumname schon vergeben???
            if (String.IsNullOrEmpty(_roomName))
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
            return true;
        }
    }
}