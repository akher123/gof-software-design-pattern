// 1. Product Class
public class Vehicle
{
    public string Model { get; set; }
    public string Engine { get; set; }
    public string Transmission { get; set; }
    public string Body { get; set; }
    public List<string> Accessories { get; set; }

    public Vehicle()
    {
        Accessories = new List<string>();
    }

    public void ShowInfo()
    {
        Console.WriteLine("\n========== VEHICLE INFORMATION ==========");
        Console.WriteLine($"Model: {Model}");
        Console.WriteLine($"Engine: {Engine}");
        Console.WriteLine($"Transmission: {Transmission}");
        Console.WriteLine($"Body: {Body}");
        Console.WriteLine("Accessories:");

        if (Accessories.Count == 0)
        {
            Console.WriteLine("  No accessories");
        }
        else
        {
            foreach (var accessory in Accessories)
            {
                Console.WriteLine($"  • {accessory}");
            }
        }
        Console.WriteLine("========================================\n");
    }
}

// 2. Builder Interface
public interface IVehicleBuilder
{
    void SetModel();
    void SetEngine();
    void SetTransmission();
    void SetBody();
    void SetAccessories();
    Vehicle GetVehicle();
}

// 3. Concrete Builders
public class HeroBuilder : IVehicleBuilder
{
    private Vehicle objVehicle;

    public HeroBuilder()
    {
        objVehicle = new Vehicle();
    }

    public void SetModel()
    {
        objVehicle.Model = "Hero Splendor";
    }

    public void SetEngine()
    {
        objVehicle.Engine = "100cc";
    }

    public void SetTransmission()
    {
        objVehicle.Transmission = "4 Speed Manual";
    }

    public void SetBody()
    {
        objVehicle.Body = "Standard";
    }

    public void SetAccessories()
    {
        objVehicle.Accessories.Add("Kick Starter");
        objVehicle.Accessories.Add("Digital Speedometer");
        objVehicle.Accessories.Add("LED Tail Light");
    }

    public Vehicle GetVehicle()
    {
        return objVehicle;
    }
}

public class HondaBuilder : IVehicleBuilder
{
    private Vehicle objVehicle;

    public HondaBuilder()
    {
        objVehicle = new Vehicle();
    }

    public void SetModel()
    {
        objVehicle.Model = "Honda City";
    }

    public void SetEngine()
    {
        objVehicle.Engine = "1.5L i-VTEC";
    }

    public void SetTransmission()
    {
        objVehicle.Transmission = "CVT Automatic";
    }

    public void SetBody()
    {
        objVehicle.Body = "Sedan";
    }

    public void SetAccessories()
    {
        objVehicle.Accessories.Add("Sunroof");
        objVehicle.Accessories.Add("Touchscreen Infotainment");
        objVehicle.Accessories.Add("Rear Camera");
        objVehicle.Accessories.Add("Climate Control");
        objVehicle.Accessories.Add("Alloy Wheels");
    }

    public Vehicle GetVehicle()
    {
        return objVehicle;
    }
}

// 4. Director Class
public class VehicleCreator
{
    private IVehicleBuilder objBuilder;

    // Constructor injection
    public VehicleCreator(IVehicleBuilder builder)
    {
        objBuilder = builder;
    }

    // Method injection (alternative)
    public void SetBuilder(IVehicleBuilder builder)
    {
        objBuilder = builder;
    }

    public void CreateVehicle()
    {
        // Follow specific construction sequence
        objBuilder.SetModel();
        objBuilder.SetEngine();
        objBuilder.SetTransmission();
        objBuilder.SetBody();
        objBuilder.SetAccessories();
    }

    public Vehicle GetVehicle()
    {
        return objBuilder.GetVehicle();
    }
}

// 5. Client Code
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== BUILDER DESIGN PATTERN DEMO ===\n");

        // Create a motorcycle using HeroBuilder
        Console.WriteLine("Building Hero Motorcycle...");
        var heroBuilder = new HeroBuilder();
        var motorcycleCreator = new VehicleCreator(heroBuilder);

        motorcycleCreator.CreateVehicle();
        var motorcycle = motorcycleCreator.GetVehicle();
        motorcycle.ShowInfo();

        // Create a car using HondaBuilder
        Console.WriteLine("Building Honda Car...");
        var hondaBuilder = new HondaBuilder();
        var carCreator = new VehicleCreator(hondaBuilder);

        carCreator.CreateVehicle();
        var car = carCreator.GetVehicle();
        car.ShowInfo();

        // Using the same director with different builders
        Console.WriteLine("Building Another Hero with Modified Accessories...");
        var customHeroBuilder = new HeroBuilder();
        var customCreator = new VehicleCreator(customHeroBuilder);

        // Get vehicle before customization
        var basicMotorcycle = customHeroBuilder.GetVehicle();
        Console.WriteLine($"Before building: Model = {basicMotorcycle.Model ?? "Not Set"}");

        // Build the vehicle
        customCreator.CreateVehicle();
        var customMotorcycle = customCreator.GetVehicle();

        // Add extra accessories after initial build
        customMotorcycle.Accessories.Add("Custom Seat Cover");
        customMotorcycle.Accessories.Add("Mobile Charger");
        customMotorcycle.ShowInfo();

        // Demonstrate step-by-step building
        Console.WriteLine("=== STEP-BY-STEP BUILDING ===");
        var stepBuilder = new HeroBuilder();

        Console.WriteLine("1. Setting Model...");
        stepBuilder.SetModel();

        Console.WriteLine("2. Setting Engine...");
        stepBuilder.SetEngine();

        Console.WriteLine("3. Setting Transmission...");
        stepBuilder.SetTransmission();

        Console.WriteLine("4. Setting Body...");
        stepBuilder.SetBody();

        Console.WriteLine("5. Setting Accessories...");
        stepBuilder.SetAccessories();

        var stepVehicle = stepBuilder.GetVehicle();
        stepVehicle.ShowInfo();

        // Test with a new builder type
        Console.WriteLine("=== CREATING CUSTOM VEHICLE ===");
        var customBuilder = new CustomLuxuryBuilder();
        var luxuryCreator = new VehicleCreator(customBuilder);
        luxuryCreator.CreateVehicle();
        var luxuryCar = luxuryCreator.GetVehicle();
        luxuryCar.ShowInfo();
    }
}

// 6. Additional Custom Builder (Optional Extension)
public class CustomLuxuryBuilder : IVehicleBuilder
{
    private Vehicle objVehicle;

    public CustomLuxuryBuilder()
    {
        objVehicle = new Vehicle();
    }

    public void SetModel()
    {
        objVehicle.Model = "Luxury Premium";
    }

    public void SetEngine()
    {
        objVehicle.Engine = "V8 Twin-Turbo";
    }

    public void SetTransmission()
    {
        objVehicle.Transmission = "8-Speed Automatic";
    }

    public void SetBody()
    {
        objVehicle.Body = "Luxury Coupe";
    }

    public void SetAccessories()
    {
        objVehicle.Accessories.Add("Massage Seats");
        objVehicle.Accessories.Add("Premium Sound System");
        objVehicle.Accessories.Add("Heads-Up Display");
        objVehicle.Accessories.Add("Night Vision");
        objVehicle.Accessories.Add("Automatic Parking");
    }

    public Vehicle GetVehicle()
    {
        return objVehicle;
    }
}