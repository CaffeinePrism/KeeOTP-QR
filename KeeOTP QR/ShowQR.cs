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
        const string keyParameter = "key";
        const string typeParameter = "type";

        private readonly KeePassLib.PwEntry entry;
        private readonly IPluginHost host;
        private string title;
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
                
                IBarcodeWriter writer = new BarcodeWriter {
                    Format = BarcodeFormat.QR_CODE,
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
            catch (Exception) {
                // TODO: be smarter with errors
                pictureBox1.Hide();
                label1.Text = "ohnoez D:";
            }
        }
        public ProtectedString buildURL()
        {
            char[] delimiter = { ':' };
            string url = "otpauth://";
            url += type;
            url += "/";
            url += title;
            url += "?secret=";
            url += key.ReadString();
            url += "&issuer=";
            url += title.Split(delimiter)[0];

            return new ProtectedString(true, url);
        }
        public void getData(string data)
        {
            NameValueCollection parameters = ParseQueryString(data);

            if (parameters[keyParameter] == null)
                throw new ArgumentException("Must have a key in the data");

            this.key = new ProtectedString(true, parameters[keyParameter]);
            if (parameters[typeParameter] != null)
                this.type = parameters[typeParameter];
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

        private void ShowQR_Load(object sender, EventArgs e)
        {

        }
    }
}
