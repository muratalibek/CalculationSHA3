using System;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Crypto.Digests;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Users\murat\sourcefolder\task2";
            DirectoryInfo folder = new DirectoryInfo(folderPath);
            FileInfo[] files = folder.GetFiles("*", SearchOption.AllDirectories);

            var hashList = new List<string>();
            foreach (FileInfo file in files)
            {
                using (var stream = file.OpenRead())
                {
                    byte[] buffer = new byte[8192];
                    var digest = new Sha3Digest(256);
                    int bytesRead;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        digest.BlockUpdate(buffer, 0, bytesRead);
                    }
                    byte[] hash = new byte[digest.GetDigestSize()];
                    digest.DoFinal(hash, 0);
                    string hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    hashList.Add(hashString);
                }
            }

            var sortedHashes = hashList.OrderBy(h => h);

            foreach (string hashString in sortedHashes)
            {
                Console.Write(hashString);
            }
        }
    }
}
