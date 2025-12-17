// Create prototype manager
using Prototype;

var prototypeManager = new VehiclePrototypeManager();

// Create and register initial prototypes
var standardScooter = new Scooter("Vespa", "Red", 2023, true);
var mountainBike = new Bike("Trek", "Black", 2024, 21);

prototypeManager.RegisterPrototype("StandardScooter", standardScooter);
prototypeManager.RegisterPrototype("MountainBike", mountainBike);

// Clone vehicles from prototypes
Console.WriteLine("=== Creating vehicles using Prototype Pattern ===\n");

// Create new scooters by cloning
var scooter1 = prototypeManager.CreateVehicle("StandardScooter") as Scooter;
scooter1.Color = "Blue"; // Customize the clone
scooter1.Drive();

var scooter2 = prototypeManager.CreateVehicle("StandardScooter") as Scooter;
scooter2.HasStorageBox = false; // Customize differently
scooter2.Drive();

// Create new bikes by cloning
var bike1 = prototypeManager.CreateVehicle("MountainBike") as Bike;
bike1.Color = "Green"; // Customize
bike1.Drive();

var bike2 = prototypeManager.CreateVehicle("MountainBike") as Bike;
bike2.GearCount = 24; // Customize
bike2.Drive();

// Verify they are different instances
Console.WriteLine("\n=== Instance Verification ===");
Console.WriteLine($"Original Scooter: {standardScooter}");
Console.WriteLine($"Cloned Scooter 1: {scooter1}");
Console.WriteLine($"Cloned Scooter 2: {scooter2}");

Console.WriteLine($"\nReference equal? {ReferenceEquals(standardScooter, scooter1)}");
Console.WriteLine($"Different colors? {standardScooter.Color != scooter1.Color}");

// Create a completely new prototype at runtime
Console.WriteLine("\n=== Adding New Prototype at Runtime ===");
var electricScooter = new Scooter("Niu", "White", 2024, false);
prototypeManager.RegisterPrototype("ElectricScooter", electricScooter);

var electricScooterClone = prototypeManager.CreateVehicle("ElectricScooter") as Scooter;
electricScooterClone.Drive();