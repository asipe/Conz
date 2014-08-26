using System;
using Conz.Core;
using Conz.Core.ConsoleAbstraction;
using Moq;
using NUnit.Framework;

namespace Conz.UnitTests.Core {
  [TestFixture]
  public class ColoredActionFactoryTest : BaseTestCase {
    [Test]
    public void TestBuild() {
      var defaultClass = CA<Class>();
      var currentClass = CA<Class>();
      var action = mFactory.Build(mConsole.Object, defaultClass, currentClass);
      mConsole.Setup(c => c.ForegroundColor).Returns(ConsoleColor.Green);
      mConsole.Setup(c => c.BackgroundColor).Returns(ConsoleColor.White);
      mConsole.SetupSet(c => c.ForegroundColor = (ConsoleColor)currentClass.Color.Value);
      mConsole.SetupSet(c => c.BackgroundColor = (ConsoleColor)currentClass.BackgroundColor.Value);
      mConsole.SetupSet(c => c.ForegroundColor = ConsoleColor.Green);
      mConsole.SetupSet(c => c.BackgroundColor = ConsoleColor.White);
      mConsole.Setup(c => c.WriteLine(""));
      action.Execute(c => c.WriteLine(""));
    }

    [Test]
    public void TestBuildCreatedNewInstances() {
      var action1 = mFactory.Build(mConsole.Object, CA<Class>(), CA<Class>());
      var action2 = mFactory.Build(mConsole.Object, CA<Class>(), CA<Class>());
      Assert.That(ReferenceEquals(action1, action2), Is.False);
    }

    [Test]
    public void TestBuildCreatesColoredActions() {
      Assert.That(mFactory.Build(mConsole.Object, CA<Class>(), CA<Class>()), Is.TypeOf<ColoredAction>());
    }

    [SetUp]
    public void DoSetup() {
      mConsole = Mok<IConsole>();
      mFactory = new ColoredActionFactory();
    }

    private Mock<IConsole> mConsole;
    private ColoredActionFactory mFactory;
  }
}