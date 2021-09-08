using System;
using System.Drawing;
using System.Windows.Forms;

namespace Logger
{
    public static class RichTextBoxExtensions
    {
        private const int WM_VSCROLL = 0x115;
        private const int SB_BOTTOM = 7;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam,
        IntPtr lParam);

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            if (box.SelectionLength > 0)
            {
                var tmp_SelectionStart = box.SelectionStart;
                var tmp_SelectionLength = box.SelectionLength;
                
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;

                box.SelectionStart = tmp_SelectionStart;
                box.SelectionLength = tmp_SelectionLength;
                
            }
            else
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;

                SendMessage(box.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
            }
            
        }
    }
}
