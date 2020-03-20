using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


using System.Linq;
using System.Net;
using System.Text;
using Kugar.Core.BaseStruct;

namespace Kugar.Core.ExtMethod
{
    /// <summary>
    ///     ͼ��ĳ��ò���
    /// </summary>
    public static class ImageExt
    {
        /// <summary>
        ///     ��λͼ������ָ���ĸ�ʽ,����ΪByte����
        /// </summary>
        /// <param name="img">λͼ����</param>
        /// <param name="format">�����ʽ</param>
        /// <returns></returns>
        public static byte[] SaveToBytes(this Image img, ImageFormat format)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            if (format == null)
            {
                format = ImageFormat.Jpeg;
            }

            using (var ms = new System.IO.MemoryStream(1024))
            {
                img.Save(ms, format);

                return ms.ToArray();
            }
        }

        /// <summary>
        ///      ��λͼ������ָ���ĸ�ʽ,����ΪByte����,�������ָ���Ļ�����
        /// </summary>
        /// <param name="img">λͼ����</param>
        /// <param name="format">λͼ��ʽ</param>
        /// <param name="buffer">�Զ��建����</param>
        /// <param name="offset">��������ʼλ�õ�ƫ����</param>
        /// <param name="count">���������õĴ�С</param>
        /// <returns></returns>
        public static int SaveToBytes(this Image img, ImageFormat format, byte[] buffer, int offset, int count)
        {
            if (img == null)
            {
                throw new ArgumentNullException("img");
            }

            if (format == null)
            {
                format = ImageFormat.Jpeg;
            }

            using (var ms = new BaseStruct.ByteStream(buffer, offset, count))
            {
                img.Save(ms, format);

                return (int)(ms.Position - offset);
            }
        }

        /// <summary>
        ///     �������Byte[]������,����λͼ
        /// </summary>
        /// <param name="bytes">��������</param>
        /// <param name="offset">������ʼ��ַƫ����</param>
        /// <param name="count">���ݿ�������</param>
        /// <returns></returns>
        public static Image BuildImage(byte[] bytes, int offset, int count)
        {
            using (var stream = new Bitmap(new ByteStream(bytes, offset, count)))
            {
                return new Bitmap(stream);
            }
            //            var bitmap = new Bitmap(new ByteStream(bytes, offset, count));
            //
            //            return bitmap;
        }

        /// <summary>
        ///     ��ָ������ַ�ж�ȡλͼͼƬ
        /// </summary>
        /// <param name="uri">ͼƬ��Ŀ����ַ</param>
        /// <returns></returns>
        public static Image BuildImageFromUri(string uri)
        {
            return BuildImageFromUri(new Uri(uri));
        }

        /// <summary>
        ///     ��ָ������ַ�ж�ȡλͼͼƬ
        /// </summary>
        /// <param name="uri">ͼƬ��Ŀ����ַ</param>
        /// <returns></returns>
        public static Image BuildImageFromUri(Uri uri)
        {
            var data = Core.Network.WebHelper.GetFile(uri);

            if (data != null && data.Data != null)
            {
                return BuildImage(data.Data, 0, data.Data.Length);
            }

            return null;
            //        	
            //        	
            //            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(uri);
            //            var response = request.GetResponse();
            //            try
            //            {
            //                using (var stream =response.GetResponseStream())
            //                {
            //                    if (stream != null)
            //                    {
            //                        var bitmap = new Bitmap(stream);
            //
            //                        return bitmap;
            //                    }
            //                    else
            //                    {
            //                        return null;
            //                    }
            //                }
            //            }
            //            catch (Exception)
            //            {
            //                return null;
            //            }
            //            finally
            //            {
            //                response.Close();
            //            }
        }

        public static void MakeThumbnail(string srcImagePath, string thumbnailPath, int width, int height, ThumbnailType mode = ThumbnailType.HW, ImageFormat imageFormat = null)
        {
            if (imageFormat == null)
            {
                imageFormat = ImageFormat.Jpeg;
            }

            using (var bitmap = Image.FromFile(srcImagePath))
            {
                using (var target = MakeThumbnail(bitmap, width, height, mode))
                {
                    target.Save(thumbnailPath, imageFormat);
                }
            }
        }

        public static Image MakeThumbnail(this Image srcImage, int width, int height, ThumbnailType mode)
        {
            //System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;

            int ow = srcImage.Width;
            int oh = srcImage.Height;

            if (srcImage.Width == width && srcImage.Height == height)
            {
                return srcImage;
            }

            switch (mode)
            {
                case ThumbnailType.HW:
                    {
                        if (srcImage.Width > width || srcImage.Height > height)
                        {
                            // scale down the smaller dimension  
                            if (width * srcImage.Height < height * srcImage.Width)
                            {
                                towidth = width;
                                toheight = (int)Math.Round((decimal)srcImage.Height * width / srcImage.Width);
                            }
                            else
                            {
                                toheight = height;
                                towidth = (int)Math.Round((decimal)srcImage.Width * height / srcImage.Height);
                            }
                        }
                        else
                        {
                            towidth = srcImage.Width;
                            toheight = srcImage.Height;
                        }

                        x = (int)Math.Round((width - towidth) / 2.0);
                        y = (int)Math.Round((height - toheight) / 2.0);
                    }   
                    break;
                case ThumbnailType.W:
                    toheight = srcImage.Height * width / srcImage.Width;
                    break;
                case ThumbnailType.H:
                    towidth = srcImage.Width * height / srcImage.Height;
                    break;
                case ThumbnailType.Cut:
                    if ((double)srcImage.Width / (double)srcImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = srcImage.Height;
                        ow = srcImage.Height * towidth / toheight;
                        y = 0;
                        x = (srcImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = srcImage.Width;
                        oh = srcImage.Width * height / towidth;
                        x = 0;
                        y = (srcImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //�½�һ��bmpͼƬ 
            Image bitmap = new Bitmap(towidth, toheight);

            //�½�һ������ 
            Graphics g = Graphics.FromImage(bitmap);

            //���ø�������ֵ�� 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //���ø�����,���ٶȳ���ƽ���̶� 
            g.SmoothingMode = SmoothingMode.HighQuality;

            //��ջ�������͸������ɫ��� 
            g.Clear(Color.Transparent);

            //��ָ��λ�ò��Ұ�ָ����С����ԭͼƬ��ָ������ 
            g.DrawImage(srcImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                return bitmap;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                srcImage.Dispose();
                g.Dispose();
            }
        }

    }

    /// <summary>
    /// ����ͼ��������
    /// </summary>
    public enum ThumbnailType
    {
        /// <summary>
        /// ָ���߿����ţ������Σ�
        /// </summary>
        HW = 0,

        /// <summary>
        /// //ָ�����߰�����    
        /// </summary>
        W = 1,

        /// <summary>
        /// //ָ���ߣ������� 
        /// </summary>
        H = 2,

        /// <summary>
        /// //ָ���߿�ü��������Σ�    
        /// </summary>
        Cut = 3
    }
}
