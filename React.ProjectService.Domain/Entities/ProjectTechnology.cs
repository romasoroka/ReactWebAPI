using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.ProjectService.Domain.Entities
{
    public class ProjectTechnology
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int TechnologyId { get; set; }
    }
}
