using NUnit.Framework;
using Whee.WordBuilder.ProjectV2;
using Whee.WordBuilder.Model.Commands;
using NMock2;

namespace test.Commands
{
    [TestFixture()]
    public class RuleCommandTest
    {
        [Test()]
        public void TestLoadCommand()
        {
            Mockery mockery = new Mockery();
            IProjectSerializer serializer = mockery.NewMock<IProjectSerializer>();
            RuleCommand rc = new RuleCommand();

            Expect.Once.On(serializer).GetProperty("LineNumber").Will(Return.Value(1));
            Expect.Once.On(serializer).Method("ReadTextToken").Will(Return.Value(new Token(TokenType.Text, "a", 0, 1)));
            Expect.Once.On(serializer).Method("ReadTextToken").Will(Return.Value(null));

            rc.LoadCommand(serializer);

            Assert.AreEqual("a", rc.Rule);

            mockery.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
