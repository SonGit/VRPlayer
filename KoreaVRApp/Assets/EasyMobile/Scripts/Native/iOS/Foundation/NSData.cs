#if UNITY_IOS
using System;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;
using EasyMobile.Internal.iOS;

namespace EasyMobile.iOS.Foundation
{
    internal class NSData : iOSInteropObject
    {
        internal NSData(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        #region NSData API

        /// <summary>
        /// The number of bytes contained by the data object.
        /// </summary>
        /// <value>The length.</value>
        public uint Length
        {
            get { return C.NSData_length(SelfPtr()); }
        }

        /// <summary>
        /// A string that contains a hexadecimal representation of the data object’s contents in a property list format.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get
            {
                return PInvokeUtil.GetNativeString((strBuffer, strLen) => 
                    C.NSData_description(SelfPtr(), strBuffer, strLen));
            }
        }

        /// <summary>
        /// Copies a number of bytes from the start of the data object into a given buffer.
        /// </summary>
        /// <returns>The bytes.</returns>
        /// <param name="length">Length.</param>
        /// <remarks>
        /// The number of bytes copied is the smaller of the length parameter and the length of the data encapsulated in the object.
        /// </remarks>
        public byte[] GetBytes(uint length)
        {
            // The number of bytes copied is the smaller of the length parameter 
            // and the length of the data encapsulated in the object.
            // So we don't want to allocate an array bigger than the actual
            // data that can be read.
            var smallerLen = Math.Min(this.Length, length);
            var buffer = new byte[smallerLen];
            C.NSData_getBytes_length(SelfPtr(), buffer, smallerLen);
            return buffer;
        }

        /// <summary>
        /// Copies a range of bytes from the data object into a given buffer.
        /// </summary>
        /// <returns>The bytes.</returns>
        /// <param name="range">Range.</param>
        /// <remarks>
        /// If range isn’t within the receiver’s range of bytes, an NSRangeException is raised.
        /// </remarks>
        public byte[] GetBytes(NSRange range)
        {
            var buffer = new byte[range.length];
            C.NSData_getBytes_range(SelfPtr(), buffer, ref range);
            return buffer;
        }

        #endregion

        #region C# Utils

        public byte[] ToBytes()
        {
            return GetBytes(Length);
        }

        #endregion

        #region C Wrapper

        private static class C
        {
            // Accessing Underlying Bytes.
            [DllImport("__Internal")]
            internal static extern IntPtr NSData_bytes(HandleRef selfPtr);

            [DllImport("__Internal")]
            internal static extern void NSData_getBytes_length(
                HandleRef selfPtr, 
                [Out] /* from(unsigned char *) */ byte[] buffer, uint length);

            [DllImport("__Internal")]
            internal static extern void NSData_getBytes_range(
                HandleRef selfPtr, 
                [Out] /* from(unsigned char *) */ byte[] buffer, ref NSRange range);

            // Testing Data.
            [DllImport("__Internal")]
            internal static extern uint NSData_length(HandleRef selfPtr);

            [DllImport("__Internal")]
            internal static extern int NSData_description(
                HandleRef selfPtr, 
                [Out] /* from(char *) */ byte[] strBuffer, int charCount);

        }

        #endregion
    }

}
#endif