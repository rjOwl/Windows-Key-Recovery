using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://stackoverflow.com/questions/4660142/what-is-a-nullreferenceexception-and-how-do-i-fix-it
//https://stackoverflow.com/questions/26299421/cant-read-registry-key
//https://stackoverflow.com/questions/55905010/firesharp-an-error-occured-while-execute-request-path-0-method-put
//http://www.mrpear.net/en/blog/1207/how-to-get-windows-product-key-from-digitalproductid-exported-out-of-registry
//https://github.com/mrpeardotnet/WinProdKeyFinder/blob/master/WinProdKeyFind/KeyDecoder.cs
//https://stackoverflow.com/questions/14668091/read-registry-binary-and-convert-to-string

// office key
//https://www.recoverlostpassword.com/article/product-key/find-office-2007-product-key.html
//https://www.techwalla.com/articles/how-to-remove-microsoft-office-from-the-registry


// VB script to get the key
//https://www.cocosenor.com/articles/product-key/check-windows-10-product-key-on-computer.html
namespace Product_Key_Getter{
    public class regClass{
        public string[] Initial(){
            RegistryKey thisKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey thisSubkey = thisKey.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
            String ProductName = (string)thisSubkey.GetValue("ProductName");
            String ProductId = (string)thisSubkey.GetValue("ProductId");
            //("Computer\\HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");

            var ProductDigitalId = (byte[])thisSubkey.GetValue("DigitalProductId");
            String strComputerName = Environment.MachineName.ToString();
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            String timeStamp = GetTimestamp(DateTime.Now);

            String license = DecodeProductKeyWin8AndUp(ProductDigitalId);
            return new string []{ ProductName, license, ProductId, strComputerName+": "+ userName +": "+ timeStamp };
        }

        private static String GetTimestamp(DateTime value){
            return value.ToString("yyyy-MM-dd  HH-mm-ss-ffff");
        }

        private static string DecodeProductKeyWin8AndUp(byte[] digitalProductId){
            var key = String.Empty;
            const int keyOffset = 52;
            var isWin8 = (byte)((digitalProductId[66] / 6) & 1);
            digitalProductId[66] = (byte)((digitalProductId[66] & 0xf7) | (isWin8 & 2) * 4);

            const string digits = "BCDFGHJKMPQRTVWXY2346789";
            var last = 0;
            for (var i = 24; i >= 0; i--){
                var current = 0;
                for (var j = 14; j >= 0; j--){
                    current = current * 256;
                    current = digitalProductId[j + keyOffset] + current;
                    digitalProductId[j + keyOffset] = (byte)(current / 24);
                    current = current % 24;
                    last = current;
                }
                key = digits[current] + key;
            }

            var keypart1 = key.Substring(1, last);
            var keypart2 = key.Substring(last + 1, key.Length - (last + 1));
            key = keypart1 + "N" + keypart2;

            for (var i = 5; i < key.Length; i += 6){
                key = key.Insert(i, "-");
            }
            return key;
        }


        private void product()
        {
            try{
                RegistryKey thisKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey thisSubkey = thisKey.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", false);
                Console.WriteLine((string)thisSubkey.GetValue("ProductId"));

                //("Computer\\HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion")){
                    if (key != null){
                        Object o = key.GetValue("ProductName");
                        Object ok = key.GetValue("ProductId");
                        if (o != null){
                            Console.WriteLine(o.ToString());
                        }
                        if (ok != null){
                            Console.WriteLine(ok.ToString());
                        }
                        else
                            Console.WriteLine("Cant get the");
                    }
                }
            }
            catch (Exception ex){  //just for demonstration...it's always best to handle specific exceptions
                Console.WriteLine(ex.ToString());
            }
        }
    }
}


