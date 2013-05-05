namespace More.Tests.Utilities
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using More.Utilities;

    [TestClass]
    public class RetryTests
    {
        [TestMethod]
        public void RetryFuncOnExceptionSuccess()
        {
            var result = Retry.OnException(run: () => 1);

            result.Should().Be(1);
        }

        [TestMethod]
        public void RetryFuncOnExceptionFailure()
        {
            this.Invoking(t =>
            {
                var result = Retry.OnException<int>(waitBetweenRetriesMsec: 0, run: () =>
                {
                    throw new ApplicationException();
                });

                result.Should().Be(0);

            }).ShouldThrow<ApplicationException>();
        }

        [TestMethod]
        public void RetryFuncOnExceptionFailureNoRetrow()
        {
            this.Invoking(t =>
            {
                var result = Retry.OnException<int>(waitBetweenRetriesMsec: 0, rethrowFinalException: false, run: () =>
                {
                    throw new ApplicationException();
                });

                result.Should().Be(0);

            }).ShouldNotThrow<ApplicationException>();
        }

        [TestMethod]
        public void RetryFuncOnExceptionSuccessOnSecondTry()
        {
            var count = 0;

            var result = Retry.OnException(waitBetweenRetriesMsec: 0, run: () =>
            {
                count++;
                if (count == 1)
                {
                    throw new ApplicationException();
                }

                return count;
            });

            result.Should().Be(2);
        }

        [TestMethod]
        public void RetryActionOnExceptionSuccess()
        {
            Retry.OnException(run: () => { });
        }

        [TestMethod]
        public void RetryActionOnExceptionFailure()
        {
            this.Invoking(t => Retry.OnException(waitBetweenRetriesMsec: 0, run: () =>
            {
                throw new ApplicationException();
            })).ShouldThrow<ApplicationException>();
        }

        [TestMethod]
        public void RetryActionOnExceptionFailureNoRetrow()
        {
            this.Invoking(t => Retry.OnException(waitBetweenRetriesMsec: 0, rethrowFinalException: false, run: () =>
            {
                throw new ApplicationException();
            })).ShouldNotThrow<ApplicationException>();
        }

        [TestMethod]
        public void RetryActionOnExceptionSuccessOnSecondTry()
        {
            var count = 0;

            Retry.OnException(waitBetweenRetriesMsec: 0, run: () =>
            {
                count++;
                if (count == 1)
                {
                    throw new ApplicationException();
                }
            });

            count.Should().Be(2);
        }

        [TestMethod]
        public void RetryFuncOnFalseSuccess()
        {
            Retry.OnFalse(run: () => true).Should().BeTrue();
        }

        [TestMethod]
        public void RetryFuncOnFalseFailed()
        {
            var count = 0;

            Retry.OnFalse(waitBetweenRetriesMsec: 0, numberOfRetries: 5, run: () =>
            {
                count++;
                return false;
            }).Should().BeFalse();

            count.Should().Be(5);
        }

        [TestMethod]
        public void RetryFuncOnFalseSuccessOnSecondTry()
        {
            var count = 0;

            Retry.OnFalse(waitBetweenRetriesMsec: 0, run: () =>
            {
                count++;
                if (count == 1)
                {
                    return false;
                }

                return true;
            }).Should().BeTrue();

            count.Should().Be(2);
        }
    }
}
