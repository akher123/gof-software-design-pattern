using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype;

// 2. Concrete Prototypes
public class Scooter : IVehiclePrototype
{
    public string Model { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public bool HasStorageBox { get; set; }

    public Scooter(string model, string color, int year, bool hasStorageBox)
    {
        Model = model;
        Color = color;
        Year = year;
        HasStorageBox = hasStorageBox;
    }

    public IVehiclePrototype Clone()
    {
        // Create a shallow copy (for simple properties)
        // For deep copy, you might need to handle reference types differently
        return (IVehiclePrototype)this.MemberwiseClone();
    }

    public void Drive()
    {
        Console.WriteLine($"Driving a {Color} {Year} {Model} scooter. Storage box: {HasStorageBox}");
    }

    public override string ToString()
    {
        return $"Scooter: {Model}, Color: {Color}, Year: {Year}, HasStorageBox: {HasStorageBox}";
    }
}
