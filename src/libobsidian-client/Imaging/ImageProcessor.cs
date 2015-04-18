using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace libObsidian.Client
{
	public static class ImageProcessor
	{
		/// <summary>
		/// Opens an <see cref="System.Drawing.Image"/> and returns a JPEG-encoded <see cref="System.Drawing.Image"/>.
		/// </summary>
		/// <returns>JPEG-encoded <see cref="System.Drawing.Image"/>.</returns>
		/// <param name="data">Image data.</param>
		public static Image FromBytes (byte[] data) {
			Image img;
			using (var tmpimg = Internal_FromBytes (data)) {
				var tmppath = ToJpeg (tmpimg, 75);
				img = Internal_FromFile (tmppath);
			}
			return img;
		}

		/// <summary>
		/// Opens an <see cref="System.Drawing.Image"/> and returns a JPEG-encoded <see cref="System.Drawing.Image"/>.
		/// </summary>
		/// <returns>JPEG-encoded <see cref="System.Drawing.Image"/>.</returns>
		/// <param name="stream">Image stream.</param>
		public static Image FromStream (Stream stream) {
			Image img;
			using (var tmpimg = Internal_FromStream (stream)) {
				var tmppath = ToJpeg (tmpimg, 75);
				img = Internal_FromFile (tmppath);
			}
			return img;
		}

		/// <summary>
		/// Opens an <see cref="System.Drawing.Image"/> and returns a JPEG-encoded <see cref="System.Drawing.Image"/>.
		/// </summary>
		/// <returns>JPEG-encoded <see cref="System.Drawing.Image"/>.</returns>
		/// <param name="path">Image file path.</param>
		public static Image FromFile (string path) {
			Image img;
			using (var tmpimg = Internal_FromFile (path)) {
				var tmppath = ToJpeg (tmpimg, 75);
				img = Internal_FromFile (tmppath);
			}
			return img;
		}

		/// <summary>
		/// Converts the specified <see cref="System.Drawing.Image"/> to an array of bytes.
		/// </summary>
		/// <returns>JPEG-encoded byte[] representation of the <paramref name="img"/> parameter.</returns>
		/// <param name="img">Image.</param>
		public static byte[] GetBytes (this Image img) {
			byte[] data;
			using (var ms = new MemoryStream ()) {
				img.Save (ms, ImageFormat.Jpeg);
				data = ms.ToArray ();
			}
			return data;
		}

		#region Private image processing functions
		static Image Internal_FromBytes (byte[] imgdata) {
			Image img;
			using (var ms = new MemoryStream (imgdata, false))
				img = Internal_FromStream (ms);
			return img;
		}

		static Image Internal_FromStream (Stream stream) {
			return Image.FromStream (stream, true, true);
		}

		static Image Internal_FromFile (string path) {
			return Image.FromFile (path);
		}

		static string ToJpeg (this Image img, long quality = 75L) {
			var codec = GetEncoder (ImageFormat.Jpeg);
			var encoder = Encoder.Quality;
			var eparams = new EncoderParameters (1);
			var eparam = new EncoderParameter (encoder, quality);
			eparams.Param [0] = eparam;
			var tmpfile = Path.GetTempFileName ();
			img.Save (tmpfile, codec, eparams);
			return tmpfile;
		}

		static bool IsJpeg (this Image img) {
			return img.RawFormat.Equals (ImageFormat.Jpeg);
		}

		static ImageCodecInfo GetEncoder (ImageFormat format) {
			var codecs = ImageCodecInfo.GetImageDecoders ();
			foreach (var codec in codecs)
				if (codec.FormatID == format.Guid)
					return codec;
			return null;
		}
		#endregion
	}
}

