namespace Assets.Assets.Scripts.Notifier
{
    public class Notification
    {       

        public Notification(string title, string description, NotificationType type)
        {
            this.Title = title;
            this.Description = description;
            this.Type = type;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public NotificationType Type { get; private set; }  
    }
}
