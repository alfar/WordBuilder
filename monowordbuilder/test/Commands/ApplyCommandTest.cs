using NUnit.Framework;
using Whee.WordBuilder.ProjectV2;
using Whee.WordBuilder.Model.Commands;

namespace test.Commands
{
    [TestFixture()]
    public class ApplyCommandTest
    {
        [Test()]
        public void TestLoadCommand()
        {
            IProjectNode project = ProjectSerializer.LoadString("rule root {\napply {\ntoken a\n}\n}\n", null, null);

            ApplyCommand cmd = (ApplyCommand)(project.Children[0].Children[0].Children[0]);

            Assert.AreEqual(1, cmd.Commands.Count);
        }
    }
}
