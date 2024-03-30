using System;
using UnityEngine;

public class StringUtils
{
  private static string SplitStringAtPositionAndUpperCaseFirstLetterInParts(string input, float position) 
  {
      Func<string, string> makeFirstLetterUpperCase = (string str) =>  
          Char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);

      int centerLeft = (int)Math.Floor(position);
      string firstPart = input.Substring(0, centerLeft);
      bool isFirstCharInUpperCase = Char.IsUpper(firstPart[0]);
      if (!isFirstCharInUpperCase) {
          firstPart = makeFirstLetterUpperCase(firstPart);
      }

      Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> firstPart: {firstPart}");
      
      string secondPart = input.Substring(centerLeft + 1, input.Length - 1 - centerLeft);
      secondPart = makeFirstLetterUpperCase(secondPart);
      
      Debug.Log($"> SplitStringAtPositionAndUpperCaseFirstLetterInParts -> secondPart: {secondPart}");
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
          Debug.Log($"> GenerateHeroFullName -> {i}|{randomChar}");
      } 

      bool hasSpace = result.Contains(' ');
      bool isSpaceLastChar = result[result.Length - 1] == ' ';

      Debug.Log($"> GenerateHeroFullName -> {result} -> hasSpace | isSpaceLastChar: {hasSpace} | {isSpaceLastChar}");
      
      float splitPosition = -1;

      if (!hasSpace || isSpaceLastChar) {
          if (isSpaceLastChar) result = result.Substring(0, result.Length - 1) + 'a';
          splitPosition = (float)(result.Length / 2);
      }
      else if (hasSpace) {
          splitPosition = (float)result.IndexOf(' ');
      }

      bool shouldSplitModifyAtPosition = splitPosition > 0;
      if (shouldSplitModifyAtPosition) {
        return SplitStringAtPositionAndUpperCaseFirstLetterInParts(result, splitPosition);
      }
      return result;
  }
}