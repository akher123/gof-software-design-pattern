using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype;

public class Bike : IVehiclePrototype
{
    public string Model { get; set; }
    public string Color { get; set; }
    public int Year { get; set; }
    public int GearCount { get; set; }

    public Bike(string model, string color, int year, int gearCount)
    {
        Model = model;
        Color = color;
        Year = year;
        GearCount = gearCount;
    }

    public IVehiclePrototype Clone()
    {
        // Using MemberwiseClone for shallow copy
        return (IVehiclePrototype)this.MemberwiseClone();
    }

    public void Drive()
    {
        Console.WriteLine($"Riding a {Color} {Year} {Model} bike with {GearCount} gears.");
    }

    public override string ToString()
    {
        return $"Bike: {Model}, Color: {Color}, Year: {Year}, Gears: {GearCount}";
    }
}