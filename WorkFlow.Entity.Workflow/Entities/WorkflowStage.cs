using System.Collections.Generic;

namespace WorkFlow.WorkflowManagement.Entities
{
    public class WorkflowStage
    {
        public int WorkflowStageID { get; set; }    
        public int WorkflowID { get; set; }
        public int StageID { get; set; }
        public int StageOrder { get; set; }
        public bool IsStageApprovalMandetory { get; set; }
        public Workflow Workflow { get; set; }
        public Stage Stage { get; set; }
    }
}
