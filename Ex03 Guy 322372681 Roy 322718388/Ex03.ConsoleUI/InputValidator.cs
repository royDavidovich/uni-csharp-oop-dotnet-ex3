using System;

public class InputValidator
{
    public static void ValidateNonEmptyString(string i_Input, string i_FieldName)
    {
        if (string.IsNullOrWhiteSpace(i_Input))
        {
            throw new FormatException($"{i_FieldName} cannot be empty or whitespace."); 
        }
    }

    public static void ValidateFloat(string i_Input, string i_FieldName)
    {
        if (!float.TryParse(i_Input, out _))
        {
            throw new FormatException($"{i_FieldName} must be a valid number."); 
        }
    }

    public static void ValidateEnum(string i_Input, Type i_EnumType, string i_FieldName)
    {
        if (!Enum.IsDefined(i_EnumType, i_Input))
        {
            throw new FormatException($"{i_FieldName} is invalid."); 
        }
    }
} 
