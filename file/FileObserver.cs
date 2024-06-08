using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iterate.file
{
    public class FileObserver
    {
        public string path;
        public bool active = true;
        public FileSystemWatcher watcher;
        private IterateApplicationContext context;
        public FileObserver(IterateApplicationContext context, string path) {
            this.path = path;
            this.context = context;
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
            path = e.FullPath;
            watcher.Filter = e.Name;
            context.projectManager.UpdateFilePath(context.projectManager.CurrentProject.Id, path);
        }
    }
}
