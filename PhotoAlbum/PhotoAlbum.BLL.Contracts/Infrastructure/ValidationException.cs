using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoAlbum.BLL.Contracts.Infrastructure
{
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }

        public Dictionary<string, string[]> ModelErrors;

        public ValidationException(string message, string prop = "")
        {
            Property = prop;
            ModelErrors = new Dictionary<string, string[]>
            {
                {prop, new[] {message}}
            };
        }

        public ValidationException(Dictionary<string, string[]> modelErrors)
        {
            ModelErrors = modelErrors;
        }

        
        public override string Message
        {
            get
            {
                return string.Join(";", 
                    ModelErrors.Select(x => ((x.Key != "")?(x.Key + "="):("")) + string.Join(",", x.Value)).ToArray());
            }
        }
    }
}