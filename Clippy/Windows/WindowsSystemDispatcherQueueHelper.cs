using System.Runtime.InteropServices;
using Windows.System;
using Windows.Win32.System.WinRT;
using Windows.Win32;

namespace Clippy.Windows
{
    public static class WindowsSystemDispatcherQueueHelper
    {
        static DispatcherQueueController m_dispatcherQueueController = null;

        public static void EnsureWindowsSystemDispatcherQueueController()
        {
            if (DispatcherQueue.GetForCurrentThread() != null)
                // one already exists, so we'll just use it.
                return;

            if (m_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options.dwSize = (uint)Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options.threadType = DISPATCHERQUEUE_THREAD_TYPE.DQTYPE_THREAD_CURRENT;
                options.apartmentType = DISPATCHERQUEUE_THREAD_APARTMENTTYPE.DQTAT_COM_STA;

                _ = PInvoke.CreateDispatcherQueueController(options, out m_dispatcherQueueController);
            }
        }
    }
}
