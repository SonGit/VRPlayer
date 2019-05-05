#if UNITY_IOS
using System;
using System.Linq;
using System.Runtime.InteropServices;
using AOT;
using EasyMobile.Internal;
using EasyMobile.Internal.iOS;
using EasyMobile.Internal.iOS.Foundation;

namespace EasyMobile.iOS.Foundation
{
    /// <summary>
    /// This class is intended for working with iOS NSArray that
    /// contains interop objects. For native arrays of other types, consider
    /// using <see cref="PInvokeUtil.GetNativeArray"/>.
    /// </summary>
    internal class NSArray<T> : InternalNSArray where T : iOSInteropObject
    {
        internal NSArray(IntPtr selfPointer)
            : base(selfPointer)
        {
        }

        #region NSArray API

        /// <summary>
        /// The number of objects in the array.
        /// </summary>
        /// <value>The count.</value>
        public uint Count
        {
            get { return C.NSArray_count(SelfPtr()); }
        }

        /// <summary>
        /// Returns the object located at the specified index.
        /// </summary>
        /// <returns>The at index.</returns>
        /// <param name="index">Index.</param>
        /// <param name="constructor">Constructor.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T ObjectAtIndex(uint index, Func<IntPtr, T> constructor)
        {
            IntPtr ptr = C.NSArray_objectAtIndex(SelfPtr(), index);
            T obj = constructor(ptr);
            CoreFoundation.CFType.CFRelease(ptr);   // release pointer returned by native method to balance ref count
            return obj;
        }

        #endregion

        #region C# Utils

        public T[] ToArray(Func<IntPtr, T> constructor)
        {
            return PInvokeUtil.ToEnumerable<T>(
                (int)Count,
                index => ObjectAtIndex(
                    (uint)index,
                    constructor
                )).ToArray();
        }

        #endregion
    }
}
#endif