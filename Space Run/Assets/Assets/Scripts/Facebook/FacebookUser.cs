namespace Assets.Assets.Scripts.Facebook
{
    public class FacebookUser
    {
        public FacebookUser(string name, string id, int score)
        {
            this.Name = name;
            this.ID = id;
            this.Score = score;
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public int Score { get; set; }
    }
    
}
