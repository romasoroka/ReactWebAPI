using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.ProjectService.Application.Dtos
{
    public class ProjectShortDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ProjectStatus Status { get; set; }
        public List<int> TechnologyIds { get; set; } = new();
        public string Description { get; set; } = null!;
    }
}
