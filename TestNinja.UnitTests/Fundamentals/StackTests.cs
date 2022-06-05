using NUnit.Framework;
using System;
using TestNinja.Fundamentals;
using Stack = TestNinja.Fundamentals.Stack<string>;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackTests
    {
        private Stack stack;

        [SetUp]
        public void SetUp()
        {
            stack = new Stack();
            
        }

        [Test]
        public void Count_StackIsEmpty_Return0()
        {
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void Count_StackHasElement_ReturnNumberOfElements(int numElements)
        {
            for (int i = 1; i <= numElements; i++)
            {
                stack.Push("a");
            }

            Assert.That(stack.Count, Is.EqualTo(numElements));
        }

        [Test]
        public void Push_ArgisNull_ThrowArgumentNullException()
        {
            Assert.That(() => stack.Push(null), Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Push_ArgisValid_AddObjectToStack()
        {
            stack.Push("a");
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnsObjectOnTopOfStack()
        {
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            
            string result = stack.Pop();
            
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemoveObjectOnTopOfStack()
        {
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithAFewObjects_ReturnsObjectOnTopOfStack()
        {
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            Assert.That(stack.Peek(), Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithAFewObjects_DoesNotRemoveObjectOnTopOfStack()
        {
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}
