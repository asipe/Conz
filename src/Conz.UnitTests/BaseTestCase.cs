using System.Linq;
using Conz.Core;
using KellermanSoftware.CompareNetObjects;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Conz.UnitTests {
  [TestFixture]
  public abstract class BaseTestCase {
    [SetUp]
    public void BaseSetup() {
      MokFac = new MockRepository(MockBehavior.Strict);
      ObjectFixture = new Fixture();
      CustomizeObjectFixture();
    }

    [TearDown]
    public void BaseTearDown() {
      VerifyMocks();
    }

    protected Mock<T> Mok<T>() where T : class {
      return MokFac.Create<T>();
    }

    protected T[] BA<T>(params T[] items) {
      return items;
    }

    protected T CA<T>() {
      return ObjectFixture.Create<T>();
    }

    protected T[] CM<T>(int count) {
      return (count == 0)
               ? BA<T>()
               : ObjectFixture
                   .CreateMany<T>(count)
                   .ToArray();
    }

    protected void AssertAreEqual(object actual, object expected) {
      var result = DoCompare(actual, expected);
      Assert.That(result.AreEqual, Is.True, result.DifferencesString);
    }

    protected static bool AreEqual(object actual, object expected) {
      return DoCompare(actual, expected).AreEqual;
    }

    protected static TValue IsEq<TValue>(TValue x) {
      return Match.Create(value => AreEqual(value, x), () => It.IsAny<TValue>());
    }

    private static ComparisonResult DoCompare(object actual, object expected) {
      return _ObjectComparer.Compare(actual, expected);
    }

    private void VerifyMocks() {
      MokFac.VerifyAll();
    }

    private void CustomizeObjectFixture() {
      ObjectFixture
        .Customize<Class>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
    }

    protected MockRepository MokFac{get;private set;}
    protected Fixture ObjectFixture{get;private set;}
    private static readonly CompareLogic _ObjectComparer = new CompareLogic();
  }
}