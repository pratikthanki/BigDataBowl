using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NFL.BigDataBowl.Utilities
{
    public static class CsvReader
    {
        public static async Task<List<string>> RequestCsv(string path)
        {
            var data = await Requester.GetData(path);
            var csv = data
                .Split(new[] {Environment.NewLine}, StringSplitOptions.None)
                .Skip(1)
                .SkipLast(1)
                .ToList();

            return csv;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            var _dataRoot = new FileInfo(typeof(Program).Assembly.Location);

            Debug.Assert(_dataRoot.Directory != null);
            var assemblyFolderPath = _dataRoot.Directory.FullName;

            var fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}