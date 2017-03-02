﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Launcher
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == buttonInstall)
                OnInstall();
            else if (sender == buttonUninstall)
                OnUninstall();
            else if (sender == buttonBackup)
                OnBackupSaves();
            else if (sender == buttonRestore)
                OnRestoreSaves();
            else if (sender == buttonInstallMod)
                OnInstallMod();
            else if (sender == buttonUninstallMod)
                OnUninstallMod();
        }

        public static string AppData
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void OnInstall()
        {
            /*WebClient webClient = new WebClient();
            webClient.DownloadFile()
            JsonConvert*/
        }

        private void OnUninstall()
        {

        }

        private void OnBackupSaves()
        {
            if (MessageBox.Show("Are you sure you want to replace the old backup ?", "Backup Saves", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            string src = System.IO.Path.Combine(AppData, "Secrets of Grindea");
            if (!Directory.Exists(src))
            {
                MessageBox.Show(string.Format("{0} doesn't exist", src), "Backup Saves");
                return;
            }
            string dst = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "BackupSaves");
            if (Directory.Exists(dst))
                Directory.Delete(dst, true);
            Copy(src, dst);
            MessageBox.Show("Done", "Backup Saves");
        }

        private void OnRestoreSaves()
        {
            if (MessageBox.Show("Are you sure you want to replace saves by the existing backup ?", "Restore Saves", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;
            string src = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "BackupSaves");
            if (!Directory.Exists(src))
            {
                MessageBox.Show(string.Format("{0} doesn't exist", src), "Restore Saves");
                return;
            }
            string dst = System.IO.Path.Combine(AppData, "Secrets of Grindea");
            if (Directory.Exists(dst))
                Directory.Delete(dst, true);
            Copy(src, dst);
            MessageBox.Show("Done", "Restore Saves");
        }

        private void OnInstallMod()
        {

        }

        private void OnUninstallMod()
        {

        }
    }
}
