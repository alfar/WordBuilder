using NUnit.Framework;
using NMock2;
using System;
using System.Collections.Generic;
using System.Text;
using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.ProjectV2;

namespace test
{
    [TestFixture()]
    public class TranslationNodeTest
    {
        [Test()]
        public void TestWorkingTranslation()
        {
            ProjectSerializer ps = new ProjectSerializer("a b c => d e f\n", null, null);

            TranslationNode tn = new TranslationNode(ps);

            Assert.IsTrue(tn.Successful);
            Assert.AreEqual(new string[] { "a", "b", "c" }, tn.Source.ToArray());
            Assert.AreEqual(new string[] { "d", "e", "f" }, tn.Destination.ToArray());
        }

        [Test()]
        public void TestBrokenTranslationMissingArrow()
        {
            ProjectSerializer ps = new ProjectSerializer("a b c\n", null, null);

            TranslationNode tn = new TranslationNode(ps);

            Assert.IsFalse(tn.Successful);
        }
    }
}
