using System.Text.Json;

public static class DTDLUnits
{
    private const string DtdlUnits = "./DtdlUnits.json";

    public static readonly Dictionary<string, string?[]?> UnitValues = new();
    //Add a Method to  parse  a json file and fillup th private readonly Dictionary<string, string[]> unitValues = new();

    public static void ParseJsonFile()
    {
        try
        {
            // Read the contents of the JSON file into a string
            string jsonString = File.ReadAllText(DtdlUnits);
            var document = JsonDocument.Parse(jsonString);
            var element = document.RootElement;

            // Fill up the unitValues dictionary with the parsed values
            foreach (JsonProperty property in element.EnumerateObject())
            {
                // Convert the property value to a string array
                JsonElement value = property.Value;
                switch (property.Name)
                {
                    case "families":
                        parseFamilies(value);
                        break;
                    default: break;

                }

            }
        }
        catch (JsonException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void parseFamilies(JsonElement element)
    {
        // Fill up the unitValues dictionary with the parsed values
        foreach (JsonProperty property in element.EnumerateObject())
        {
            // Convert the property value to a string array
            JsonElement value = property.Value;

            if (value.ValueKind != JsonValueKind.Array)
            {
                throw new InvalidOperationException($"Property '{property.Name}' does not contain an array value.");
            }
            var array = value.EnumerateArray().Select(e => e.GetString()).ToArray();

            // Add the property name and array to the dictionary
            if (array is not null)
                UnitValues.Add(property.Name, array);
        }
    }


    // Print all Units in the Dictionary
    public static void Print()
    {
        foreach (var entry in UnitValues)
        {
            Console.WriteLine(entry.Key);
            if (entry.Value == null) continue;
            foreach (var value in entry.Value)
            {
                Console.WriteLine(value);
            }
            Console.WriteLine();
        }

    }
}
