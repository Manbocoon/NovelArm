using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelArm.Modules.Systems
{
    public class FileLock : IDisposable
    {
        #region Properties

        private FileStream Lock;
        public bool IsLocked { get; set; }
        #endregion

        public void Dispose()
        {
            if (Lock != null)
                Lock.Dispose();
        }


        public FileLock(string path)
        {
            if (File.Exists(path))
            {
                Lock = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                IsLocked = true;
            }
        }

    }
}
