using NUnit.Framework;
using world;

namespace Tests
{
    public class testrealm
    {
        public Realm realm;

        [SetUp]
        public void Setup()
        {
            realm = new Realm();
        }

        [Test]
        public void TestAddUser()
        {
            ICommand x = realm.AddUser();
        }
    }
}