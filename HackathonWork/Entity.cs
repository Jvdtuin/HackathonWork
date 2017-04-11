namespace HackathonWork
{
    public abstract class Entity
    {
        private static int unqueEntiyId = 0;

        protected int _id;
        protected EntityType _type;

        public Entity(EntityType type )
        {
            _type = type;
            _id = unqueEntiyId++;
        }

        public abstract string ToPlayerString(int playerIdx);

        protected string ToPlayerString(int arg1, int arg2, int arg3, int arg4, int arg5)
        {
            return $"{_id} {_type} {arg1} {arg2} {arg3} {arg4} {arg5}";
        }

        public int Id { get { return _id; } }

		internal static void Reset()
		{
			unqueEntiyId = 0;
		}
    }
}