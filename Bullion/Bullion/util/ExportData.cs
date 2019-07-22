using BullionLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Bullion.util
{
    public class ExportData
    {
        // Writes a JSON file.
        public void Export(Account account, Cipher cipher)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                Title = "Write Account To JSON",
                InitialDirectory = Environment.CurrentDirectory
            };
            try
            {
                saveFileDialog.ShowDialog();
                string filename = saveFileDialog.FileName;

                FileStream writer = new FileStream(filename, FileMode.Create, FileAccess.Write);
                DataContractJsonSerializer outputSerializer = new DataContractJsonSerializer(typeof(Account));
                outputSerializer.WriteObject(writer, account);
                writer.Close();
                try
                {
                    cipher.EncryptFile(filename);   // Encrypts the exported file.
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
