using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COWE.CaptureAgent;

namespace COWE_ClientTests
{
    [TestClass]
    public class CaptureAgentTests
    {
        [TestMethod]
        public void MoveCaptureFileToParseFolderTest()
        {
            // Arrange
            string captureFolderPath = @"C:\Temp\CaptureTest";
            string parseFolderPath = @"C:\Temp\ParseTest";
            string captureFileName = "TestFile.txt";
            
            StreamWriter sw = File.CreateText(captureFolderPath + '\\' + captureFileName);
            sw.Close();

            if (File.Exists(parseFolderPath + '\\' + captureFileName))
            {
                File.Delete(parseFolderPath + '\\' + captureFileName);
            }

            // Act
            CaptureAgent ca = new CaptureAgent();
            bool expected = ca.MoveCaptureFileToParseFolder(captureFolderPath, parseFolderPath, captureFileName);
            bool actual = File.Exists(parseFolderPath + '\\' + captureFileName);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
