using UnityEngine;

namespace AStarTools
{
    public class Node
    {
        public Node prev;
        public float staticWeight, travelWeight;
        public Vector3Int coordinates;

        public Node(Vector3Int coords)
        {
            coordinates = coords;
            staticWeight = 0;
            travelWeight = 1;
        }

        public float weight
        {
            get
            {
                return staticWeight + travelWeight;
            }
        }

        public bool Equals(Node other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return coordinates.Equals(other.coordinates);
        }

        public static bool operator ==(Node obj1, Node obj2)
        {
            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            if (ReferenceEquals(obj1, null))
            {
                return false;
            }
            if (ReferenceEquals(obj2, null))
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Node obj1, Node obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = coordinates.GetHashCode();
                return hashCode;
            }
        }

    }
}