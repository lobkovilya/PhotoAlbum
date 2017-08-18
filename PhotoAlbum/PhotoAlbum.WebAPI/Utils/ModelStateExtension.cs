using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace PhotoAlbum.WebAPI.Utils
{
    public static class ModelStateExtension
    {
        public static void AddModelErrors(this ModelStateDictionary model, Dictionary<string, string[]> errors)
        {
            errors?.Keys.ToList()
                .ForEach(key => errors[key].ToList()
                    .ForEach(message => model.AddModelError(key, message)));
        }
    }
}