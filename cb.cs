using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace MundoMusical.CODE_BAR
{
    public class cb
    {
        private BarcodeWriter writer;
        private Bitmap result;
        private MemoryStream ms;
        private FileStream stream;
        private IBarcodeReader reader;
        private Result Sres;

        private BarcodeFormat formato = BarcodeFormat.CODE_39;

        public cb()
        {
            this.writer = new BarcodeWriter();
            this.reader = new BarcodeReader();
            this.writer.Format = this.formato;
            this.writer.Options.Height = 150;
            this.writer.Options.Width = 160;
            this.result = null;
            this.ms = null;
            this.stream = null;            
            this.Sres = null;
        }

        public Bitmap getImage(string val)
        {
            this.result = null;
            try
            {
                this.result = new Bitmap(writer.Write(val));
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString(), "Error");
            }
            return this.result;
        }

        public Bitmap getImage(string val, int width, int height, bool includestring)
        {
            if (!includestring)
            {
                this.writer.Options.PureBarcode = true;
            }
            else
            {
                this.writer.Options.PureBarcode = false;
            }

            this.result = null;

            this.writer.Options.Width = width;
            this.writer.Options.Height = height;

            try
            {
                this.result = new Bitmap(writer.Write(val));
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString(), "Error");
            }
            return this.result;
        }

        public Bitmap getonlycodabar(string number, int width, int height)
        {
            this.writer = new BarcodeWriter()
            {

                Options = new EncodingOptions
                {
                    PureBarcode = true,
                    Height = height,
                    Width = width,
                    Margin = 0
                },
                Format = this.formato
            };

            return this.getImage(number);
        }

        public bool saveImage(Bitmap act, string title, string path)
        {
            bool flag = false;

            try
            {
                act.RotateFlip(RotateFlipType.Rotate90FlipXY);
                this.ms = new MemoryStream();

                act.Save(ms, ImageFormat.Jpeg);

                ms.Seek(0, SeekOrigin.Begin);

                if (!Directory.Exists(path))
                {

                    Directory.CreateDirectory(path);
                }

                this.stream = new FileStream(path +"\\"+title+".jpeg", FileMode.Create, FileAccess.Write);

                ms.WriteTo(stream);

                this.ms.Dispose();

                this.stream.Dispose();

                flag = true;

            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString(), "Error");
            }

            return flag;
        }

        public Bitmap codeB(string value)
        {
            this.result = null;

            try
            {
                this.result= new Bitmap(writer.Write(value));
                this.ms = new MemoryStream();
                result.Save(ms, ImageFormat.Jpeg);
                ms.Seek(0, SeekOrigin.Begin);
                genericDefinitions.ok(Application.StartupPath,"");
                Directory.CreateDirectory(Application.StartupPath + "\\CODEBARS");
                this.stream = new FileStream(Application.StartupPath+"\\CODEBARS\\"+value+ ".jpeg", FileMode.Create, FileAccess.Write);
                ms.WriteTo(stream);                

            }
            catch (Exception ex)
            {
               genericDefinitions.error(ex.ToString(),"Error");
            }

            return this.result;
        }

        public string decodeB(ref Bitmap bm)
        {            
            try
            {
                this.Sres = reader.Decode(bm);
            }catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString(), "Error");                    
            }

            return this.Sres.ToString();
        }
    }
}
