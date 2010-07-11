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
    public class BranchCommandTest
    {
        [Test()]
        public void TestLoadCommand()
        {
            IProjectNode project = ProjectSerializer.LoadString("rule root {\nbranch plural Plural-Form\n}\n", null, null);

            BranchCommand cmd = (BranchCommand)(project.Children[0].Children[0].Children[0]);

            Assert.AreEqual("plural", cmd.Name);
            Assert.AreEqual("Plural-Form", cmd.Rule);
        }
    }
}
