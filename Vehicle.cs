public class Vehicle
{
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Model { get; set; }
    public int CapacitatMaleter { get; set; }

    public Vehicle(int id, string marca, string model, int capacitatMaleter)
    {
        Id = id;
        Marca = marca;
        Model = model;
        CapacitatMaleter = capacitatMaleter;
    }

    public Vehicle(string marca, string model, int capacitatMaleter)
    {
        Marca = marca;
        Model = model;
        CapacitatMaleter = capacitatMaleter;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Marca: {Marca}, Model: {Model}, Maleter: {CapacitatMaleter}";
    }
}
