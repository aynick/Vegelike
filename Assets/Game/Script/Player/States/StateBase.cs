namespace Game.Script
{
    public abstract class StateBase
    {
        protected IStateSwitcher _switcher;
        public StateBase(IStateSwitcher switcher)
        {
            _switcher = switcher;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void FixedUpdate();
        public abstract void Update();
    }
}