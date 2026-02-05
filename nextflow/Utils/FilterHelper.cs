using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace Nextflow.Utils;

public static class FilterHelper
{
    public static IReadOnlyDictionary<string, string>? Parse(string? filtersJson)
    {
        if (string.IsNullOrWhiteSpace(filtersJson))
            return null;

        try
        {
            // Aceita valores que venham como number/bool/date no JSON e converte para string.
            var raw = JsonConvert.DeserializeObject<Dictionary<string, object?>>(filtersJson);
            if (raw == null || raw.Count == 0) return null;

            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var (k, v) in raw)
            {
                if (string.IsNullOrWhiteSpace(k) || v is null) continue;

                var s = v is IFormattable f
                    ? f.ToString(null, CultureInfo.InvariantCulture)
                    : v.ToString();

                if (string.IsNullOrWhiteSpace(s)) continue;
                result[k] = s.Trim();
            }

            return result.Count > 0 ? result : null;
        }
        catch
        {
            return null;
        }
    }

    public static IReadOnlyDictionary<string, string>? EnsureDefault(
        IReadOnlyDictionary<string, string>? filters,
        string key,
        string value)
    {
        var dict = filters != null
            ? new Dictionary<string, string>(filters, StringComparer.OrdinalIgnoreCase)
            : new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!dict.ContainsKey(key))
            dict[key] = value;

        return dict.Count > 0 ? dict : null;
    }
}
