using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace Usb_Flash_Detector
{
    class clsUsbFlashDetector
    {
        #region Public Vars
        public DoWorkEventHandler DoWorkEvent;
        public string key = "masterkey";
        public string file = "itsme.ffl";
        #endregion

        #region Private Vars
        BackgroundWorker backgroundWorker1 = new BackgroundWorker();
        #endregion

        #region Public Methods

        public void start()
        {
            backgroundWorker1.DoWork += DoWorkEvent;
            backgroundWorker1.RunWorkerAsync();
        }

        public bool verifyDriver()
        {
            foreach (System.Management.ManagementObject drive in
                new System.Management.ManagementObjectSearcher(
                    "select DeviceID, Model, PNPDeviceID from Win32_DiskDrive " +
                     "where InterfaceType='USB'").Get())
            {
                System.Management.ManagementObject partition = new System.Management.ManagementObjectSearcher(String.Format(
                    "associators of {{Win32_DiskDrive.DeviceID='{0}'}} " +
                          "where AssocClass = Win32_DiskDriveToDiskPartition",
                    drive["DeviceID"])).First();

                if (partition != null)
                {
                    System.Management.ManagementObject logical = new System.Management.ManagementObjectSearcher(String.Format(
                        "associators of {{Win32_DiskPartition.DeviceID='{0}'}} " +
                            "where AssocClass= Win32_LogicalDiskToPartition",
                        partition["DeviceID"])).First();


                    string DriveLetter = logical["Name"].ToString();
                    if (System.IO.File.Exists(DriveLetter + "\\" + file))
                    {
                        if (DriveLetter != "")
                        {

                            try
                            {
                                string serialAtual = EncryptSerial(parseSerialFromDeviceID(drive["PNPDeviceID"].ToString()));
                                string serial = System.IO.File.ReadAllText(DriveLetter + "\\" + file);
                                serial = serial.Substring(125, serial.Length - 125);

                                if (serial == serialAtual)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;

                                }

                            }
                            catch { return false; }
                        }
                    }

                }
            }
            return false;

        }

        public string GetDriverSerial()
        {
            string result = "";
            foreach (System.Management.ManagementObject drive in
                new System.Management.ManagementObjectSearcher(
                    "select DeviceID, Model, PNPDeviceID from Win32_DiskDrive " +
                     "where InterfaceType='USB'").Get())
            {
                System.Management.ManagementObject partition = new System.Management.ManagementObjectSearcher(String.Format(
                    "associators of {{Win32_DiskDrive.DeviceID='{0}'}} " +
                          "where AssocClass = Win32_DiskDriveToDiskPartition",
                    drive["DeviceID"])).First();

                if (partition != null)
                {
                    System.Management.ManagementObject logical = new System.Management.ManagementObjectSearcher(String.Format(
                        "associators of {{Win32_DiskPartition.DeviceID='{0}'}} " +
                            "where AssocClass= Win32_LogicalDiskToPartition",
                        partition["DeviceID"])).First();


                    string DriveLetter = logical["Name"].ToString();

                        if (DriveLetter != "")
                        {

                            try
                            {
                                string serialAtual = parseSerialFromDeviceID(drive["PNPDeviceID"].ToString());

                                result = serialAtual;

                            }
                            catch { result = "none"; }
                        }
                    

                }
            }
            return result;

        }

        public string GetDriverKey()
        {
            string result = "";
            foreach (System.Management.ManagementObject drive in
                new System.Management.ManagementObjectSearcher(
                    "select DeviceID, Model, PNPDeviceID from Win32_DiskDrive " +
                     "where InterfaceType='USB'").Get())
            {
                System.Management.ManagementObject partition = new System.Management.ManagementObjectSearcher(String.Format(
                    "associators of {{Win32_DiskDrive.DeviceID='{0}'}} " +
                          "where AssocClass = Win32_DiskDriveToDiskPartition",
                    drive["DeviceID"])).First();

                if (partition != null)
                {
                    System.Management.ManagementObject logical = new System.Management.ManagementObjectSearcher(String.Format(
                        "associators of {{Win32_DiskPartition.DeviceID='{0}'}} " +
                            "where AssocClass= Win32_LogicalDiskToPartition",
                        partition["DeviceID"])).First();


                    string DriveLetter = logical["Name"].ToString();

                    if (DriveLetter != "")
                    {

                        try
                        {
                            string serialAtual = EncryptSerial(parseSerialFromDeviceID(drive["PNPDeviceID"].ToString()));

                            result = serialAtual;

                        }
                        catch { result = "none"; }
                    }


                }
            }
            return result;

        }

        public string GetDrivers()
        {
            string result = "";

            foreach (System.Management.ManagementObject drive in
                new System.Management.ManagementObjectSearcher(
                    "select DeviceID, Model, PNPDeviceID from Win32_DiskDrive " +
                     "where InterfaceType='USB'").Get())
            {
                System.Management.ManagementObject partition = new System.Management.ManagementObjectSearcher(String.Format(
                    "associators of {{Win32_DiskDrive.DeviceID='{0}'}} " +
                          "where AssocClass = Win32_DiskDriveToDiskPartition",
                    drive["DeviceID"])).First();

                if (partition != null)
                {
                    System.Management.ManagementObject logical = new System.Management.ManagementObjectSearcher(String.Format(
                        "associators of {{Win32_DiskPartition.DeviceID='{0}'}} " +
                            "where AssocClass= Win32_LogicalDiskToPartition",
                        partition["DeviceID"])).First();


                    result = logical["Name"].ToString();
                }
            }
            return result;
        }
        #endregion

        #region private Methods
        private string parseSerialFromDeviceID(string deviceId)
        {
            string[] splitDeviceId = deviceId.Split('\\');
            string[] serialArray;
            string serial;
            int arrayLen = splitDeviceId.Length - 1;

            serialArray = splitDeviceId[arrayLen].Split('&');
            serial = serialArray[0];

            return serial;
        }
        
        private string EncryptSerial(string serial)
        {

            string result = "S";
            int x = 0;
            int y = 0;
            foreach (char ch in serial)
            {
                result += (char)(ch ^ result[x]);
                result += (char)(ch + key[y]);
                if (y >= key.Length - 1)
                    y = 0;
                x++; y++;
            }
            return result;
        }

        #endregion
    }
}

public static class Extend
{

    public static System.Management.ManagementObject First(this System.Management.ManagementObjectSearcher searcher)
    {
        System.Management.ManagementObject result = null;
        foreach (System.Management.ManagementObject item in searcher.Get())
        {
            result = item;
            break;
        }
        return result;
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class
         | AttributeTargets.Method)]
    public sealed class ExtensionAttribute : Attribute { }
}
