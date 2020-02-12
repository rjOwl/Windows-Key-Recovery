using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//https://www.c-sharpcorner.com/UploadFile/mahesh/create-a-text-file-in-C-Sharp/

namespace Product_Key_Getter
{
    class ValidationsClass
    {
        public int CREATED = 0, EXISTS=1, FAIL=-1;
        public int CheckRun()
        {
            string fileName = @"C:\Users\Public\System.log";
            if (File.Exists(fileName))
                return EXISTS;
            try{
                File.Create(fileName);
                return CREATED;
            }
            catch(Exception ex){
                MessageBox.Show(ex.ToString());
                return FAIL;
            }
        }
    }
}
