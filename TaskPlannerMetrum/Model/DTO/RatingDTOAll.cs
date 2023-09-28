using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace TaskPlannerMetrum.Model
{
    public class RatingDTOAll
    {
        [Key]

        public int UserId { get; set; }

        public int ProjectID { get; set; }

        public int RatingID { get; set; }

        public int RatingValue { get; set; }

        public int RatingDescriptioID { get; set; }

    }
}
