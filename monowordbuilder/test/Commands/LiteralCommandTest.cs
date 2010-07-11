using NUnit.Framework;
using NMock2;
using System;
using System.IO;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.ProjectV2;
using Whee.WordBuilder.Model.Commands;

namespace test.Commands
{
    [TestFixture()]
    public class LiteralCommandTest
    {
        [Test()]
        public void TestLoadCommand()
        {
            IProjectNode project = ProjectSerializer.LoadString("rule root {\nliteral a\n}\n", null, null);

            LiteralCommand cmd = (LiteralCommand)(project.Children[0].Children[0].Children[0]);

            Assert.AreEqual("a", cmd.Literal);
        }
    }
}
