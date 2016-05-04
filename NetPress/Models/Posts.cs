using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace NetPress.Models
{
    public class Posts
    {
        [Key]
        [Display(Name = "Post ID")]
        public int postID { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Content")]
        public string content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string category { get; set; }

        public enum Status
        {
            Published = 0,
            Draft = 1,
            Archived = 2
        }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Status")]
        public Status status { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? dateCreated { get; set; }

        [Display(Name = "Last Modified")]
        public DateTime? lastModified { get; set; }

        [Display(Name = "Author")]
        public string UserID { get; set; }


    }
}