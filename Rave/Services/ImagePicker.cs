using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using System.IO;

namespace Rave.Services
{
    // Clase para seleccionar una imagen de la galería
    internal class ImagePicker
    {
        // Propiedad para guardar la ruta de la imagen seleccionada
        public string FullPath { get; set; }
        public Stream File { get; set; }
        // Método para seleccionar una imagen de la galería
        public async Task<ImageSource> GetImage()
        {
            // Seleccionar una imagen de la galería
            var pickResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Seleccionar imagen"
            });

            // Si se seleccionó una imagen
            if (pickResult != null)
            {
                // Procesar la imagen seleccionada aquí
                ImageSource Source = ImageSource.FromStream(() => pickResult.OpenReadAsync().Result);
                File = await pickResult.OpenReadAsync();
                FullPath = pickResult.FullPath;
                return Source;
            }
            return null;
        }
    }
}
