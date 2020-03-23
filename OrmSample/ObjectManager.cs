using System.IO;
using ECode.Data;
using ECode.DependencyInjection;

namespace Sample1
{
    public static class ObjectManager
    {
        private static ObjectContainer      objects     = null;


        public static IDatabase Database
        { get; private set; }


        static ObjectManager()
        {
            objects = new ObjectContainer(new FileInfo("objects.config"));

            Database = (IDatabase)objects.Get("Database");
        }


        public static T Get<T>(string objectId)
        {
            return (T)objects.Get(objectId);
        }
    }
}