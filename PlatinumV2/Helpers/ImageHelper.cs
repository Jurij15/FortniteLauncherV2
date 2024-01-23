using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Storage;

namespace PlatinumV2.Helpers
{
    public class ImageHelper
    {
        public static ImageSource GetImageSource(string path)
        {
            ImageSource returnval = null;

            if (path != null)
            {
                try
                {
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.UriSource = new System.Uri(path);

                    returnval = bitmapImage;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return returnval;
        }

        public static async Task<ImageSource> ConvertJPGtoImageSource (string path)
        {
            ImageSource returnval = null;

            if (path != null)
            {
                try
                {
                    StorageFile file = await StorageFile.GetFileFromPathAsync(path);
                    BitmapImage bitmapImage = await ConvertSoftwareBitmapToBitmapImage(await ConvertJpgToSoftwareBitmap(file));

                    // Convert BitmapImage to ImageSource
                    ImageSource imageSource = bitmapImage as ImageSource;

                    returnval = imageSource;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return returnval;
        }

        // Helper method to convert .jpg to SoftwareBitmap
        private static async Task<SoftwareBitmap> ConvertJpgToSoftwareBitmap(StorageFile jpgFile)
        {
            using (IRandomAccessStream stream = await jpgFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                return softwareBitmap;
            }
        }

        // Helper method to convert SoftwareBitmap to BitmapImage
        private static async Task<BitmapImage> ConvertSoftwareBitmapToBitmapImage(SoftwareBitmap softwareBitmap)
        {
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);
                encoder.SetSoftwareBitmap(softwareBitmap);
                await encoder.FlushAsync();

                BitmapImage bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);

                return bitmapImage;
            }
        }
    }
}
