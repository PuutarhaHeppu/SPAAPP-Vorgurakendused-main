namespace teamtrack_api.Model;

public record EventPeople {
    public int Id { get; set;}
    public int EventId { get; set;}
    public int PeopleId { get; set;}
}