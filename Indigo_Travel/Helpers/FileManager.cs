namespace Indigo_Travel.Helpers
{
    public static class FileManager
    {
        public static string SaveFile( string rootPath, string folderName, IFormFile file)
        {
            string filename = file.FileName;
            filename = filename.Length > 64 ? filename.Substring(filename.Length - 64, 64) : filename;

            filename = Guid.NewGuid().ToString() + filename;

            string path = Path.Combine(rootPath, folderName, filename);

            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(filestream);
            }

            return filename;
        }


        public static void DeleteFile(string rootPath, string folderName, string filename)
        {
            string path = Path.Combine(rootPath, folderName, filename);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
