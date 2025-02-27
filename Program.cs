
using MySql.Data.MySqlClient;

class Program
{
    private static readonly string ConnectionString = "Server=localhost;Database=MP06_UF02_AEA1;User ID=root;Password=root;";

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nMenú del CRUD");
            Console.WriteLine("1. Inserir vehicle");
            Console.WriteLine("2. Llegir vehicles");
            Console.WriteLine("3. Actualitzar vehicle");
            Console.WriteLine("4. Eliminar vehicle");
            Console.WriteLine("5. Sortir");
            Console.Write("Tria una opció: ");

            int opcio = int.Parse(Console.ReadLine());

            try
            {
                switch (opcio)
                {
                    case 1:
                        InserirVehicle();
                        break;
                    case 2:
                        LlegirVehicles();
                        break;
                    case 3:
                        ActualitzarVehicle();
                        break;
                    case 4:
                        EliminarVehicle();
                        break;
                    case 5:
                        Console.WriteLine("Sortint del programa...");
                        return;
                    default:
                        Console.WriteLine("Opció no vàlida. Intenta-ho de nou.");
                        break;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("⚠️ Error SQL: " + e.Message);
            }
        }
    }

    private static MySqlConnection GetConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public static void InserirVehicle()
    {
        // Capturamos la información para el vehículo
        Console.Write("Marca: ");
        string marca = Console.ReadLine();
        Console.Write("Model: ");
        string model = Console.ReadLine();
        Console.Write("Capacitat del maleter: ");
        int capacitatMaleter = int.Parse(Console.ReadLine());

        // Crear instancia del objeto Vehicle con los datos proporcionados
        Vehicle vehicle = new Vehicle(marca, model, capacitatMaleter);

        // Ahora usamos las propiedades del objeto para pasarlas a la consulta
        string sql = "INSERT INTO Vehicles (Marca, Model, CapacitatMaleter) VALUES (@Marca, @Model, @CapacitatMaleter)";

        using (var connection = GetConnection())
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            {
                // Utilizamos los getters (en este caso, las propiedades) del objeto vehicle
                cmd.Parameters.AddWithValue("@Marca", vehicle.Marca);
                cmd.Parameters.AddWithValue("@Model", vehicle.Model);
                cmd.Parameters.AddWithValue("@CapacitatMaleter", vehicle.CapacitatMaleter);
                cmd.ExecuteNonQuery();
            }
            Console.WriteLine("✅ Vehicle inserit correctament.");
        }
    }


    public static List<Vehicle> LlegirVehicles()
    {
        string sql = "SELECT * FROM Vehicles";
        List<Vehicle> vehicles = new List<Vehicle>();

        using (var connection = GetConnection())
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var vehicle = new Vehicle(
                        reader.GetInt32("Id"),
                        reader.GetString("Marca"),
                        reader.GetString("Model"),
                        reader.GetInt32("CapacitatMaleter")
                    );
                    vehicles.Add(vehicle);
                    Console.WriteLine(vehicle);
                }
            }
            Console.WriteLine("Lectura de vehicles finalitzada.");
        }

        return vehicles;
    }


    public static void ActualitzarVehicle()
    {
        Console.Write("ID del vehicle a actualitzar: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Nova marca: ");
        string marca = Console.ReadLine();
        Console.Write("Nou model: ");
        string model = Console.ReadLine();
        Console.Write("Nova capacitat del maleter: ");
        int capacitatMaleter = int.Parse(Console.ReadLine());

        string sql = "UPDATE Vehicles SET Marca = @Marca, Model = @Model, CapacitatMaleter = @CapacitatMaleter WHERE Id = @Id";

        using (var connection = GetConnection())
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Marca", marca);
                cmd.Parameters.AddWithValue("@Model", model);
                cmd.Parameters.AddWithValue("@CapacitatMaleter", capacitatMaleter);
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Vehicle actualitzat." : "No s'ha trobat el vehicle.");
            }
        }
    }

    public static void EliminarVehicle()
    {
        Console.Write("ID del vehicle a eliminar: ");
        int id = int.Parse(Console.ReadLine());

        string sql = "DELETE FROM Vehicles WHERE Id = @Id";

        using (var connection = GetConnection())
        {
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? "Vehicle eliminat." : "No s'ha trobat el vehicle.");
            }
        }
    }
}

