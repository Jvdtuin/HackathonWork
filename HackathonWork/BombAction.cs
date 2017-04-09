namespace HackathonWork
{
    internal class BombAction : Action
    {
        private Factory _source;
        private Factory _destination;

        public BombAction(Factory source, Factory destination)
        {
            _source = source;
            _destination = destination;
        }
        public Factory Source { get { return _source; } }
        public Factory Destination { get { return _destination; } }
    }
}