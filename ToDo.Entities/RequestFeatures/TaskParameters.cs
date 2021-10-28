namespace ToDo.Entities.RequestFeatures
{
    public class TaskParameters
    {
        public string Description { get; set; }
        public int MinProgress { get; set; }
        public int MaxProgress { get; set; } = 100;
    }
}