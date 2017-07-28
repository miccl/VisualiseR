using strange.extensions.signal.impl;
using VisualiseR.Common;

namespace VisualiseR.Main
{
    /// <summary>
    /// Signal to instantiate the <see cref="CreateRoomCommand"/>
    /// </summary>
    public class CreateRoomSignal : Signal<string, RoomType, PictureMedium>
    {
    }
}