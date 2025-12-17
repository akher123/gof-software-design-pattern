using System;

namespace FactoryMethod;

// 1. Product Interface
public interface IFactory
{
    void Drive();
}

// 2. Concrete Products
public class Scooter : IFactory
{
    public void Drive()
    {
        Console.WriteLine("Scooter is being driven.");
    }
}

public class Bike : IFactory
{
    public void Drive()
    {
        Console.WriteLine("Bike is being ridden.");
    }
}

// 3. Abstract Creator
public abstract class VehicleFactory
{
    // The Factory Method
    public abstract IFactory GetVehicle();

    // Optional: A common operation that uses the factory method
    public void UseVehicle()
    {
        var vehicle = GetVehicle();
        Console.Write("Using vehicle: ");
        vehicle.Drive();
    }
}

// 4. Concrete Creators
public class ScooterFactory : VehicleFactory
{
    public override IFactory GetVehicle()
    {
        return new Scooter();
    }
}

public class BikeFactory : VehicleFactory
{
    public override IFactory GetVehicle()
    {
        return new Bike();
    }
}

// 5. ConcreteVehicleFactory (As shown in your diagram)
public class ConcreteVehicleFactory : VehicleFactory
{
    private readonly string _vehicleType;

    public ConcreteVehicleFactory(string vehicleType)
    {
        _vehicleType = vehicleType;
    }

    public override IFactory GetVehicle()
    {
        return _vehicleType.ToLower() switch
        {
            "scooter" => new Scooter(),
            "bike" => new Bike(),
            _ => throw new ArgumentException($"Invalid vehicle type: {_vehicleType}")
        };
    }
}

// 6. Client Code
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== FACTORY METHOD PATTERN DEMO ===\n");

        // Method 1: Using specific factories
        Console.WriteLine("Method 1: Using Specific Factories");
        Console.WriteLine("-----------------------------------");

        VehicleFactory scooterFactory = new ScooterFactory();
        var scooter = scooterFactory.GetVehicle();
        scooter.Drive();

        VehicleFactory bikeFactory = new BikeFactory();
        var bike = bikeFactory.GetVehicle();
        bike.Drive();

        // Using the common operation
        Console.WriteLine("\nUsing UseVehicle method:");
        scooterFactory.UseVehicle();
        bikeFactory.UseVehicle();

        // Method 2: Using ConcreteVehicleFactory (as shown in diagram)
        Console.WriteLine("\n\nMethod 2: Using ConcreteVehicleFactory");
        Console.WriteLine("--------------------------------------");

        VehicleFactory factory1 = new ConcreteVehicleFactory("scooter");
        var vehicle1 = factory1.GetVehicle();
        vehicle1.Drive();

        VehicleFactory factory2 = new ConcreteVehicleFactory("bike");
        var vehicle2 = factory2.GetVehicle();
        vehicle2.Drive();

        // Method 3: Dynamic vehicle creation
        Console.WriteLine("\n\nMethod 3: Dynamic Vehicle Creation");
        Console.WriteLine("----------------------------------");

        CreateAndDriveVehicle("scooter");
        CreateAndDriveVehicle("bike");

        // Method 4: Factory with additional logic
        Console.WriteLine("\n\nMethod 4: Advanced Vehicle Factory");
        Console.WriteLine("----------------------------------");

        var advancedFactory = new AdvancedVehicleFactory();

        var commuterScooter = advancedFactory.GetVehicle(VehicleType.Scooter, "Commuter");
        commuterScooter.Drive();

        var sportsBike = advancedFactory.GetVehicle(VehicleType.Bike, "Sports");
        sportsBike.Drive();

        var luxuryScooter = advancedFactory.GetVehicle(VehicleType.Scooter, "Luxury");
        luxuryScooter.Drive();

        // Error handling example
        try
        {
            VehicleFactory errorFactory = new ConcreteVehicleFactory("car");
            var errorVehicle = errorFactory.GetVehicle();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
    }

    static void CreateAndDriveVehicle(string vehicleType)
    {
        VehicleFactory factory = new ConcreteVehicleFactory(vehicleType);
        var vehicle = factory.GetVehicle();
        Console.WriteLine($"Created a {vehicleType.ToUpper()}:");
        vehicle.Drive();
    }
}

// 7. Enhanced Example with Additional Features
public enum VehicleType
{
    Scooter,
    Bike
}

// Enhanced product classes with more functionality
public class EnhancedScooter : IFactory
{
    private readonly string _model;

    public EnhancedScooter(string model = "Standard")
    {
        _model = model;
    }

    public void Drive()
    {
        Console.WriteLine($"Driving {_model} Scooter at 40 km/h");
    }
}

public class EnhancedBike : IFactory
{
    private readonly string _model;

    public EnhancedBike(string model = "Standard")
    {
        _model = model;
    }

    public void Drive()
    {
        Console.WriteLine($"Riding {_model} Bike at 25 km/h");
    }
}

// Enhanced factory with parameterized creation
public class AdvancedVehicleFactory : VehicleFactory
{
    public IFactory GetVehicle(VehicleType type, string model)
    {
        return type switch
        {
            VehicleType.Scooter => new EnhancedScooter(model),
            VehicleType.Bike => new EnhancedBike(model),
            _ => throw new ArgumentException($"Invalid vehicle type: {type}")
        };
    }

    // Required to implement abstract method
    public override IFactory GetVehicle()
    {
        return new EnhancedScooter(); // Default vehicle
    }
}