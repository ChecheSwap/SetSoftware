using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoMusical.IMAGE_OPERATIONS
{
    public static class imageOperations
    {
        public static Bitmap getImageFromBytes(byte[] source)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
            Bitmap bmp = null;

            try
            {
                bmp = (Bitmap)converter.ConvertFrom(source);
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return bmp;
        }

        public static byte[] getbytefromimage(Bitmap bitmap)
        {
            byte[] result = null;
            if (bitmap != null)
            {
                try
                {
                    MemoryStream stream = new MemoryStream();
                    bitmap.Save(stream, bitmap.RawFormat);
                    result = stream.ToArray();

                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }
            return result;
        }
    }
}
