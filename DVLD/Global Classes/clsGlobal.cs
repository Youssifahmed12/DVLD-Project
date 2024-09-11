using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;
        private const string registryPath = @"SOFTWARE\DVLD\Credentials";
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
               
                RegistryKey key = Registry.CurrentUser.CreateSubKey(registryPath);

                
                if (string.IsNullOrEmpty(Username))
                {
                    Registry.CurrentUser.DeleteSubKey(registryPath, false);
                    return true;
                }

                
                key.SetValue("Username", Username);
                key.SetValue("Password", Password);

                key.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }

       
        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                
                RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath);

                
                if (key != null)
                {
                   
                    Username = key.GetValue("Username")?.ToString() ?? "";
                    Password = key.GetValue("Password")?.ToString() ?? "";

                    key.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
