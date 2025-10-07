namespace Telerik.ILS.Common;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// Provides a set of useful extension methods for the <see cref="System.String"/> class.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Converts the input string into its MD5 hash representation (a 32-character hexadecimal string).
    /// </summary>
    /// <param name="input">The string to be hashed.</param>
    /// <returns>A 32-character hexadecimal string representing the MD5 hash.</returns>
    public static string ToMd5Hash(this string input)
    {
        var md5Hash = MD5.Create();

        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        var builder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            builder.Append(data[i].ToString("x2"));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Converts common string representations of truthiness to a boolean value.
    /// </summary>
    /// <param name="input">The string to check (e.g., "true", "ok", "yes", "1", "да").</param>
    /// <returns><c>true</c> if the input matches a recognized true value, otherwise <c>false</c>.</returns>
    public static bool ToBoolean(this string input)
    {
        var stringTrueValues = new[] { "true", "ok", "yes", "1", "да" };
        return stringTrueValues.Contains(input.ToLower());
    }

    /// <summary>
    /// Converts the input string to a <see cref="System.Int16"/> (<c>short</c>).
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The parsed short value, or 0 if parsing fails.</returns>
    public static short ToShort(this string input)
    {
        short shortValue;
        short.TryParse(input, out shortValue);
        return shortValue;
    }

    /// <summary>
    /// Converts the input string to a <see cref="System.Int32"/> (<c>int</c>).
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The parsed integer value, or 0 if parsing fails.</returns>
    public static int ToInteger(this string input)
    {
        int integerValue;
        int.TryParse(input, out integerValue);
        return integerValue;
    }

    /// <summary>
    /// Converts the input string to a <see cref="System.Int64"/> (<c>long</c>).
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The parsed long value, or 0 if parsing fails.</returns>
    public static long ToLong(this string input)
    {
        long longValue;
        long.TryParse(input, out longValue);
        return longValue;
    }

    /// <summary>
    /// Converts the input string to a <see cref="System.DateTime"/> object.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The parsed DateTime value, or <c>DateTime.MinValue</c> if parsing fails.</returns>
    public static DateTime ToDateTime(this string input)
    {
        DateTime dateTimeValue;
        DateTime.TryParse(input, out dateTimeValue);
        return dateTimeValue;
    }

    /// <summary>
    /// Capitalizes the first letter of the input string.
    /// </summary>
    /// <param name="input">The string to capitalize.</param>
    /// <returns>The string with the first letter capitalized. Returns the original string if it is null or empty.</returns>
    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return input.Substring(0, 1).ToUpper(CultureInfo.CurrentCulture) + input.Substring(1, input.Length - 1);
    }

    /// <summary>
    /// Extracts a substring that lies between two specified substrings (<paramref name="startString"/> and <paramref name="endString"/>).
    /// </summary>
    /// <param name="input">The source string to search within.</param>
    /// <param name="startString">The starting delimiter.</param>
    /// <param name="endString">The ending delimiter.</param>
    /// <param name="startFrom">The optional start index for searching within the source string (0-based).</param>
    /// <returns>The extracted substring, or <see cref="string.Empty"/> if the delimiters are not found.</returns>
    public static string GetStringBetween(this string input, string startString, string endString, int startFrom = 0)
    {
        input = input.Substring(startFrom);
        startFrom = 0;

        if (!input.Contains(startString) || !input.Contains(endString))
        {
            return string.Empty;
        }

        var startPosition = input.IndexOf(startString, startFrom, StringComparison.Ordinal) + startString.Length;

        if (startPosition == -1)
        {
            return string.Empty;
        }

        var endPosition = input.IndexOf(endString, startPosition, StringComparison.Ordinal);
        if (endPosition == -1)
        {
            return string.Empty;
        }

        return input.Substring(startPosition, endPosition - startPosition);
    }

    /// <summary>
    /// Converts Bulgarian Cyrillic letters within the string to their Latin (transliterated) representations.
    /// </summary>
    /// <param name="input">The string containing Cyrillic characters.</param>
    /// <returns>The string with Cyrillic characters replaced by Latin ones.</returns>
    public static string ConvertCyrillicToLatinLetters(this string input)
    {
        var bulgarianLetters = new[]
                                       {
                                           "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п",
                                           "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я"
                                       };
        var latinRepresentationsOfBulgarianLetters = new[]
                                                        {
                                                            "a", "b", "v", "g", "d", "e", "j", "z", "i", "y", "k",
                                                            "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h",
                                                            "c", "ch", "sh", "sht", "u", "i", "yu", "ya"
                                                        };

        for (var i = 0; i < bulgarianLetters.Length; i++)
        {
            input = input.Replace(bulgarianLetters[i], latinRepresentationsOfBulgarianLetters[i]);

            input = input.Replace(bulgarianLetters[i].ToUpper(), latinRepresentationsOfBulgarianLetters[i].CapitalizeFirstLetter());
        }

        return input;
    }

    /// <summary>
    /// Converts Latin letters in the string to their corresponding Bulgarian Cyrillic characters
    /// based on the standard Bulgarian keyboard layout (QWERTY to ЙЦУКЕН).
    /// </summary>
    /// <param name="input">The string containing Latin characters typed with a Bulgarian layout.</param>
    /// <returns>The string with Latin characters converted to Bulgarian Cyrillic.</returns>
    public static string ConvertLatinToCyrillicKeyboard(this string input)
    {
        var latinLetters = new[]
                               {
                                   "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
                                   "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
                               };

        var bulgarianRepresentationOfLatinKeyboard = new[]
                                                        {
                                                            "а", "б", "ц", "д", "е", "ф", "г", "х", "и", "й", "к",
                                                            "л", "м", "н", "о", "п", "я", "р", "с", "т", "у", "ж",
                                                            "в", "ь", "ъ", "з"
                                                        };

        for (int i = 0; i < latinLetters.Length; i++)
        {
            input = input.Replace(latinLetters[i], bulgarianRepresentationOfLatinKeyboard[i]);
            input = input.Replace(latinLetters[i].ToUpper(), bulgarianRepresentationOfLatinKeyboard[i].ToUpper());
        }

        return input;
    }

    /// <summary>
    /// Converts the string to a valid username format: 
    /// 1. Converts Cyrillic to Latin. 
    /// 2. Removes all characters that are not letters, digits, underscore (_), or dot (.).
    /// </summary>
    /// <param name="input">The source string.</param>
    /// <returns>A clean, valid username string.</returns>
    public static string ToValidUsername(this string input)
    {
        input = input.ConvertCyrillicToLatinLetters();

        return Regex.Replace(input, @"[^a-zA-z0-9_\.]+", string.Empty);
    }

    /// <summary>
    /// Converts the string to a valid Latin file name (suitable for URLs/slugs): 
    /// 1. Converts Cyrillic to Latin. 
    /// 2. Replaces spaces with hyphens (-).
    /// 3. Removes all characters that are not letters, digits, underscore (_), dot (.), or hyphen (-).
    /// </summary>
    /// <param name="input">The source string.</param>
    /// <returns>A clean, valid Latin file name string.</returns>
    public static string ToValidLatinFileName(this string input)
    {
        input = input.Replace(" ", "-").ConvertCyrillicToLatinLetters();

        return Regex.Replace(input, @"[^a-zA-z0-9_\.\-]+", string.Empty);
    }

    /// <summary>
    /// Returns the first specified number of characters from the string.
    /// </summary>
    /// <param name="input">The source string.</param>
    /// <param name="charsCount">The maximum number of characters to retrieve.</param>
    /// <returns>The substring of the first <paramref name="charsCount"/> characters, or the whole string if it is shorter.</returns>
    public static string GetFirstCharacters(this string input, int charsCount)
    {
        return input.Substring(0, Math.Min(input.Length, charsCount));
    }

    /// <summary>
    /// Extracts the file extension from a file name (e.g., "txt" from "document.txt").
    /// </summary>
    /// <param name="fileName">The file name string.</param>
    /// <returns>The file extension in lowercase, or <see cref="string.Empty"/> if no valid extension is found.</returns>
    public static string GetFileExtension(this string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return string.Empty;
        }

        string[] fileParts = fileName.Split(new[] { "." }, StringSplitOptions.None);

        if (fileParts.Count() == 1 || string.IsNullOrEmpty(fileParts.Last()))
        {
            return string.Empty;
        }

        return fileParts.Last().Trim().ToLower();
    }

    /// <summary>
    /// Maps a file extension to its corresponding standard MIME content type.
    /// </summary>
    /// <param name="fileExtension">The file extension (e.g., "pdf").</param>
    /// <returns>The MIME content type string (e.g., "application/pdf"), or "application/octet-stream" as a fallback.</returns>
    public static string ToContentType(this string fileExtension)
    {
        var fileExtensionToContentType = new Dictionary<string, string>
                                            {
                                                { "jpg", "image/jpeg" },
                                                { "jpeg", "image/jpeg" },
                                                { "png", "image/x-png" },
                                                {
                                                    "docx",
                                                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                                                },
                                                { "doc", "application/msword" },
                                                { "pdf", "application/pdf" },
                                                { "txt", "text/plain" },
                                                { "rtf", "application/rtf" }
                                            };

        string trimmedExtension = fileExtension.Trim().ToLower();

        if (fileExtensionToContentType.ContainsKey(trimmedExtension))
        {
            return fileExtensionToContentType[trimmedExtension];
        }

        return "application/octet-stream";
    }

    /// <summary>
    /// Converts the string to a byte array using block copy for efficiency.
    /// </summary>
    /// <param name="input">The string to convert.</param>
    /// <returns>The resulting byte array.</returns>
    public static byte[] ToByteArray(this string input)
    {
        var bytesArray = new byte[input.Length * sizeof(char)];

        Buffer.BlockCopy(input.ToCharArray(), 0, bytesArray, 0, bytesArray.Length);
        return bytesArray;
    }
}