namespace Game.Script
{
    public interface IStateSwitcher
    {
        public void Switch<T>() where T : StateBase;
    }
}