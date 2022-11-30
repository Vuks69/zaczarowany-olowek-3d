namespace Assets.Scripts.Actions
{
    public abstract class Action
    {
        public static Action Instance;

        public void HandleLeftButton()
        {
            // left button is always //Undo so no need to override this
            // NOT NEEDED as //Undo is not reliant on specific actions
            ////Undo.Perform//Undo();
        }

        public void HandleRightButton()
        {
            // NOT NEEDED as redo is not reliant on specific actions
            ////Undo.PerformRedo();
        }

        public abstract void Init();
        public abstract void HandleTriggerDown();
        public abstract void HandleTriggerUp();
        public abstract void Update();
        public abstract void Finish();
    }
}