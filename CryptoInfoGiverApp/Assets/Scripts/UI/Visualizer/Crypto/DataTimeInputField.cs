using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class DataTimeInputField : MonoBehaviour
{
    private TMP_InputField _text;

    private void Awake()
    {
        _text = GetComponent<TMP_InputField>();
    }

    public bool TryParse(out DateTime dateTime)
    {
        return TryParseDataTime(_text.text, out dateTime);
    }

    private bool TryParseDataTime(string input, out DateTime dateTime)
    {
        dateTime = default(DateTime);

        const char splitChar = '/';
        const int splitCount = 3;

        const int dayIndex = 2;
        const int monthIndex = 1;
        const int yearIndex = 0;

        try
        {
            var splitedString = input.Split(splitChar);

            if (splitedString.Length != splitCount)
                return false;

            var dates = new int[splitCount];

            for (int i = 0; i < splitCount; i++)
            {
                if (int.TryParse(splitedString[i], out dates[i]) == false)
                    return false;
            }

            dateTime = new DateTime(dates[yearIndex], dates[monthIndex], dates[dayIndex]);

            return true;
        }
        catch
        {
            return false;
        }
    }
}
