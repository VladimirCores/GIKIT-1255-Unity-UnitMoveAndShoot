using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class StringUtils
{
  public static void Test_GenerateFullName() {
    while(GenerateFullName(10).Length != 10) {
      var generatedName = GenerateFullName(10);
      if (!generatedName.Contains(" ")) throw new ArgumentException("Generated name have no space");
      if (generatedName[generatedName.Length - 1] == ' ') throw new ArgumentException("Generated name have space as last char");
    }
    Debug.Log("> Test_GenerateFullName -> Complete");
  }

  private static string SplitStringAtPositionAndUpperCaseFirstLetterInParts(string input, float position)
  {
    Func<string, string> makeFirstLetterUpperCase = (string str) =>
        Char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);

    int centerLeft = (int)Math.Floor(position);
    string firstPart = input.Substring(0, centerLeft);
    bool isFirstCharInUpperCase = Char.IsUpper(firstPart[0]);
    if (!isFirstCharInUpperCase)
    {
      firstPart = makeFirstLetterUpperCase(firstPart);
    }

    // Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> firstPart: {firstPart}");

    string secondPart = input.Substring(centerLeft + 1, input.Length - 1 - centerLeft);
    secondPart = makeFirstLetterUpperCase(secondPart);

    // Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> secondPart: {secondPart}");
    return firstPart + ' ' + secondPart;
  }

  public static string GenerateFullName(int length = 10)
  {
    System.Random random = new System.Random();
    string str = "abcdefghijklmnopqrstuvwxyz ";
    string result = "";
    bool isSpaceAdded = false;
    int lastIndex = length - 1;
    for (int i = 0; i < length; i++)
    {
      char randomChar = str[random.Next(str.Length)];
      bool isCharSpace = randomChar == ' ';
      bool isCharFirst = i == 0;
      bool isCharLast = i == lastIndex;

      bool shouldStepBack = isCharSpace && (isSpaceAdded || isCharFirst || isCharLast);
      if (shouldStepBack && i-- >= 0)
      {
        // Debug.Log($"> GenerateFullName -> stepBack = {i}");
        continue;
      }
      if (isCharSpace) isSpaceAdded = true;
      if (isCharFirst) randomChar = Char.ToUpper(randomChar);

      result += randomChar;
      // Debug.Log($"> GenerateFullName -> {i}|{randomChar}");
    }

    bool hasSpace = result.Contains(' ');
    // Debug.Log($"> GenerateFullName -> {result} -> hasSpace: {hasSpace}");

    float splitPosition = hasSpace
      ? (float)result.IndexOf(' ')
      : (float)(result.Length / 2);

    if (splitPosition > 0)
    {
      return SplitStringAtPositionAndUpperCaseFirstLetterInParts(result, splitPosition);
    }
    return result;
  }
}