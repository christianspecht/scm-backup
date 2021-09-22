using System;
using System.Collections.Generic;
using System.Text;

namespace ScmBackup.Tests
{
    class FakeFileSystemHelper : IFileSystemHelper
    {
        public List<string> SubDirectoryNames { get; set; } = new List<string>();

        public string CreateSubDirectory(string mainDir, string subDir)
        {
            throw new NotImplementedException();
        }

        public bool DirectoryIsEmpty(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSubDirectoryNames(string path)
        {
            return this.SubDirectoryNames;
        }

        public string PathCombine(string path1, string path2)
        {
            throw new NotImplementedException();
        }
    }
}
