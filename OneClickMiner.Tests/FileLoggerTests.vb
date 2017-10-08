Imports FakeItEasy
Imports NUnit.Framework
Imports VertcoinOneClickMiner.Core


<TestFixture> _
Class FileLoggerTests
    Private logFilePath As String
    Private systemIo As ISystemWrapper

    Private Function CreateSut() As FileLogger
        Return New FileLogger(logFilePath, systemIo)
    End Function

    <SetUp> _
    Public Sub SetUp()
        logFilePath = "theLogFile"
        systemIo = A.Fake(Of ISystemWrapper)()
    End Sub

    <Test>
    Public Sub Constructor_WhenCalledWithNullFilePath_ShouldThrowArgumentNullException()

        logFilePath = Nothing

        Assert.Throws(of ArgumentNullException)(Sub() CreateSut())

    End Sub

    <Test> _
    <TestCase("")> _
    <TestCase(vbCrLf)> _
    <TestCase(" ")> _
    Public Sub Constructor_WhenCalledWithInvalidFilePath_ShouldThrowArgumentException(path As string)

        logFilePath = path

        Assert.Throws(of ArgumentException)(Sub() CreateSut())

    End Sub

    <Test> _
    Public Sub Trace_WhenCalled_ShouldAppendToLogFileWithExpectedFormat()
        Dim resultLogFile As String = Nothing
        Dim resultLogLines As IEnumerable(Of String) = Nothing

        Dim timestamp = DateTime.Now
        A.CallTo(Function() systemIo.UtcNow).Returns(timestamp)
        A.CallTo(Sub() systemIo.AppendAllLines(A(Of string).Ignored, A(of IEnumerable(Of string)).Ignored)) _
            .Invokes(Sub(file As String, lines As IEnumerable(Of String))
                resultLogFile = file
                resultLogLines = lines
                        End Sub)

        Dim sut = CreateSut()

        Dim message As String = "the log message"
        Dim caller As String = "the method which called the logger"

        sut.Trace(message, caller)
        Dim expectedLogString = $"- {timestamp:O}, {caller}: {message}"

        Assert.That(resultLogFile, [Is].EqualTo(logFilePath))
        Assert.That(resultLogLines.First(), [Is].EqualTo(expectedLogString))
    End Sub

    <Test> _
    Public Sub LogError_WhenCalled_ShouldAppendToLogFileWithExceptionDetails()
        Dim resultLogFile As String = Nothing
        Dim resultLogLines As IEnumerable(Of String) = Nothing

        Dim timestamp = DateTime.Now
        A.CallTo(Function() systemIo.UtcNow).Returns(timestamp)
        A.CallTo(Sub() systemIo.AppendAllLines(A(Of string).Ignored, A(of IEnumerable(Of string)).Ignored)) _
            .Invokes(Sub(file As String, lines As IEnumerable(Of String))
                resultLogFile = file
                resultLogLines = lines
                        End Sub)

        Dim sut = CreateSut()

        Dim exception = New Exception("something went bang")

        Dim message As String = exception.ToString()
        Dim caller As String = "the caller method"

        sut.LogError(exception, caller)
        Dim expectedLogString = $"- {timestamp:O}, {caller}: {message}"

        Assert.That(resultLogFile, [Is].EqualTo(logFilePath))
        Assert.That(resultLogLines.First(), [Is].EqualTo(expectedLogString))
    End Sub
End Class