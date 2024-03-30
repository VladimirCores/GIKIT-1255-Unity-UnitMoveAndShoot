using System;
using UnityEngine;

public class StringUtils2
{
  private static string SplitStringAtPositionAndUpperCaseFirstLetterInParts(string input, float position) 
  {
      Func<string, string> makeFirstLetterUpperCase = (string str) =>  
          Char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);

      int centerLeft = (int)Math.Floor(position);
      int centerRight = (int)Math.Ceiling(position);

      string firstPart = input.Substring(0, centerLeft);

      bool isFirstCharInUpperCase = Char.IsUpper(firstPart[0]);

      if (!isFirstCharInUpperCase) {
          firstPart = makeFirstLetterUpperCase(firstPart);
      }

      Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> firstName: {firstPart}");
      
      string secondPart = input.Substring(centerLeft, centerRight);
      secondPart = makeFirstLetterUpperCase(secondPart);
      
      Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> lastName: {secondPart}");
      return firstPart + ' ' + secondPart;
  }

  public static string GenerateHeroFullName(int length = 10)
  {
      System.Random res = new System.Random(); 
      string str = "abcdefghijklmnopqrstuvwxyz ";
      string result = ""; 
      bool isSpaceAdded = false;
      for (int i = 0; i < length; i++) 
      {
          char randomChar = str[res.Next(str.Length)];
          bool isCharSpace = randomChar == ' ';
          bool isFirstChar = i == 0;
          
          bool shouldStepBack = isCharSpace && (isSpaceAdded || isFirstChar);
          if (shouldStepBack && i-- >= 0) {
            Debug.Log($"> GenerateHeroFullName -> stepBack = {i}");
              continue;
          }
          if (isCharSpace) isSpaceAdded = true;
          if (isFirstChar) randomChar = Char.ToUpper(randomChar);
          
          result += randomChar;
          Debug.Log($"> GenerateHeroFullName -> {i}|{randomChar} - {result}");
      } 

      bool isSpaceLastChar = result[result.Length - 1] == ' ';
      if (isSpaceLastChar) result = result.Substring(0, result.Length - 1) + 'a';

      Debug.Log($"> GenerateHeroFullName -> isSpaceLastChar: {isSpaceLastChar}");
      
      bool hasSpace = result.Contains(' ');
      float splitPosition = hasSpace 
        ? (float)result.IndexOf(' ')
        : (float)(result.Length / 2);

      if (splitPosition > 0) {
        return SplitStringAtPositionAndUpperCaseFirstLetterInParts(result, splitPosition);
      }
      return result;
  }
}