﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Riganti.Utils.Testing.SeleniumCore;

namespace DotVVM.Samples.Tests.Control
{
    [TestClass]
    public class FileUploadTests : SeleniumTestBase
    {

        [TestMethod]
        [Timeout(60000)]
        public void FileUpload()
        {
            RunInAllBrowsers(browser =>
            {
                browser.NavigateToUrl("ControlSamples/FileUpload");

                // get existing files
                var existingFiles = browser.FindElements("li").Select(e => e.GetText()).ToList();

                browser.Click(".dot-upload-button a");
                browser.Wait();

                // generate a sample file to upload
                var tempFile = Path.GetTempFileName();
                File.WriteAllText(tempFile, string.Join(",", Enumerable.Range(1, 100000)));

                // write the full path to the dialog
                SendKeys.SendWait(tempFile);
                SendKeys.SendWait("{Enter}");

                // wait for the file to be uploaded
                while (browser.First(".dot-upload-files").GetText() != "1 files")
                {
                    browser.Wait(2000);
                }
                TestContext.WriteLine("The file was uploaded.");

                // submit
                browser.Click("input[type=button]");

                // verify the file is there present
                ElementWrapper firstLi;
                do
                {
                    browser.Wait(2000);
                    firstLi = browser.First("ul").FindElements("li").FirstOrDefault(t => !existingFiles.Contains(t.GetText()));
                }
                while (firstLi == null);

                // delete the file
                browser.NavigateToUrl("ControlSamples/FileUpload?delete=" + firstLi.GetText());

                // delete the temp file
                File.Delete(tempFile);
            });
        }

    }
}