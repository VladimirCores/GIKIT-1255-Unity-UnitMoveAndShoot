using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class MathUtils
{
  public static float GetRandomReflectedInRange(float range) {
    System.Random random = new System.Random();
    float delta = 2 * range;
    return -range + delta * (float)random.NextDouble();
  }
}