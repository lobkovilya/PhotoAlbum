using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class PostCreateModel
    {
        [Required]
        [Display(Name = "PostCaption", ResourceType = typeof(Resources))]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "PostDescription", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string Login { get; set; }
    }
}