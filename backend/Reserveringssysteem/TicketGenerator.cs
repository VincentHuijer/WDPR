class TicketGenerator
{
    public static string GerateTicket()
    {
        return Guid.NewGuid().ToString();
    }
}