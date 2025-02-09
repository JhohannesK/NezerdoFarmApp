namespace NezerdoFarmApp.DTOs;

public class FarmDto
{
    public required Guid FarmId { get; set; }
    public required string FarmName { get; set; }
    public required string FarmLocation { get; set; }
    public required string City { get; set; }
    public string? FarmSize { get; set; }
}

public class CreateFarmDto
{
    public required string FarmName { get; set; }
    public required string FarmLocation { get; set; }
    public required string City { get; set; }
    public string? FarmSize { get; set; }
}