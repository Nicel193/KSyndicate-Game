using System;
using Newtonsoft.Json;

namespace CodeBase.Data
{
  [Serializable]
  public class PositionOnLevel
  {
    public string Level;
    public Vector3Data Position;

    public PositionOnLevel()
    {
      
    }

    public PositionOnLevel(string level, Vector3Data position)
    {
      Level = level;
      Position = position;
    }

    public PositionOnLevel(string initialLevel)
    {
      Level = initialLevel;
    }
  }
}