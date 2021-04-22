using System;
namespace AssistantBarb
{
    public class Location
    {
        public string Name { get; private set; }
        public Location(string name)
        {
            Name = name;
        }
    }
}
