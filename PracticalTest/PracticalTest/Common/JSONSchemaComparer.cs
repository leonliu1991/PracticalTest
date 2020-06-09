using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace PracticalTest.Common
{
    class JsonSchemaComparer
    {
        public void AssertArraySchema(JArray responseJ, string customSchemaPath)
        {
            var schemaFile = new StreamReader(customSchemaPath);
            using (schemaFile)
            {
                var schema = JSchema.Parse(Regex.Unescape(schemaFile.ReadToEnd()));

                if (!responseJ.IsValid(schema, out IList<string> errorMessages))
                    throw new Exception("\n Schema validation failed with error: " + string.Join("\n", errorMessages));
            }
        }

        public void AssertObjectSchema(JObject responseJ, string customSchemaPath)
        {
            var schemaFile = new StreamReader(customSchemaPath);
            using (schemaFile)
            {
                var schema = JSchema.Parse(Regex.Unescape(schemaFile.ReadToEnd()));

                if (!responseJ.IsValid(schema, out IList<string> errorMessages))
                    throw new Exception("\n Schema validation failed with error: " + string.Join("\n", errorMessages));
            }
        }
    }
}