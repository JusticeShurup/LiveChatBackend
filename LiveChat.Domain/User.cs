namespace LiveChat.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Message>? Messages { get; set; }

        public User() { }

        public User(Guid id, string firstName, string lastName, ICollection<Message>? messages)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CreatedDate = DateTime.UtcNow;
            Messages = messages;
        }
    }
}