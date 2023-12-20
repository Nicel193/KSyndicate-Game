using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 position) =>
            new Vector3Data(position.x, position.y, position.z);
        
        public static Vector3 AsUnityVector(this Vector3Data position) =>
            new Vector3(position.x, position.y, position.z);
    }
}