using CSharpLib.Utils;
using System.Collections.Generic;
using System.Linq;

namespace _2021_CS.Day19
{
    internal class Scanner
    {
        public int Id { get; }
        public IEnumerable<int> Distances { get; }
        public IEnumerable<(int X, int Y, int Z)>? AlignedBeaconSet { get; set; }
        public List<List<(int X, int Y, int Z)>> AllBeaconSets { get; }
        public (int X, int Y, int Z) RelativePosition { get; private set; } = (0, 0, 0);

        public Scanner(int id, IEnumerable<(int X, int Y, int Z)> beacons)
        {
            Id = id;
            var beaconList = beacons.ToList();
            AllBeaconSets = GeometryUtils.OrientationFunctions().Select(fn => beaconList.Select(b => fn(b.X, b.Y, b.Z)).ToList()).ToList();

            var distanceList = new List<int>();
            for (int i = 0; i < beaconList.Count; i++)
            {
                var b1 = beaconList[i];
                for (int j = i + 1; j < beaconList.Count; j++)
                {
                    var b2 = beaconList[j];
                    distanceList.Add(GeometryUtils.ManhattanDistance(b1, b2));
                }
            }
            Distances = distanceList;
        }

        public bool TryToAlignWith(Scanner other)
        {
            // The other set must be aligned already, this one must not be
            if (other.AlignedBeaconSet == null || AlignedBeaconSet != null)
            {
                return false;
            }

            // For performance reasons, we only have to check if there is a chance of a match
            // if the two sets have at least 66 distances in common. 
            if (new HashSet<int>(Distances).Intersect(other.Distances).Count() < 66)
            {
                return false;
            }

            foreach(var beaconSet in AllBeaconSets)
            {
                foreach(var otherBeacon in other.AlignedBeaconSet)
                {
                    foreach (var beacon in beaconSet)
                    {
                        var dX = beacon.X - otherBeacon.X;
                        var dY = beacon.Y - otherBeacon.Y;
                        var dz = beacon.Z - otherBeacon.Z;

                        // Make a set of all beacons in this beacon set translated so that beacon == otherBeacon.
                        var set = new HashSet<(int, int, int)>(beaconSet.Select(b => (b.X - dX, b.Y - dY, b.Z - dz)));

                        // If this beacon set has at least 12 positions in common with the other scanner's
                        // already aligned beacon set then this is the set that should be used.
                        if (set.Intersect(other.AlignedBeaconSet).Count() > 11)
                        {
                            AlignedBeaconSet = set;
                            RelativePosition = (-dX, -dY, -dz);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
