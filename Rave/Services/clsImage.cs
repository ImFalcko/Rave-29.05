
using System.IO;
using Xamarin.Forms;

namespace Rave.Services
{
    class clsImagen
    {
        // Convertir una imagen a un arreglo de bytes
        public static byte[] ConvertImageToByteArray(Stream imageStream)
        {
            byte[] imageData;
            var memoryStream = new MemoryStream();
            
            imageStream.CopyTo(memoryStream);
            imageData = memoryStream.ToArray();
            
            return imageData;
        }

        // Convertir un arreglo de bytes a una imagen
        public static ImageSource ConvertByteArrayToImage(byte[] byteArray)
        {
            var memoryStream = new MemoryStream(byteArray);
            return ImageSource.FromStream(() => memoryStream);
            
        }
    }
}
  