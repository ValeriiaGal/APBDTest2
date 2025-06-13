namespace Models;

public class CarManufacturer
{
    public int Id {get;set;}
    public string Name {get;set;}

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}