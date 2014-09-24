using KeePass.Plugins;
using KeePassLib.Security;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXing;

namespace KeeOTPQR
{
    public partial class ShowQR : Form
    {
        private readonly KeePassLib.PwEntry entry;
        private readonly IPluginHost host;
        private string title;
        private string size = "6";
        private string step = "30";
        private string type = "totp";
        private ProtectedString data;
        private ProtectedString key;


        public ShowQR(KeePassLib.PwEntry entry, IPluginHost host)
        {
            this.host = host;
            this.entry = entry;
            InitializeComponent();

            this.title = entry.Strings.Get("Title").ReadString();
            this.data = entry.Strings.Get("otp");

            banner.Image = KeePass.UI.BannerFactory.CreateBanner(banner.Width,
                banner.Height,
                KeePass.UI.BannerStyle.Default,
                Properties.Resources.qrcode.GetThumbnailImage(32, 32, null, IntPtr.Zero),
                "QR Code",
                "QR Code from stored KeeOTP secret");

            this.Icon = host.MainWindow.Icon;

            try
            {
                getData(data.ReadString());
                string[] titlesplit = title.Split(new char[] { ':' });
                keyIssuer.Text = titlesplit[0];
                keyTitle.Text = titlesplit[1];

                buildQR();
            } catch (Exception e)
            {
                if (e is ArgumentException || e is NullReferenceException)
                {
                    errorMsg.Show();
                    errorMsg.Text = "No KeeOTP data!";
                    groupBox1.Hide();
                }
                else
                    throw;
            }
            
        }
        public void buildQR()
        {
            bool optionsBlank = (keyIssuer.Text.Trim().Length == 0 || keyTitle.Text.Trim().Length == 0) ? true : false;

            if (optionsBlank)
            {
                errorMsg.Show();
                errorMsg.Text = "QR options cannot be blank";
            }
            else
            {
                errorMsg.Hide();
            }
            IBarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Renderer = new ZXing.Rendering.BitmapRenderer
                {
                    // assuming the form UI colors will always be sane
                    Background = this.BackColor,
                    Foreground = optionsBlank ? Color.Red : this.ForeColor
                },
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = pictureBox1.Width,
                    Height = pictureBox1.Height
                }
            };
            var result = writer.Write(buildURL().ReadString());
            var barcodeBitmap = new Bitmap(result);
            pictureBox1.Image = barcodeBitmap;
        }
        public ProtectedString buildURL()
        {
            // TODO: support keepass's built in {HMACOTP}
            string url = "otpauth://";
            url += type;
            url += "/";
            url += keyIssuer.Text+":"+keyTitle.Text;
            url += "?secret=";
            url += key.ReadString();
            url += "&issuer=";
            url += keyIssuer.Text;
            url += "&digits=";
            url += size;
            url += "&period=";
            url += step;

            return new ProtectedString(true, url);
        }
        public void getData(string data)
        {
            NameValueCollection parameters = ParseQueryString(data);

            if (parameters["key"] == null)
                throw new ArgumentException("Must have a key in the data");

            this.key = new ProtectedString(true, parameters["key"]);
            if (parameters["type"] != null)
                this.type = parameters["type"];
            if (parameters["size"] != null)
                this.size = parameters["size"];
            if (parameters["step"] != null)
                this.step = parameters["step"];
        }

        /// <remarks>
        /// Hacky query string parsing.  This was done due to reports
        /// of people with just a 3.5 or 4.0 client profile getting errors
        /// as the System.Web assembly where .net's implementation of
        /// Url encoding and query string parsing is locate.
        /// 
        /// This should be fine since the only thing stored in the string
        /// that needs to be encoded or decoded is the '=' sign.
        /// </remarks>
        private static NameValueCollection ParseQueryString(string data)
        {
            var collection = new NameValueCollection();

            var parameters = data.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var parameter in parameters)
            {
                if (parameter.Contains("="))
                {
                    var pieces = parameter.Split('=');
                    if (pieces.Length != 2)
                        continue;

                    collection.Add(pieces[0], pieces[1].Replace("%3d", "="));
                }
            }

            return collection;
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        private void keyIssuer_TextChanged(object sender, EventArgs e)
        {
            buildQR();
        }

        private void keyTitle_TextChanged(object sender, EventArgs e)
        {
            buildQR();
        }
    }
}
