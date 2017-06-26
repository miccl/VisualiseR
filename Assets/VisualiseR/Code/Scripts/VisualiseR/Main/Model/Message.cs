using System;

namespace VisualiseR.Main
{
    /// <summary>
    /// Class for messages to show in the scenes.
    /// </summary>
    public class Message : IMessage
    {
        public MessageType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Message(MessageType type, string title, string text)
        {
            if (title == null) throw new ArgumentNullException("title");
            if (text == null) throw new ArgumentNullException("text");
            Type = type;
            Title = title;
            Text = text;
        }
    }
}