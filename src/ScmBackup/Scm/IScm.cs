﻿using System;

namespace ScmBackup.Scm
{
    internal interface IScm
    {
        /// <summary>
        /// Short name of the SCM (used to find settings in the config)
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// "Pretty" name for displaying
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Checks whether the SCM is present on this computer
        /// </summary>
        bool IsOnThisComputer();

        /// <summary>
        /// Gets the SCM's version number.
        /// Should throw exceptions if the version number can't be determined.
        /// </summary>
        string GetVersionNumber();

        /// <summary>
        /// Checks whether the git LFS is present on this computer
        /// </summary>
        bool LFSIsOnThisComputer();

        /// <summary>
        /// Checks whether the given directory is a repository
        /// </summary>
        bool DirectoryIsRepository(string directory);

        /// <summary>
        /// Creates a repository in the given directory
        /// </summary>
        void CreateRepository(string directory);

        /// <summary>
        /// Checks whether a repository exists under the given URL
        /// </summary>
        bool RemoteRepositoryExists(string remoteUrl);

        /// <summary>
        /// Checks whether a repository exists under the given URL
        /// </summary>
        bool RemoteRepositoryExists(string remoteUrl, ScmCredentials credentials);

        /// <summary>
        /// Pulls from a remote repository into a local folder.
        /// If the folder doesn't exist or is not a repository, it's created first.
        /// </summary>
        void PullFromRemote(string remoteUrl, string directory);
        
        /// <summary>
        /// Pulls from a remote repository into a local folder.
        /// If the folder doesn't exist or is not a repository, it's created first.
        /// </summary>
        void PullFromRemote(string remoteUrl, string directory, ScmCredentials credentials);

        /// <summary>
        /// Pulls all LFS files from a remote repository into a local folder.
        /// </summary>
        void PullLFSFromRemote(string remoteUrl, string directory, ScmCredentials credentials);

        /// <summary>
        /// Checks whether the repo in this directory contains LFS files
        /// </summary>
        bool RepositoryContainsLFS(string directory);

        /// <summary>
        /// Checks whether the repo in this directory contains a commit with this ID
        /// </summary>
        bool RepositoryContainsCommit(string directory, string commitid);

    }
}
