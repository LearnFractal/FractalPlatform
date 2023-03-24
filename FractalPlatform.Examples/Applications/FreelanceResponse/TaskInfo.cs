using System.Collections.Generic;

namespace BigDoc.Client.WebApp.Applications.FreelanceResponse
{
    public enum StatusType
    {
        New,
        InProgress,
        Rejected,
        Completed
    }

    public enum TenerModelType
    {
        BestDemo,
        BestDeveloper
    }

    public class DemoInfo
    {
        public string Who { get; set; }
    }

    public class AcceptInfo
    {
        public string Who { get; set; }
    }

    public class TaskInfo
    {
        public string TaskNumber { get; set; }

        public string Who { get; set; }

        public string Developer { get; set; }

        public TenerModelType TenderModel { get; set; }

        public StatusType Status { get; set; }

        public uint MinPrice { get; set; }

        public uint MaxPrice { get; set; }

        public List<AcceptInfo> Accepts { get; set; }

        public List<DemoInfo> Demos { get; set; }
    }
}
