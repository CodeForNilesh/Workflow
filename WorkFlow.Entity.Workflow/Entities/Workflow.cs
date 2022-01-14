using System.Collections.Generic;

namespace WorkFlow.WorkflowManagement.Entities
{
    public class Workflow
    {
        public int WorkflowID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public List<Stage> Stages { get; set; }
    }
}
