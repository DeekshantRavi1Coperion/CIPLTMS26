using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CollectFiles
/// </summary>
public static class CollectFiles
{
    //public CollectFiles()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}



    public static void GetLocalFiles(string voucherNo)
    {
        string folderPath = @"C:\VOUCHER_AUTH_DOCS\" + voucherNo;

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            //Console.WriteLine($"Files in {folderPath}:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }
        else
        {
            //Console.WriteLine($"Folder does not exist: {folderPath}");
        }

    }

    public static List<FileDetails> GetLocalFiles(string drive, string voucherNo)
    {
        List<FileDetails> filesList = new List<FileDetails>();

        string basePath = drive + @":\VOUCHER_AUTH_DOCS";
        string folderPath = Path.Combine(basePath, voucherNo);

        if (Directory.Exists(folderPath))
        {
            var files = Directory.GetFiles(folderPath)
                .Select(f => new
                {
                    FileName = Path.GetFileName(f),
                    FullPath = f
                }).ToList();

            foreach (var item in files)
            {
                FileDetails singleFile = new FileDetails();
                singleFile.FileName = item.FileName;
                singleFile.FullPath = item.FullPath;

                filesList.Add(singleFile);
            }

            //rptFiles.DataSource = files;
            //rptFiles.DataBind();
        }

        return filesList;

    }


    public static DataTable GetLocalFiles(int currentUserId, int typeId, DataTable dtDetails)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("SR_NO", typeof(int));
        dtFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtFiles.Columns.Add("TYPE_ID", typeof(int));
        dtFiles.Columns.Add("FILE_PATH", typeof(string));
        dtFiles.Columns.Add("FILE_NAME", typeof(string));
        dtFiles.Columns.Add("FILE_BYTES", typeof(byte[]));


        DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);

        // Get base path
        string directoryPath = Convert.ToString(dsPath.Tables[0].Rows[0]["DIRECTORY_PATH"]);

        foreach (DataRow row in dtDetails.Rows)
        {
            string voucherNumber = Convert.ToString(row["VOUCHER_NO"]); // Adjust column name as per your DataTable
            string voucherNoFormatted = voucherNumber.Replace("/", "").Replace("-", "");

            string folderPath = Path.Combine(directoryPath, voucherNoFormatted);

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath)
                    .Select(f => new
                    {
                        FileName = Path.GetFileName(f),
                        FullPath = f
                    }).ToList();

                int count = 0;
                foreach (var item in files)
                {
                    count++;
                    byte[] fileBytes = File.ReadAllBytes(item.FullPath);

                    DataRow dr = dtFiles.NewRow();

                    dr["SR_NO"] = count;
                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["TYPE_ID"] = typeId;
                    dr["FILE_PATH"] = item.FullPath;
                    dr["FILE_NAME"] = item.FileName;
                    dr["FILE_BYTES"] = fileBytes;

                    dtFiles.Rows.Add(dr);
                }
            }
        }

        return dtFiles;
    }

    public static List<DataTable> GetLocalFilesList_OLD(int currentUserId, int typeId, DataTable dtDetails, string filePath)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        List<DataTable> dtFilesList = new List<DataTable>();

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("SR_NO", typeof(int));
        dtFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtFiles.Columns.Add("TYPE_ID", typeof(int));
        dtFiles.Columns.Add("FILE_PATH", typeof(string));
        dtFiles.Columns.Add("FILE_NAME", typeof(string));
        dtFiles.Columns.Add("FILE_BYTES", typeof(byte[]));

        DataTable dtFileVouchers = new DataTable();
        dtFileVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtFileVouchers.Columns.Add("COUNT", typeof(int));

        string directoryPath = string.Empty;

        if (!string.IsNullOrEmpty(filePath))
        {
            directoryPath = filePath;
        }
        else
        {
            DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);
            // Get base path
            directoryPath = Convert.ToString(dsPath.Tables[0].Rows[0]["DIRECTORY_PATH"]);
        }

        foreach (DataRow row in dtDetails.Rows)
        {
            string voucherNumber = Convert.ToString(row["VOUCHER_NO"]); // Adjust column name as per your DataTable
            string voucherNoFormatted = voucherNumber.Replace("/", "").Replace("-", "").Replace(".", "");

            string folderPath = Path.Combine(directoryPath, voucherNoFormatted);

            if (Directory.Exists(folderPath))
            {
                var files = Directory.GetFiles(folderPath)
                    .Select(f => new
                    {
                        FileName = Path.GetFileName(f),
                        FullPath = f
                    }).ToList();

                int count = 0;
                foreach (var item in files)
                {
                    count++;
                    byte[] fileBytes = File.ReadAllBytes(item.FullPath);

                    DataRow dr = dtFiles.NewRow();

                    dr["SR_NO"] = count;
                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["TYPE_ID"] = typeId;
                    dr["FILE_PATH"] = item.FullPath;
                    dr["FILE_NAME"] = item.FileName;
                    dr["FILE_BYTES"] = fileBytes;

                    dtFiles.Rows.Add(dr);
                }

                if (files.Count > 0)
                {
                    DataRow dr = dtFileVouchers.NewRow();

                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["COUNT"] = count;

                    dtFileVouchers.Rows.Add(dr);
                }

                dtFilesList.Add(dtFiles);
                dtFilesList.Add(dtFileVouchers);
            }
        }


        return dtFilesList;
    }

    public static List<DataTable> GetLocalFilesList(int currentUserId, int typeId, DataTable dtDetails, string filePath)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        List<DataTable> dtFilesList = new List<DataTable>();

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("SR_NO", typeof(int));
        dtFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtFiles.Columns.Add("TYPE_ID", typeof(int));
        dtFiles.Columns.Add("FILE_PATH", typeof(string));
        dtFiles.Columns.Add("FILE_NAME", typeof(string));
        dtFiles.Columns.Add("FILE_BYTES", typeof(byte[]));

        DataTable dtFileVouchers = new DataTable();
        dtFileVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtFileVouchers.Columns.Add("COUNT", typeof(int));

        string directoryPath = string.Empty;

        if (!string.IsNullOrEmpty(filePath))
        {
            directoryPath = filePath;
        }
        else
        {
            DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);
            if (dsPath.Tables.Count > 0 && dsPath.Tables[0].Rows.Count > 0)
            {
                directoryPath = Convert.ToString(dsPath.Tables[0].Rows[0]["DIRECTORY_PATH"]);
            }
        }

        foreach (DataRow row in dtDetails.Rows)
        {
            string voucherNumber = Convert.ToString(row["VOUCHER_NO"]);
            if (string.IsNullOrEmpty(voucherNumber))
                continue;

            string voucherNoFormatted = voucherNumber.Replace("/", "").Replace("-", "").Replace(".", "").Trim();

            int count = 0;

            // -----------------------
            // 1. Check for folder
            // -----------------------
            string matchingFolder = Directory.GetDirectories(directoryPath)
                .FirstOrDefault(dir => Path.GetFileName(dir).StartsWith(voucherNoFormatted, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(matchingFolder))
            {
                count = 0;

                var filesInFolder = Directory.GetFiles(matchingFolder);
                foreach (var file in filesInFolder)
                {
                    count++;
                    byte[] fileBytes = File.ReadAllBytes(file);
                    DataRow dr = dtFiles.NewRow();
                    dr["SR_NO"] = count;
                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["TYPE_ID"] = typeId;
                    dr["FILE_PATH"] = file;
                    dr["FILE_NAME"] = Path.GetFileName(file);
                    dr["FILE_BYTES"] = fileBytes;
                    dtFiles.Rows.Add(dr);
                }

                if (count > 0)
                {
                    DataRow drCount = dtFileVouchers.NewRow();
                    drCount["VOUCHER_NO"] = voucherNumber;
                    drCount["COUNT"] = count;
                    dtFileVouchers.Rows.Add(drCount);
                }
            }

            //else
            //{
            //count = 0;

            // -----------------------
            // 2. Check for file directly
            // -----------------------
            var matchingFiles = Directory.GetFiles(directoryPath)
                .Where(f => Path.GetFileNameWithoutExtension(f)
                    .StartsWith(voucherNoFormatted, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var file in matchingFiles)
            {
                count++;
                byte[] fileBytes = File.ReadAllBytes(file);
                DataRow dr = dtFiles.NewRow();
                dr["SR_NO"] = count;
                dr["VOUCHER_NO"] = voucherNumber;
                dr["TYPE_ID"] = typeId;
                dr["FILE_PATH"] = file;
                dr["FILE_NAME"] = Path.GetFileName(file);
                dr["FILE_BYTES"] = fileBytes;
                dtFiles.Rows.Add(dr);
            }

            if (count > 0)
            {
                DataRow drCount = dtFileVouchers.NewRow();
                drCount["VOUCHER_NO"] = voucherNumber;
                drCount["COUNT"] = count;
                dtFileVouchers.Rows.Add(drCount);
            }
            //}


        }

        dtFilesList.Add(dtFiles);
        dtFilesList.Add(dtFileVouchers);

        return dtFilesList;
    }

    public static List<DataTable> GetLocalFilesListMatchingFile(int currentUserId, int typeId, DataTable dtDetails, string filePath)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        List<DataTable> dtFilesList = new List<DataTable>();

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("SR_NO", typeof(int));
        dtFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtFiles.Columns.Add("TYPE_ID", typeof(int));
        dtFiles.Columns.Add("FILE_PATH", typeof(string));
        dtFiles.Columns.Add("FILE_NAME", typeof(string));
        dtFiles.Columns.Add("FILE_BYTES", typeof(byte[]));

        DataTable dtFileVouchers = new DataTable();
        dtFileVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtFileVouchers.Columns.Add("COUNT", typeof(int));

        string directoryPath = string.Empty;

        if (!string.IsNullOrEmpty(filePath))
        {
            directoryPath = filePath;
        }
        else
        {
            DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);
            // Get base path
            directoryPath = Convert.ToString(dsPath.Tables[0].Rows[0]["DIRECTORY_PATH"]);
        }

        foreach (DataRow row in dtDetails.Rows)
        {
            string voucherNumber = Convert.ToString(row["VOUCHER_NO"]); // Adjust column name as per your DataTable
            string voucherNoFormatted = voucherNumber.Replace("/", "").Replace("-", "").Replace(".", "");

            var matchingFiles = Directory.GetFiles(directoryPath)
                                .Where(f => Path.GetFileNameWithoutExtension(f)
                                .StartsWith(voucherNoFormatted, StringComparison.OrdinalIgnoreCase)).ToList();

            //if (Directory.Exists(folderPath))
            //{
            //var files = Directory.GetFiles(folderPath)
            //    .Select(f => new
            //    {
            //        FileName = Path.GetFileName(f),
            //        FullPath = f
            //    }).ToList();

            if (matchingFiles.Count > 0)
            {
                int count = 0;
                foreach (var file in matchingFiles)
                {
                    count++;
                    byte[] fileBytes = File.ReadAllBytes(file);

                    DataRow dr = dtFiles.NewRow();
                    dr["SR_NO"] = count;
                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["TYPE_ID"] = typeId;
                    dr["FILE_PATH"] = file;
                    dr["FILE_NAME"] = Path.GetFileName(file);
                    dr["FILE_BYTES"] = fileBytes;
                    dtFiles.Rows.Add(dr);
                }

                if (count > 0)
                {
                    DataRow dr = dtFileVouchers.NewRow();

                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["COUNT"] = count;

                    dtFileVouchers.Rows.Add(dr);
                }

                dtFilesList.Add(dtFiles);
                dtFilesList.Add(dtFileVouchers);
            }

            //}
        }

        return dtFilesList;
    }

    public static List<DataTable> GetLocalFilesListMatchingPath(int currentUserId, int typeId, DataTable dtDetails, string filePath)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        List<DataTable> dtFilesList = new List<DataTable>();

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("SR_NO", typeof(int));
        dtFiles.Columns.Add("VOUCHER_NO", typeof(string));
        dtFiles.Columns.Add("TYPE_ID", typeof(int));
        dtFiles.Columns.Add("FILE_PATH", typeof(string));
        dtFiles.Columns.Add("FILE_NAME", typeof(string));
        dtFiles.Columns.Add("FILE_BYTES", typeof(byte[]));

        DataTable dtFileVouchers = new DataTable();
        dtFileVouchers.Columns.Add("VOUCHER_NO", typeof(string));
        dtFileVouchers.Columns.Add("COUNT", typeof(int));

        string directoryPath = string.Empty;

        if (!string.IsNullOrEmpty(filePath))
        {
            directoryPath = filePath;
        }
        else
        {
            DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);
            // Get base path
            directoryPath = Convert.ToString(dsPath.Tables[0].Rows[0]["DIRECTORY_PATH"]);
        }

        foreach (DataRow row in dtDetails.Rows)
        {
            string voucherNumber = Convert.ToString(row["VOUCHER_NO"]); // Adjust column name as per your DataTable
            string voucherNoFormatted = voucherNumber.Replace("/", "").Replace("-", "").Replace(".", "");

            //string folderPath = Path.Combine(directoryPath, voucherNoFormatted);

            string matchingFolder = Directory.GetDirectories(directoryPath)
                                    .FirstOrDefault(dir => Path.GetFileName(dir)
                                    .StartsWith(voucherNoFormatted, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(matchingFolder) && Directory.Exists(matchingFolder))
            {
                var files = Directory.GetFiles(matchingFolder)
                   .Select(f => new
                   {
                       FileName = Path.GetFileName(f),
                       FullPath = f
                   }).ToList();

                int count = 0;
                foreach (var item in files)
                {
                    count++;
                    byte[] fileBytes = File.ReadAllBytes(item.FullPath);

                    DataRow dr = dtFiles.NewRow();

                    dr["SR_NO"] = count;
                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["TYPE_ID"] = typeId;
                    dr["FILE_PATH"] = item.FullPath;
                    dr["FILE_NAME"] = item.FileName;
                    dr["FILE_BYTES"] = fileBytes;

                    dtFiles.Rows.Add(dr);
                }

                if (files.Count > 0)
                {
                    DataRow dr = dtFileVouchers.NewRow();

                    dr["VOUCHER_NO"] = voucherNumber;
                    dr["COUNT"] = count;

                    dtFileVouchers.Rows.Add(dr);
                }

                dtFilesList.Add(dtFiles);
                dtFilesList.Add(dtFileVouchers);
            }
        }


        return dtFilesList;
    }

    public static DataTable GetFilePath(int currentUserId, int typeId)
    {
        BAL.VouchersAuthorization objVouchersAuthorization = new BAL.VouchersAuthorization();

        DataSet dsPath = objVouchersAuthorization.BindUserDirectoryPath(currentUserId, typeId);

        if (dsPath.Tables.Count > 0 && dsPath.Tables[0].Rows.Count > 0)
        {
            return dsPath.Tables[0];
        }
        return null;

    }

    public static DataTable GetVoucherCounts(DataTable dtfiles)
    {
        DataView view = new DataView(dtfiles);
        DataTable dtDistinctVoucherNo = view.ToTable(true, "VOUCHER_NO"); // 'true' = distinct


        DataTable dtVoucherCounts = new DataTable();
        dtVoucherCounts.Columns.Add("VOUCHER_NO", typeof(string));
        dtVoucherCounts.Columns.Add("COUNT", typeof(int));

        var voucherGroups = dtfiles.AsEnumerable()
        .GroupBy(row => row.Field<string>("VOUCHER_NO"));

        foreach (var group in voucherGroups)
        {
            DataRow newRow = dtVoucherCounts.NewRow();
            newRow["VOUCHER_NO"] = group.Key;
            newRow["COUNT"] = group.Count();
            dtVoucherCounts.Rows.Add(newRow);
        }

        return dtVoucherCounts;
    }
}