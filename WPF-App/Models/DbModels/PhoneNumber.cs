namespace wpf_app.Models;

public class PhoneNumber : TableBase
{
    public int AbonentId { get; set; }
    
    public string Number { get; set; }
    
    public int TypeId { get; set; }
}