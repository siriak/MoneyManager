using System;
using Xunit;

namespace Core.Tests
{
    public class StateManagerTests
    {
        [Fact]
        public void ReturnsTrue()
        {
            Assert.True(true);
        }

        [Fact]
        public void Increment()
        {
            var res = State.Increment(5);
            Assert.Equal(6, res);
        }

        [Fact]
        public void IncrementWhong()
        {
            var res = State.Increment(7);
            Assert.Equal(6, res);
        }
    }
}
