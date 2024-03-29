using System.Collections.Generic;
using System.Linq;

namespace CharacterOfTheDay.Http
{
    public class HttpHeaders
    {
        readonly IDictionary<string, IList<string>> headers = new Dictionary<string, IList<string>>();

        public HttpHeaders Add(string name, string value)
        {
            var lowerName = name.ToLowerInvariant();
            if (!Exists(lowerName))
            {
                headers.Add(lowerName, new List<string>());
            }

            headers[lowerName].Add(value);
            return this;
        }

        internal void AddAll(IEnumerable<KeyValuePair<string, IEnumerable<string>>> headerCollection)
        {
            foreach (var header in headerCollection)
            {
                foreach (var value in header.Value)
                {
                    Add(header.Key, value);
                }
            }
        }

        public string GetValue(string name)
        {
            var lowerName = name.ToLowerInvariant();
            if (Exists(lowerName))
                return headers[lowerName].First();

            return string.Empty;
        }

        public bool Exists(string name)
        {
            return headers.ContainsKey(name.ToLowerInvariant());
        }

        public IList<string> GetAllValues(string name)
        {
            var lowerName = name.ToLowerInvariant();
            if (Exists(lowerName))
                return headers[lowerName];

            return new List<string>();
        }

        public IList<string> GetAllHeaderNames()
        {
            return headers.Keys.ToList();
        }
    }
}