using BullionLibrary;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Bullion.util
{
    public class ImportData
    {
        // Reads a JSON file.
        public Account Import(Account account, Cipher cipher)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON File (*.json)|*.json",
                Title = "Read Account From JSON",
                InitialDirectory = Environment.CurrentDirectory
            };
            try
            {
                openFileDialog.ShowDialog();
                string filename = openFileDialog.FileName;

                try
                {
                    cipher.DecryptFile(filename);   // Decrypts the imported file.
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                FileStream reader = new FileStream(filename, FileMode.Open, FileAccess.Read);
                DataContractJsonSerializer inputSerializer = new DataContractJsonSerializer(typeof(Account));
                account = (Account)inputSerializer.ReadObject(reader);
                reader.Close();
                try
                {
                    cipher.EncryptFile(filename);   // Re-encrpts the imported file to maintain security.
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
            return account;
        }
    }
}
