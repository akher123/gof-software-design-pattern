using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype;

// 1. Prototype Interface
public interface IVehiclePrototype
{
    string Model { get; set; }
    string Color { get; set; }
    int Year { get; set; }

    IVehiclePrototype Clone();
    void Drive();
}
