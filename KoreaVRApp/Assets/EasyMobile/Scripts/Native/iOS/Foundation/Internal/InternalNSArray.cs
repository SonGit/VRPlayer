#if UNITY_IOS
using System;
using System.Runtime.InteropServices;

namespace EasyMobile.Internal.iOS.Foundation
{
    internal abstract class InternalNSArray : iOSInteropObject
    {
        internal InternalNSArray(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        /// <summary>
        /// Returns the pointer for the object at the given index.
        /// </summary>
        /// <returns>The pointer at index.</returns>
        /// <param name="index">Index.</param>
        protected virtual IntPtr ObjectPointerAtIndex(uint index)
        {
            return C.NSArray_objectAtIndex(SelfPtr(), index);
        }

        #region C Wrapper

        protected static class C
        {
            [DllImport("__Internal")]
            internal static extern uint NSArray_count(HandleRef self);

            [DllImport("__Internal")]
            internal static extern IntPtr NSArray_objectAtIndex(HandleRef self, uint index);
        }

        #endregion
    }
}
#endif