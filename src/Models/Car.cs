namespace Models;

public class Car
{
    public int Id {get;set;}
    public string ModelName {get;set;}
    public int Number {get;set;}
    
    public int CarManufactureId {get;set;}
    public CarManufacturer CarManufacture {get;set;}

    public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}