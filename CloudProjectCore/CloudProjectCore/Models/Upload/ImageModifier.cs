using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace CloudProjectCore.Models.Upload
{
    public static partial class MyUploadManagerPartial
    {
        private static Bitmap GetTheImageIconForMaps(Bitmap window, Bitmap image)
        {
            Graphics g = Graphics.FromImage(window);
            g.CompositingMode = CompositingMode.SourceCopy;
            image.MakeTransparent();
            g.DrawImage(image, new Point(5, 5));
            return window;
        }
        private static void MakePreview(Image image, Stream streamDestination, Size finalSizeOfImageToCut, Size finalSizeOfImage)
        {
            EncoderParameters ep = new EncoderParameters();
            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 0L);

            ImageCodecInfo imageEncoder = GetEncoder(ImageFormat.Png);

            var photoPreview = ResizeImage(image, finalSizeOfImageToCut, finalSizeOfImage);

            photoPreview.Save(streamDestination, imageEncoder, ep);
            streamDestination.Seek(0, SeekOrigin.Begin);
        }
        private static Bitmap ResizeImage(Image image, Size finalSizeOfImageToCut, Size finalSizeOfImage)
        {
            int imageHeight = (int)((double)finalSizeOfImageToCut.Width / image.Width * image.Height);
            var resizedImage = new Bitmap(image, new Size(finalSizeOfImageToCut.Width, imageHeight));
            if (imageHeight < finalSizeOfImage.Height)
            {
                int imageWidth = (int)((double)finalSizeOfImageToCut.Height / image.Height * image.Width);
                resizedImage = new Bitmap(image, new Size(imageWidth, finalSizeOfImageToCut.Height));
            }

            var imageFinal = new Bitmap(finalSizeOfImageToCut.Width, finalSizeOfImageToCut.Height);
            imageFinal.SetResolution(36, 36);

            using (Graphics g = Graphics.FromImage(imageFinal))
            {
                g.DrawImage(resizedImage, new Rectangle(0, 0, finalSizeOfImage.Width, finalSizeOfImage.Height), new Rectangle(0, 0, finalSizeOfImageToCut.Width, finalSizeOfImageToCut.Height), GraphicsUnit.Pixel);
            }

            return imageFinal;
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
                if (codec.FormatID == format.Guid)
                    return codec;

            return null;
        }
    }
}
