using Riverside.Toolkit.Helpers;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using Windows.Win32.Foundation;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.Win32.UI.WindowsAndMessaging;
using WinUIEx;
using static Riverside.Toolkit.Helpers.NativeHelper;
using static WindowsInput.Native.SystemMetrics;
using Windows.Win32;

namespace Clippy.Helpers
{
    public class ClippyInputHelper
    {
        public static void PointerPress(IntPtr WindowToIgnore)
        {
            return;
                // NativeHelper.GetCursorPos(out Point point);

                // Perform hit testing to determine the target
                //  IntPtr hWnd = await GetWindowHandleAtPoint(point, WindowToIgnore);

                // Forward the pointer event to the target window
                //  NativeHelper.SendMessage(hWnd, NativeHelper.WM_LBUTTONDOWN, (IntPtr)point.X, (IntPtr)point.Y);
                // NativeHelper.SendMessage(hWnd, NativeHelper.WM_LBUTTONUP, (IntPtr)point.X, (IntPtr)point.Y);
        }

        public static void PointerHover(IntPtr WindowToIgnore)
        {
            return;
                PInvoke.GetCursorPos(out Point point);

                // Perform hit testing to determine the target
                IntPtr hWnd = GetWindowHandleAtPoint(point, WindowToIgnore);

                // Forward the pointer event to the target window
                PInvoke.SendMessage((HWND)hWnd, NativeHelper.WM_MOUSEMOVE, (nuint)point.X, (nint)point.Y);
        }


        public static IntPtr GetWindowHandleAtPoint(Point point, IntPtr WindowToIgnore)
        {
            IntPtr hWnd = PInvoke.WindowFromPoint(point);

                while (hWnd != IntPtr.Zero && hWnd != WindowToIgnore)
                {
                    RECT rect;
                    PInvoke.GetWindowRect((HWND)hWnd, out rect);
                    if (rect.left <= point.X && rect.top <= point.Y && rect.right >= point.X && rect.bottom >= point.Y)
                    {
                        // Check if there is a child window at the point
                        IntPtr childHwnd = PInvoke.ChildWindowFromPointEx((HWND)hWnd, point, (CWP_FLAGS)GW_CHILD);
                        if (childHwnd != IntPtr.Zero)
                            hWnd = childHwnd;
                        else
                            break;
                    }
                    else
                    {
                        hWnd = PInvoke.GetWindow((HWND)hWnd, GET_WINDOW_CMD.GW_HWNDNEXT);
                    }
                }

            return hWnd;
        }
    }
}
