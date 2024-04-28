namespace wpf_app.Models;

public class Address : TableBase
{
    public int AbonentId { get; set; }
    
    public int StreetId { get; set; }
    
    public string HouseNumber { get; set; }
    
    public string? Description { get; set; }
}