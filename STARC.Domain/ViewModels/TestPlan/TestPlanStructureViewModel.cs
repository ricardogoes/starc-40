namespace STARC.Domain.ViewModels.TestPlan
{
    public class TestPlanStructureViewModel
    {
        public string NodeType { get; set; }

        public long ParentNodeId { get; set; }

        public long Id { get; set; }

        public string NodeId { get; set; }

        public string Value { get; set; }

        public int Level { get; set; }

        public bool HasChildren { get; set; }
    }
}
