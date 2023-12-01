// See https://aka.ms/new-console-template for more information
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("Hello, World!");

string filePath = "C:\\Users\\steven.barker\\source\\repos\\AdventofCode2\\AdventofCode2\\AdventDay1.txt";

// Check if the file exists
if (File.Exists(filePath))
{
    // Open the file with a StreamReader
    using (StreamReader reader = new StreamReader(filePath))
    {
        Helper.setUpNumberWordDictionary();
        string line;
        Dictionary<int, List<int>> dctNumbersInLine = new Dictionary<int, List<int>>();

        int iIndex = 0;
        // Read and display lines from the file until the end is reached

        // Test Cases
        Dictionary<string, int> dctTestValues = new Dictionary<string, int>();

        dctTestValues.Add("two1nine", 29);
        dctTestValues.Add("eightwothree", 83);
        dctTestValues.Add("abcone2threexyz", 13);
        dctTestValues.Add("xtwone3four", 24);
        dctTestValues.Add("4nineeightseven2", 42);
        dctTestValues.Add("zoneight234", 14);
        dctTestValues.Add("7pqrstsixteen", 76);
        dctTestValues.Add("eighthree", 83);
        dctTestValues.Add("sevenine", 79);


        foreach (string strLine in dctTestValues.Keys)
        {
            int iCalibrationValue = Helper.getCalibarationValue(strLine);

            int iExpectedValue = dctTestValues[strLine];

            if (iCalibrationValue != iExpectedValue)
            {
                return;
            }
        }


        Dictionary<string, int> dctData = new Dictionary<string, int>();
        int iLineNumber = 0;

        while ((line = reader.ReadLine()) != null)
        {
            int iCalibrationValue = Helper.getCalibarationValue(line);

            // Add to dictionary
            dctData.Add(iLineNumber + ":" + line, iCalibrationValue);
            iLineNumber++;
        }

        // Loop around dictionary and add up the first and last numbers in the lisL
        int iSum = 0;

        foreach(int iResult in dctData.Values)
        {
            iSum += iResult;
        }

        Console.WriteLine(iSum);
    }
}
else
{
    Console.WriteLine("File not found: " + filePath);
}


public static class Helper
{
    static Dictionary<string, int> dctNumbers = new Dictionary<string, int>();

    public static void setUpNumberWordDictionary()
    {
        dctNumbers.Add("one", 1);
        dctNumbers.Add("two", 2);
        dctNumbers.Add("three", 3);
        dctNumbers.Add("four", 4);
        dctNumbers.Add("five", 5);
        dctNumbers.Add("six", 6);
        dctNumbers.Add("seven", 7);
        dctNumbers.Add("eight", 8);
        dctNumbers.Add("nine", 9);
    }
    public static int getCalibarationValue(string strLine)
    {
        List<int> lstIntegers = new List<int>();
        string strWord = string.Empty;
        int iResult = 0;

        foreach (char character in strLine)
        {
            if (char.IsDigit(character))
            {
                int intValue = character - '0';
                lstIntegers.Add(intValue);
                strWord = string.Empty;
            }

            // Else build up word string
            strWord = strWord + character;

            foreach (string strKey in dctNumbers.Keys)
            {
                // does the word contain the key?
                if (strWord.Contains(strKey))
                {
                    int iWordNumber = dctNumbers[strKey];
                    lstIntegers.Add(iWordNumber);
                    // Keep only the last character
                    strWord = strWord.Substring(strWord.Length - 1);

                }
            }
        }

        // Add to dictionary
        if (lstIntegers.Count > 0)
        {
            int iFirstNumber = lstIntegers.First();
            int iLastNumber = lstIntegers.Last();

            string concatenatedString = iFirstNumber.ToString() + iLastNumber.ToString();
            iResult = int.Parse(concatenatedString);
        }

        return iResult;
    }
}