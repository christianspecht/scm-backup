using ScmBackup.Configuration;
using ScmBackup.Hosters;
using ScmBackup.Http;
using System.Collections.Generic;
using System.IO;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;

namespace ScmBackup
{
    /// <summary>
    /// Backs up all repositories from a single source
    /// </summary>
    internal class BackupMaker : IBackupMaker
    {
        private readonly ILogger logger;
        private readonly IFileSystemHelper fileHelper;
        private readonly IHosterBackupMaker backupMaker;
        private readonly IContext context;

        public BackupMaker(ILogger logger, IFileSystemHelper fileHelper, IHosterBackupMaker backupMaker, IContext context)
        {
            this.logger = logger;
            this.fileHelper = fileHelper;
            this.backupMaker = backupMaker;
            this.context = context;
        }

        public string Backup(ConfigSource source, IEnumerable<HosterRepository> repos)
        {
            this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Source, source.Title);

            string sourceFolder = this.fileHelper.CreateSubDirectory(context.Config.LocalFolder, source.Title);

            var url = new UrlHelper();

            foreach (var repo in repos)
            {
                string repoFolder = this.fileHelper.CreateSubDirectory(sourceFolder, repo.FullName);

                this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_Repo, repo.Scm.ToString(), url.RemoveCredentialsFromUrl(repo.CloneUrl));

                this.backupMaker.MakeBackup(source, repo, repoFolder);

                if (this.context.Config.Options.Backup.LogRepoFinished)
                {
                    this.logger.Log(ErrorLevel.Info, Resource.BackupMaker_RepoFinished);
                }
            }

            // Upload backup folder to S3 if bucket name is specified
            if (!string.IsNullOrEmpty(context.Config.S3BucketName))
            {
                UploadToS3(sourceFolder, context.Config.S3BucketName);
            }

            return sourceFolder;
        }

        private void UploadToS3(string sourceFolderPath, string bucketName)
        {
            try
            {
                this.logger.Log(ErrorLevel.Info, "Starting upload to S3 bucket: {0}", bucketName);
                
                using (var s3Client = new AmazonS3Client())
                {
                    var transferUtility = new TransferUtility(s3Client);
                    var directoryTransferUtility = new TransferUtilityUploadDirectoryRequest
                    {
                        BucketName = bucketName,
                        Directory = sourceFolderPath,
                        SearchPattern = "*.*",
                        SearchOption = SearchOption.AllDirectories,
                        KeyPrefix = Path.GetFileName(sourceFolderPath)
                    };

                    transferUtility.UploadDirectory(directoryTransferUtility);
                }
                
                this.logger.Log(ErrorLevel.Info, "Successfully uploaded backup to S3 bucket: {0}", bucketName);
            }
            catch (Exception ex)
            {
                this.logger.Log(ErrorLevel.Error, "Failed to upload to S3: {0}", ex.Message);
            }
        }
    }
}
