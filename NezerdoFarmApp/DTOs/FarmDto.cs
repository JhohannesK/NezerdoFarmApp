namespace NezerdoFarmApp.DTOs;

public class FarmDto
{
    
}

public class CreateFarmDto
{
    public required string FarmName { get; set; }
    public string? FarmLocation { get; set; }
    public required string City { get; set; }
    public string? FarmSize { get; set; }
}