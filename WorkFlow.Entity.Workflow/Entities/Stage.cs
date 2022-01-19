using System;
using System.Collections.Generic;
using WorkFlow.UserManagement.Entities;

namespace WorkFlow.WorkflowManagement.Entities
{
    public class Stage
    {
        public int StageID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Person> Reviewers { get; set; }
        public List<Department> ReviewerDepartments { get; set; }

        public List<Person> Escalators { get; set; }
        public List<Department> EscalatorDepartments { get; set; }
        public bool ActionCommentMandetory { get; set; }

        public int ReviewTime { get; set; }
        public int EscalationTime { get; set; }
        public bool IsAnyApprover { get; set; }
    }
}
