using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StringResources;

namespace PhotoAlbum.Client.Models
{
    public class PostEditModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "PostCaption", ResourceType = typeof(Resources))]
        public string Caption { get; set; }

        [Required]
        [Display(Name = "PostDescription", ResourceType = typeof(Resources))]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}