using System.Collections.Generic;

public static class LocalizationManager
{
    private static readonly Dictionary<string, Dictionary<string, string>> _locales;
    public static string lang = "ru_ru";

    // Static constructor to initialize the locales
    static LocalizationManager()
    {
        _locales = new Dictionary<string, Dictionary<string, string>>();

        // Example locale entries (add more as needed)
        _locales["en_us"] = new Dictionary<string, string>
        {
            {"quartz", "Quartz"}
        };

        _locales["ru_ru"] = new Dictionary<string, string>
        {
            {"quartz", "Кварц"}
        };
    }

    public static string Translate(string lang, string keyword)
    {
        if (string.IsNullOrEmpty(lang) || !_locales.ContainsKey(lang)) return "404";
        if (string.IsNullOrEmpty(keyword) || !_locales[lang].ContainsKey(keyword)) return "404";

        return _locales[lang][keyword];
    }
    public static string Auto(string keyword)
    {
        return Translate(lang, keyword);
    }
}
