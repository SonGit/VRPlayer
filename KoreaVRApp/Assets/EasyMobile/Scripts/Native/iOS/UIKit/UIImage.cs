#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using EasyMobile.Internal;
using EasyMobile.Internal.iOS;

namespace EasyMobile.iOS.UIKit
{
    using EasyMobile.iOS.CoreGraphics;

    internal class UIImage : iOSInteropObject
    {
        internal UIImage(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        /// <summary>
        /// The scale factor of the image.
        /// </summary>
        /// <value>The scale.</value>
        public float Scale
        {
            get
            {
                return C.UIImage_scale(SelfPtr());
            }
        }

        /// <summary>
        /// The logical dimensions of the image, measured in points.
        /// </summary>
        /// <value>The size.</value>
        public CGSize Size
        {
            get
            {
                var size = new CGSize(0, 0);
                C.UIImage_size(SelfPtr(), ref size);
                return size;
            }
        }

        /// <summary>
        /// Returns the data for the specified image in PNG format.
        /// </summary>
        /// <returns>The image PNG representation.</returns>
        /// <param name="image">The original image data.</param>
        public static byte[] UIImagePNGRepresentation(UIImage image)
        {
            return image == null ? null : PInvokeUtil.GetNativeArray<byte>((buffer, length) =>
                C.UIImage_PNGRepresentation(image.SelfPtr(), buffer, length));
        }

        /// <summary>
        /// Returns the data for the specified image in JPEG format.
        /// </summary>
        /// <returns>The image JPEG representation.</returns>
        /// <param name="image">The original image data.</param>
        /// <param name="compressionQuality">The quality of the resulting JPEG image, 
        /// expressed as a value from 0.0 to 1.0. The value 0.0 represents the maximum compression 
        /// (or lowest quality) while the value 1.0 represents the least compression (or best quality).</param>
        public byte[] UIImageJPEGRepresentation(UIImage image, float compressionQuality)
        {
            if (image == null)
                return null;
            
            compressionQuality = UnityEngine.Mathf.Clamp(compressionQuality, 0, 1);
            return PInvokeUtil.GetNativeArray<byte>((buffer, length) =>
                C.UIImage_JPEGRepresentation(image.SelfPtr(), compressionQuality, buffer, length));
        }

        #region C wrapper

        private static class C
        {
            [DllImport("__Internal")]
            internal static extern float UIImage_scale(HandleRef self);

            [DllImport("__Internal")]
            internal static extern float UIImage_size(HandleRef self, [In, Out] ref CGSize buffer);

            [DllImport("__Internal")]
            internal static extern int UIImage_PNGRepresentation(
                HandleRef self, [In, Out] /* from(unsigned char *) */ byte[] buffer, int byteCount);

            [DllImport("__Internal")]
            internal static extern int UIImage_JPEGRepresentation(
                HandleRef self, float compressionQuality, [In, Out] /* from(unsigned char *) */ byte[] buffer, int byteCount);

        }

        #endregion
    }
}
#endif
