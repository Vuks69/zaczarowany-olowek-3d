namespace Assets.Scripts.Actions
{
    public class ActionsData
    {
        public Selecting Selecting { get; } = new Selecting();
        public LineDrawing LineDrawing { get; } = new LineDrawing();
        public Erasing Erasing { get; } = new Erasing();
        public ObjectSelecting ObjectSelecting { get; } = new ObjectSelecting();
		public ObjectAdding ObjectAdding { get; } = new ObjectAdding();
    }
}