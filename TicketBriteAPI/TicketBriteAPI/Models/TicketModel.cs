public class TicketModel(){
    public Guid ticketID { get; set; }
    public Guid eventID { get; set; }
    public string ticketName { get; set; }
    public decimal ticketPrice { get; set; }
    public int ticketMaxAvailbale { get; set; }
    public int ticketsRemaining { get; set; }
    public Boolean ticketStatus { get; set; }
}