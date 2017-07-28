using System;

namespace VisualiseR.Main
{
    /// <summary>
    /// Represents a message in the scenes.
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// Type of the message.
        /// </summary>
        public MessageType Type { get; set; }
        /// <summary>
        /// Title of the message.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Text of the message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Ctor for <see cref="Message"/>.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <exception cref="ArgumentNullException"></exception>
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