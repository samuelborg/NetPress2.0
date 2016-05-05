using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace NetPress.ViewModels
{
    public class ViewPosts
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

        [Display(Name = "Date Created")]
        public DateTime? dateCreated { get; set; }

        [Display(Name = "Last Modified")]
        public DateTime? lastModified { get; set; }

        [Display(Name = "Author")]
        public string UserID { get; set; }

        [Display(Name = "Author")]
        public string UserFullName { get; set; }
    }
}