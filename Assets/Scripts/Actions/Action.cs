namespace Assets.Scripts.Actions
{
    public abstract class Action
    {
        public static Action Instance;

        public abstract void Init();
        public abstract void HandleTriggerDown();
        public abstract void HandleTriggerUp();
        public abstract void Update();
        public abstract void Finish();
    }
}