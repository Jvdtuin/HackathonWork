namespace HackathonWork
{
    internal class IncAction : Action
    {
        private Factory _source;

        public IncAction(Factory source)
        {
            _source = source;
        }
    }
}