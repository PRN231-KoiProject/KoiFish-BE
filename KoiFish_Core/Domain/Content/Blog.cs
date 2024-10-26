using KoiFish_Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Core.Domain.Content
{
    public class Blog
    {
        [Key]
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set;}

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        public ICollection<ImageB> ImageBs { get; set; }
    }
}
