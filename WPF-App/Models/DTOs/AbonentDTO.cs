namespace wpf_app.Models.DTOs;

public class AbonentDto
{
    public AbonentDto(
        string fullName,
        string street,
        string houseNumber,
        string housePhoneNumber,
        string workPhoneNumber,
        string personalPhoneNumber
    )
    {
        FullName = fullName;
        Street = street;
        HouseNumber = houseNumber;
        HousePhoneNumber = housePhoneNumber;
        WorkPhoneNumber = workPhoneNumber;
        PersonalPhoneNumber = personalPhoneNumber;
    }
    
    public AbonentDto() {}
    
    public string FullName { get; set; }
    
    public string Street { get; set; }

    public string HouseNumber { get; set; } 

    public string HousePhoneNumber { get; set; }

    public string WorkPhoneNumber { get; set; }

    public string PersonalPhoneNumber { get; set; }
}