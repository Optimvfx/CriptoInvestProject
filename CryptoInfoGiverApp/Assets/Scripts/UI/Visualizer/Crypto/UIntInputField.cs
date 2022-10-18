using System;
using TMPro;
using UnityEngine;

public class UIntInputField : MonoBehaviour
{
    private TMP_InputField _text;

    private void Awake()
    {
        _text = GetComponent<TMP_InputField>();
    }

    public bool TryParse(out uint value)
    {
        return TryParseUInt(_text.text, out value);
    }

    private bool TryParseUInt(string input, out uint value)
    {
        value = 0;
        var parsedValue = 0;

        if (int.TryParse(input, out parsedValue) == false)
            return false;

        if(parsedValue < 0)
            return false;

        value = (uint)parsedValue;

        return true;
    }
}
