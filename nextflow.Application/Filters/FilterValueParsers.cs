using System.Globalization;

namespace Nextflow.Application.Filters;

public static class FilterValueParsers
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

    public static bool TryParseGuid(string value, out Guid guid)
        => Guid.TryParse(value, out guid);

    public static bool TryParseBool(string value, out bool b)
    {
        var v = value.Trim().ToLowerInvariant();
        switch (v)
        {
            case "true":
            case "1":
            case "yes":
            case "y":
            case "on":
            case "sim":
            case "s":
                b = true;
                return true;
            case "false":
            case "0":
            case "no":
            case "n":
            case "off":
            case "nao":
            case "não":
                b = false;
                return true;
            default:
                return bool.TryParse(v, out b);
        }
    }

    public static bool TryParseInt(string value, out int i)
        => int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out i)
           || int.TryParse(value, NumberStyles.Integer, PtBr, out i);

    public static bool TryParseDecimal(string value, out decimal d)
        => decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out d)
           || decimal.TryParse(value, NumberStyles.Number, PtBr, out d);

    public static bool TryParseDateOnly(string value, out DateOnly date)
    {
        // Preferência: ISO-8601 (yyyy-MM-dd). Aceita também pt-BR (dd/MM/yyyy).
        if (DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            return true;

        if (DateOnly.TryParseExact(value, "dd/MM/yyyy", PtBr, DateTimeStyles.None, out date))
            return true;

        // fallback: tentar DateTime e pegar a parte da data
        if (TryParseDateTime(value, out var dt))
        {
            date = DateOnly.FromDateTime(dt);
            return true;
        }

        return false;
    }

    public static bool TryParseDateTime(string value, out DateTime dt)
    {
        // ISO-8601 comum
        var formats = new[]
        {
            "yyyy-MM-ddTHH:mm:ss.FFFK",
            "yyyy-MM-ddTHH:mm:ssK",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd"
        };

        if (DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out dt))
            return true;

        // fallback culturas
        if (DateTime.TryParse(value, CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                out dt))
            return true;

        return DateTime.TryParse(value, PtBr,
            DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
            out dt);
    }
}

