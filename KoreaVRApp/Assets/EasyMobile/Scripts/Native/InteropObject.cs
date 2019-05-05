using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EasyMobile.Internal
{
    /// <summary>
    /// An abstract class representing objects that act as proxies for unmanaged objects, which are referred to using IntPtrs.
    /// </summary>
    internal abstract class InteropObject : IDisposable
    {
        private HandleRef mSelfPointer;

        protected abstract void AttachHandle(HandleRef selfPointer);

        protected abstract void ReleaseHandle(HandleRef selfPointer);

        public bool IsDisposed()
        {
            return PInvokeUtil.IsNull(mSelfPointer);
        }

        public bool HasSamePointerWith(InteropObject other)
        {
            return other != null && this.ToPointer().Equals(other.ToPointer());
        }

        protected HandleRef SelfPtr()
        {
            if (IsDisposed())
            {
                throw new InvalidOperationException(
                    "Attempted to use object after it was cleaned up");
            }

            return mSelfPointer;
        }

        protected virtual void AttachHandle()
        {
            AttachHandle(SelfPtr());
        }

        protected virtual void ReleaseHandle()
        {
            ReleaseHandle(SelfPtr());
        }

        internal InteropObject(IntPtr pointer)
        {
            mSelfPointer = PInvokeUtil.CheckNonNull(new HandleRef(this, pointer));
            AttachHandle(mSelfPointer);
        }

        ~InteropObject ()
        {
            Cleanup();
        }

        public void Dispose()
        {
            Cleanup();
            System.GC.SuppressFinalize(this);
        }

        internal IntPtr ToPointer()
        {
            return SelfPtr().Handle;
        }

        private void Cleanup()
        {
            if (!PInvokeUtil.IsNull(mSelfPointer))
            {
                ReleaseHandle(mSelfPointer);
                mSelfPointer = new HandleRef(this, IntPtr.Zero);
            }
        }
    }
}

