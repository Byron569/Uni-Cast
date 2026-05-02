using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

class ScrcpyUI : Form
{
    [DllImport("dwmapi.dll")]
    static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int pv, int cb);
    [DllImport("user32.dll")]
    static extern bool SetProcessDPIAware();
    [DllImport("user32.dll")]
    static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    static extern int SendMessage(IntPtr hWnd, int msg, int wp, int lp);

    const int WM_NCHITTEST = 0x84;
    const int WM_NCLBUTTONDOWN = 0xA1;
    const int HTLEFT = 10, HTRIGHT = 11, HTTOP = 12;
    const int HTTOPLEFT = 13, HTTOPRIGHT = 14;
    const int HTBOTTOM = 15, HTBOTTOMLEFT = 16, HTBOTTOMRIGHT = 17;
    const int HTCLIENT = 1, HTCAPTION = 2;
    const int RESIZE_BORDER = 6;

    static string scrcpyDir = Path.GetDirectoryName(Application.ExecutablePath);
    static string adbExe = Path.Combine(scrcpyDir, "adb.exe");
    static string scrcpyExe = Path.Combine(scrcpyDir, "scrcpy.exe");
    static string htmlPath = Path.Combine(scrcpyDir, "ui", "index.html");

    WebView2 webView;

    [STAThread]
    static void Main()
    {
        SetProcessDPIAware();
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new ScrcpyUI());
    }

    ScrcpyUI()
    {
        var sw = Screen.PrimaryScreen.WorkingArea.Width;
        var sh = Screen.PrimaryScreen.WorkingArea.Height;
        var w = Math.Max(800, (int)(sw * 0.5));
        var h = Math.Max(680, (int)(sh * 0.68));

        Text = "Nexus Cast";
        Size = new Size(w, h);
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(9, 9, 11);
        MinimumSize = new Size(640, 440);
        MaximizeBox = false;

        Load += async (s, e) =>
        {
            int v = 2; DwmSetWindowAttribute(Handle, 33, ref v, 4);
            v = 1; DwmSetWindowAttribute(Handle, 20, ref v, 4);
            await InitWebViewAsync();
        };
    }

    async Task InitWebViewAsync()
    {
        webView = new WebView2();
        webView.Dock = DockStyle.Fill;
        webView.DefaultBackgroundColor = Color.FromArgb(9, 9, 11);

        var userData = Path.Combine(Path.GetTempPath(), "nexus_cast_webview");

        Controls.Add(webView);

        var env = await Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync(null, userData);

        try
        {
            await webView.EnsureCoreWebView2Async(env);
        }
        catch (Exception ex)
        {
            MessageBox.Show("WebView2 init failed: " + ex.Message, "Nexus Cast", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
            return;
        }

        webView.CoreWebView2.AddHostObjectToScript("bridge", new ScrcpyBridge());

        string html;
        if (File.Exists(htmlPath))
        {
            html = File.ReadAllText(htmlPath);
            html = html.Replace("{{IS_WEBVIEW}}", "true");
        }
        else
        {
            html = "<html><body style='background:#09090b;color:#f4f4f5;display:flex;align-items:center;justify-content:center;height:100vh;font-family:system-ui'><div style='text-align:center'><h1>Nexus Cast</h1><p style='color:#71717a'>UI file not found</p></div></body></html>";
        }

        webView.CoreWebView2.NavigateToString(html);
        webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pn = new Pen(Color.FromArgb(38, 38, 52), 1f))
            DrawRoundedRect(e.Graphics, pn, 0, 0, Width - 1, Height - 1, 12);
    }

    void DrawRoundedRect(Graphics g, Pen pn, int x, int y, int w, int h, int r)
    {
        var d = r * 2;
        using (var path = new GraphicsPath())
        {
            path.AddArc(x, y, d, d, 180, 90);
            path.AddArc(x + w - d, y, d, d, 270, 90);
            path.AddArc(x + w - d, y + h - d, d, d, 0, 90);
            path.AddArc(x, y + h - d, d, d, 90, 90);
            path.CloseFigure();
            g.DrawPath(pn, path);
        }
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_NCHITTEST)
        {
            int x = (int)(m.LParam.ToInt64() & 0xFFFF);
            int y = (int)(m.LParam.ToInt64() >> 16);
            var pt = PointToClient(new Point(x, y));

            if (pt.Y <= RESIZE_BORDER)
            {
                if (pt.X <= RESIZE_BORDER)        { m.Result = (IntPtr)HTTOPLEFT; return; }
                if (pt.X >= Width - RESIZE_BORDER) { m.Result = (IntPtr)HTTOPRIGHT; return; }
                m.Result = (IntPtr)HTTOP; return;
            }
            if (pt.Y >= Height - RESIZE_BORDER)
            {
                if (pt.X <= RESIZE_BORDER)        { m.Result = (IntPtr)HTBOTTOMLEFT; return; }
                if (pt.X >= Width - RESIZE_BORDER) { m.Result = (IntPtr)HTBOTTOMRIGHT; return; }
                m.Result = (IntPtr)HTBOTTOM; return;
            }
            if (pt.X <= RESIZE_BORDER)             { m.Result = (IntPtr)HTLEFT; return; }
            if (pt.X >= Width - RESIZE_BORDER)      { m.Result = (IntPtr)HTRIGHT; return; }
        }
        base.WndProc(ref m);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (webView != null)
        {
            webView.Dispose();
            webView = null;
        }
        base.OnFormClosed(e);
    }
}

// ═══════════════════════════════════════════════════════════════════
// Bridge class exposed to JavaScript via WebView2 Host Objects
// ═══════════════════════════════════════════════════════════════════
[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class ScrcpyBridge
{
    [DllImport("user32.dll")]
    static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    static extern int SendMessage(IntPtr hWnd, int msg, int wp, int lp);
    const int WM_NCLBUTTONDOWN = 0xA1;
    const int HTCAPTION = 2;

    static string scrcpyDir = Path.GetDirectoryName(Application.ExecutablePath);

    public string Call(string method, string arg)
    {
        try
        {
            switch (method)
            {
                case "adb":         return Adb(arg);
                case "check-usb":   return HasUsb() ? "true" : "false";
                case "usb-serial":  return GetUsbSerial();
                case "detect-ip":   return DetectPhoneIp();
                case "device-name": return GetDeviceName();
                case "tcpip":       return Adb("tcpip 5555", 8000);
                case "skip-tutorial": return SkipTut() ? "true" : "false";
                case "set-skip-tutorial":
                    try { File.WriteAllText(Path.Combine(scrcpyDir, ".skip_tutorial"), "1"); } catch { }
                    return "ok";
                case "launch":
                    LaunchFromJson(arg);
                    return "ok";
                case "kill-server":
                    Adb("kill-server");
                    return "ok";
                case "minimize":
                    var f0 = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
                    if (f0 != null) f0.BeginInvoke((Action)(() => f0.WindowState = FormWindowState.Minimized));
                    return "ok";
                case "close":
                    Application.Exit();
                    return "ok";
                case "begin-drag":
                    {
                        var f1 = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
                        if (f1 != null)
                            f1.BeginInvoke((Action)(() =>
                            {
                                ReleaseCapture();
                                SendMessage(f1.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                            }));
                    }
                    return "ok";
                default:
                    return "{\"err\":\"unknown method: " + method + "\"}";
            }
        }
        catch (Exception ex)
        {
            return "{\"err\":\"" + ex.Message.Replace("\"", "'").Replace("\\", "/") + "\"}";
        }
    }

    // ---- ADB / scrcpy ops ----
    string RunCmd(string exe, string args, int timeout = 5000)
    {
        try
        {
            using (var p = Process.Start(new ProcessStartInfo(exe, args)
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }))
            {
                p.WaitForExit(timeout);
                return p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
            }
        }
        catch { return ""; }
    }

    string Adb(string args, int timeout = 5000)
    {
        return RunCmd(Path.Combine(scrcpyDir, "adb.exe"), args, timeout);
    }

    bool HasUsb()
    {
        return Adb("devices").Contains("\tdevice");
    }

    string GetUsbSerial()
    {
        var o = Adb("devices");
        foreach (Match m in Regex.Matches(o, @"^(\S+)\tdevice", RegexOptions.Multiline))
        {
            var s = m.Groups[1].Value;
            if (!s.Contains(":")) return s;
        }
        return "";
    }

    string DetectPhoneIp()
    {
        if (!HasUsb()) return "";
        string o = Adb("shell ip -4 -br addr show wlan0");
        var m = Regex.Match(o, @"(\d+\.\d+\.\d+\.\d+)");
        if (m.Success) return m.Groups[1].Value;
        o = Adb("shell ip addr show wlan0");
        m = Regex.Match(o, @"inet\s+(\d+\.\d+\.\d+\.\d+)");
        if (m.Success) return m.Groups[1].Value;
        o = Adb("shell getprop dhcp.wlan0.ipaddress");
        m = Regex.Match(o, @"(\d+\.\d+\.\d+\.\d+)");
        if (m.Success) return m.Groups[1].Value;
        o = Adb("shell ip route show");
        m = Regex.Match(o, @"src\s+(\d+\.\d+\.\d+\.\d+)");
        if (m.Success) return m.Groups[1].Value;
        return "";
    }

    string GetDeviceName()
    {
        var n = Adb("shell getprop ro.product.marketname").Trim();
        if (!string.IsNullOrEmpty(n) && !n.Contains("getprop")) return n;
        var b = Adb("shell getprop ro.product.brand").Trim();
        var m2 = Adb("shell getprop ro.product.model").Trim();
        if (!string.IsNullOrEmpty(b) && !string.IsNullOrEmpty(m2) && !b.Contains("getprop")) return b + " " + m2;
        return "Android Device";
    }

    bool SkipTut()
    {
        return File.Exists(Path.Combine(scrcpyDir, ".skip_tutorial"));
    }

    void LaunchFromJson(string json)
    {
        var flag = ExtractJsonStr(json, "flag");
        var name = ExtractJsonStr(json, "name");
        var quality = ExtractJsonInt(json, "quality");

        var args = flag + " --render-driver=direct3d --window-title=\"" + name + "\"";

        switch (quality)
        {
            case 0: args += " --max-size=1280 --video-bit-rate=8M --max-fps=60 --no-audio"; break;
            case 1: args += " --max-size=1920 --video-bit-rate=16M --max-fps=60"; break;
            case 2: args += " --video-bit-rate=32M --max-fps=60"; break;
        }

        var exe = Path.Combine(scrcpyDir, "scrcpy.exe");
        try
        {
            Process.Start(new ProcessStartInfo(exe, args)
            {
                UseShellExecute = false,
                CreateNoWindow = true
            });

            var form = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
            if (form != null)
                form.BeginInvoke((Action)(() => form.WindowState = FormWindowState.Minimized));
        }
        catch { }
    }

    string ExtractJsonStr(string json, string key)
    {
        var m = Regex.Match(json, "\"" + key + "\"\\s*:\\s*\"([^\"]*)\"");
        return m.Success ? m.Groups[1].Value : "";
    }

    int ExtractJsonInt(string json, string key)
    {
        var m = Regex.Match(json, "\"" + key + "\"\\s*:\\s*(\\d+)");
        return m.Success ? int.Parse(m.Groups[1].Value) : 1;
    }
}