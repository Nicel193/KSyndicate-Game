using System;
using System.ComponentModel;
using UnityEngine;
using Newtonsoft.Json;

namespace CodeBase.Data
{
  public static class DataExtensions
  {
    public static Vector3Data AsVectorData(this Vector3 vector) => 
      new Vector3Data(vector.x, vector.y, vector.z);
    
    public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
      new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }

    public static float SqrMagnitudeTo(this Vector3 from, Vector3 to)
    {
      return Vector3.SqrMagnitude(to - from);
    }

    public static string ToJson(this object obj) => 
      JsonConvert.SerializeObject(obj);

    public static T ToDeserialized<T>(this string json) =>
      JsonConvert.DeserializeObject<T>(json);
    
    public static string GetDescription(this Enum value)
    {
      var field = value.GetType().GetField(value.ToString());
      var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
      return attribute == null ? value.ToString() : attribute.Description;
    }
  }
}