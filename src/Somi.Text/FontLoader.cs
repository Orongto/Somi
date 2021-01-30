using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Nurose.Text
{
    public static class FontLoader
    {
        public static FontFile Load(string filename)
        {
            XmlSerializer deserializer = XmlSerializer.FromTypes(new[] { typeof(FontFile) })[0];
            string errorMessage = $"Could not find Font '{filename}'";
            try
            {
                var xmlReader = XmlReader.Create(new StreamReader(filename));
                FontFile file = (FontFile)deserializer.Deserialize(xmlReader);
                xmlReader.Close();
                return file;
            }
            catch (FileNotFoundException)
            {
                //TODO voeg logger toe. 
                //Logger.LogError(errorMessage);
                return null;
            }
            catch(DirectoryNotFoundException)
            {
                //Logger.LogError(errorMessage);
                return null;
            }
        }
    }

}