using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFL.BigDataBowl.Utilities;

namespace NFL.BigDataBowl.Utilities
{
    public static class CsvReader
    {
        public static async Task<List<string>> ParseCsv(string path)
        {
            var data = await Requester.GetData(path);
            var csv = data
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                .Skip(1)
                .SkipLast(1)
                .ToList();

            return csv;
        }
    }
}
