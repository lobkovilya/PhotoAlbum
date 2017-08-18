using System;
using System.Collections.Generic;
using System.Linq;
using PhotoAlbum.DAL.Entities;

namespace PhotoAlbum.BLL.Utils
{
    public static class PageExtensions
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var itemToSkip = pageNumber * pageSize;
            return source.Skip(itemToSkip).Take(pageSize);
        }

        
        public static IEnumerable<Post> OrderBy(this IEnumerable<Post> source, string order)
        {
            switch (order)
            {
                case "new":
                    return source.OrderByDescending(x => x.Date);
                case "popular":
                    return source.OrderByDescending(x => x.Rating);
            }
            throw new ArgumentException($"No such order: {order}");
        }
    }
}