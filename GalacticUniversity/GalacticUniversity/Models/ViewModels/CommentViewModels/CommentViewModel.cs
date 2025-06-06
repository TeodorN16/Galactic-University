﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalacticUniversity.Models.ViewModels.CommentViewModels
{
    public class CommentViewModel
    {
        public int ID { get; set; }

        public string CommentText { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;

        public int Rating { get; set; }

        public int CourseID { get; set; }

        public List<SelectListItem> Courses { get; set; }
    }
}
