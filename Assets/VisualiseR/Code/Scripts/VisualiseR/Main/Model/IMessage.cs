namespace VisualiseR.Main
{
    /// <summary>
    /// Represents a message in the scenes.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Type of the message.
        /// </summary>
        MessageType Type { get; set; }
        /// <summary>
        /// Title of the message.
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// Text of the message.
        /// </summary>
        string Text { get; set; }
    }
}