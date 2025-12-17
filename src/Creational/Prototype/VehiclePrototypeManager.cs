using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype;
// 3. Client Code - Vehicle Prototype Manager (similar to Factory)
public class VehiclePrototypeManager
{
    private Dictionary<string, IVehiclePrototype> _prototypes = new Dictionary<string, IVehiclePrototype>();

    public void RegisterPrototype(string key, IVehiclePrototype prototype)
    {
        _prototypes[key] = prototype;
    }

    public IVehiclePrototype CreateVehicle(string key)
    {
        if (_prototypes.ContainsKey(key))
        {
            return _prototypes[key].Clone();
        }
        throw new ArgumentException($"No prototype registered for key: {key}");
    }
}

