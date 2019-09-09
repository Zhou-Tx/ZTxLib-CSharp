using System.IO;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;

namespace ZTxLib.Zip
{
    public static class Tgz
    {
        //public static bool Unzip(string tgzFile)
        //{
        //    string fileName = Path.GetFileName(tgzFile);
        //}

        public static bool UnzipTgz(string tgzPath, string folder)
        {
            Stream inStream = null;
            Stream gzipStream = null;
            TarArchive tarArchive = null;
            try
            {
                using (inStream = File.OpenRead(tgzPath))
                {
                    using (gzipStream = new GZipInputStream(inStream))
                    {
                        tarArchive = TarArchive.CreateInputTarArchive(gzipStream);
                        tarArchive.ExtractContents(folder);
                        tarArchive.Close();
                    }
                }
                return true;
            }
            catch //(Exception ex)
            {
                //LogManager.GetCurrentClassLogger().Error(ex);
                return false;
            }
            finally
            {
                if (tarArchive != null) tarArchive.Close();
                if (gzipStream != null) gzipStream.Close();
                if (inStream != null) inStream.Close();
            }
        }
    }
}
