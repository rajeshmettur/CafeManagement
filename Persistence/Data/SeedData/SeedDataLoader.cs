using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.SeedData
{
   public static class SeedDataLoader
{
    public static List<T> LoadSeedData<T>(string fileName)
    {
        //var filePath = Path.Combine(AppContext.BaseDirectory, "SeedData", fileName);
        var path = $"D:\\Working\\Projects\\NTT\\CafeManagement\\Persistence\\Data\\";
        var filePath = Path.Combine(path, "SeedData", fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Seed data file not found: {filePath}");

        var jsonData = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<T>>(jsonData) ?? new List<T>();
    }
}
}