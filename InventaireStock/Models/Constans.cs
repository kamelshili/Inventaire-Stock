﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventaireStock.Models
{
    public static class Constants
    {
        public const string DatabaseFilename = "CEDBSQLite.db";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;
        public const string pathFolder = @"/sdcard/Android/data/com.tunitrack.inventairestock/setting.txt";
        public const string DirectoryImport = @"/sdcard/Android/data/com.tunitrack.inventairestock/Import";
        public const string DirectoryExport = @"/sdcard/Android/data/com.tunitrack.inventairestock/Export";

        public const string pathExcelImport = DirectoryImport + @"/Inventaire.csv";
        public const string pathExcelExport = DirectoryExport + @"/Inventaire.csv";

        public static string DatabasePath()
        {
             //var path = @"/sdcard/Android/data/com.tunitrack.inventairestock/dataInventoryLight.db";
            var path = @"/sdcard/Android/data/com.tunitrack.inventairestock/dataInventoryStock.db";

            if (File.Exists(path) == true)
            {

                return path;
            }
            else
            {
                return path;
            }
        }


    }
}

