using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using VertcoinOneClickMiner.Core;

namespace OneClickMiner.Tests
{
    [TestFixture]
    class FileLoggerTests
    {
        private string logFilePath;
        private ISystemWrapper systemIo;

        private FileLogger CreateSut()
        {
            return new FileLogger(logFilePath, systemIo);
        }

        [SetUp]
        public void SetUp()
        {
            logFilePath = "theLogFile";
            systemIo = A.Fake<ISystemWrapper>();
        }

        [Test]
        public void Trace_WhenCalled_ShouldAppendToLogFileWithExpectedFormat()
        {
            string resultLogFile = null;
            IEnumerable<string> resultLogLines = null;

            var timestamp = DateTime.Now;
            A.CallTo(() => systemIo.UtcNow).Returns(timestamp);
            A.CallTo(() => systemIo.AppendAllLines(A<string>.Ignored, A<IEnumerable<string>>.Ignored))
                .Invokes((string file, IEnumerable<string> lines) =>
                {
                    resultLogFile = file;
                    resultLogLines = lines;
                });

            var sut = CreateSut();

            string message = "the log message";
            string caller = "the method which called the logger";

            sut.Trace(message, caller);
            var expectedLogString = $"- {timestamp:O}, {caller}: {message}";
            
            Assert.That(resultLogFile, Is.EqualTo(logFilePath));
            Assert.That(resultLogLines.First(), Is.EqualTo(expectedLogString));
        }

        [Test]
        public void LogError_WhenCalled_ShouldAppendToLogFileWithExceptionDetails()
        {
            string resultLogFile = null;
            IEnumerable<string> resultLogLines = null;

            var timestamp = DateTime.Now;
            A.CallTo(() => systemIo.UtcNow).Returns(timestamp);
            A.CallTo(() => systemIo.AppendAllLines(A<string>.Ignored, A<IEnumerable<string>>.Ignored))
                .Invokes((string file, IEnumerable<string> lines) =>
                {
                    resultLogFile = file;
                    resultLogLines = lines;
                });

            var sut = CreateSut();

            var exception = new Exception("something went bang");

            string message = exception.ToString();
            string caller = "the caller method";

            sut.LogError(exception, caller);
            var expectedLogString = $"- {timestamp:O}, {caller}: {message}";
            
            Assert.That(resultLogFile, Is.EqualTo(logFilePath));
            Assert.That(resultLogLines.First(), Is.EqualTo(expectedLogString));
        }
    }
}
