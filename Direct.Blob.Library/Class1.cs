using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Direct.Shared;
using Direct.Interface;
using System.IO;
using log4net;

namespace Direct.Blob.Library
{
    /// <summary>
    /// Known Limitation, do not display base64 as value in the monitor.
    /// It is ok to use a local variable, but the monitor will crash with large texts.
    /// </summary>
    [DirectSealed]
    [DirectDom("Blob")]
    [ParameterType(false)]
    public class Blob
    {
        private static readonly ILog logArchitect = LogManager.GetLogger(Loggers.LibraryObjects);



        [DirectDom("Write Binary to File")]
        [DirectDomMethod("Write {Binary} to {File Path}")]
        [MethodDescription("Take a binary string and create a file from it")]
        public static bool BinaryToFile(string binarystring, string filepath)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Create);
                // Create the writer for data.
                BinaryWriter bw = new BinaryWriter(fs);

                bw.Write(binarystring);

                fs.Close();
                bw.Close();
                return true;
            }
            catch (Exception e)
            {
                logArchitect.Error("Direct.Blob.Library - Convert Binary to File Exception", e);
                return false;
            }
        }

        [DirectDom("Convert Blob to File")]
        [DirectDomMethod("Write {Blob} to {File Path}")]
        [MethodDescription("Take a Blob encoded string and create a file from it")]
        public static bool BlobToFile(string Blobstring, string filepath)
        {
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.Debug("Direct.Blob.Library - Start Converting to file from Blob: " + Blobstring);
                }
                byte[] tempBytes = Convert.FromBase64String(Blobstring);
                System.IO.File.WriteAllBytes(filepath, tempBytes);
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.Debug("Direct.Blob.Library - Completed converting Blob to filepath: " + filepath);
                }
                return true;
            }
            catch (Exception e)
            {
                logArchitect.Error("Direct.Blob.Library - Convert Blob to File Exception", e);
                return false;
            }
        }

        [DirectDom("Convert File to Blob")]
        [DirectDomMethod("Read Blob from {File Path}")]
        [MethodDescription("Read a file and return Blob string")]
        public static string FileToBlob(string filepath)
        {
            try
            {
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.Debug("Direct.Blob.Library - Start Converting to Blob from file: " + filepath);
                }
                byte[] AsBytes = File.ReadAllBytes(filepath);
                String AsBase64String = Convert.ToBase64String(AsBytes);
                if (logArchitect.IsDebugEnabled)
                {
                    logArchitect.Debug("Direct.Blob.Library - Completed converting filepath to Blob: " + AsBase64String);
                }
                return AsBase64String;
            }
            catch (Exception e)
            {
                logArchitect.Error("Direct.Blob.Library - Convert File to Blob Exception", e);
                return null;
            }
        }

    }
}
