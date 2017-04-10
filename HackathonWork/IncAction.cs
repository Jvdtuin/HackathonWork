namespace HackathonWork
{
    internal class IncAction : Action
    {
        public Factory Source { get; private set; }

        public IncAction(Factory source)
        {
            Source = source;
        }
    }
}