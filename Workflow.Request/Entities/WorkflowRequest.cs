using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Request.Entities
{
    public class WorkflowRequest
    {
        public int RequestID { get; set; }
        public int WorkflowID { get; set; }
        public int CurrentStageID { get; set; }
        public string Desciption { get; set; }
        public string WorkflowInstance { get; set; }

        public List<RequestStage> RequestStages;
    }

    public class RequestStage
    {
        public int RequestStageID { get; set; }
        public int RequestID { get; set; }
        public int StageID { get; set; }
        public int StageOrder { get; set; }
        public bool IsAnyApprover { get; set; }
        public bool IsStageApprovalMandetory { get; set; }
        public bool ActionCommentMandetory { get; set; }

        public List<RequestStageReviewer> Reviewers;
    }
    public class RequestStageReviewer
    {
        public int RequestStageReviewerID { get; set; }
        public int RequestStageID { get; set; }
        public int ReviewerID { get; set; }
        public int ReviewTime { get; set; }
        public bool Approval { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
