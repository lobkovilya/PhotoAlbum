using System.Collections.Generic;

namespace PhotoAlbum.Client.Models
{
    public class FeedModel
    {
        public IEnumerable<PostDisplayModel> Posts { get; set; }

        public string UserName { get; set; }
        
        public string UserFilter { get; set; }
    }
}