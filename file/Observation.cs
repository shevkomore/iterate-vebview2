using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterate.file
{
    public class Observation
    {
        public string path;
        public bool active = true;
        public FileSystemWatcher watcher;
        public Observation(string path) {
            this.path = path;
            if (!File.Exists(path))
            {
                active = false;
            }
            watcher = new FileSystemWatcher(Path.GetDirectoryName(path), Path.GetFileName(path));
            watcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;
            watcher.Renamed += onRenamed;
            watcher.EnableRaisingEvents = true;
        }

        private void onRenamed(object sender, RenamedEventArgs e)
        {
            watcher.Filter = e.Name;
        }
    }
}
