#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace EasyMobile.Internal.iOS
{
    internal class iOSInteropObject : InteropObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EasyMobile.iOS.iOSInteropObject"/> class.
        /// </summary>
        /// <param name="selfPointer">Self pointer.</param>
        internal iOSInteropObject(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        /// <summary>
        /// Effectively increments the reference count of the native object
        /// at the given pointer. This is called inside the base constructor
        /// and should never be called anywhere else.
        /// </summary>
        /// <param name="selfPointer">Self pointer.</param>
        protected override void AttachHandle(HandleRef selfPointer)
        {
            C.InteropObject_Ref(selfPointer);
        }

        /// <summary>
        /// Effectively decrements the reference count of the native object
        /// at the give pointer. This is called when the object is disposed
        /// and should never be called anywhere else.
        /// </summary>
        /// <param name="selfPointer">Self pointer.</param>
        protected override void ReleaseHandle(HandleRef selfPointer)
        {
            C.InteropObject_Unref(selfPointer);
        }

        #region C wrapper

        private static class C
        {
            [DllImport("__Internal")]
            internal static extern void InteropObject_Ref(HandleRef self);

            [DllImport("__Internal")]
            internal static extern void InteropObject_Unref(HandleRef self);
        }

        #endregion
    }
}
#endif
