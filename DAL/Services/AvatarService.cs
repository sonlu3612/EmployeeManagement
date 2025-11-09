using System;
using System.Drawing;
using System.Linq;

namespace EmployeeManagement.DAL.Services
{
    public class AvatarService
    {
        private static readonly Color[] AVATAR_COLORS = new Color[]
        {
            Color.FromArgb(76, 175, 80),     // Xanh lá
            Color.FromArgb(33, 150, 243),    // Xanh dương
            Color.FromArgb(255, 152, 0),     // Cam
            Color.FromArgb(156, 39, 176)     // Tím
        };

        public Image CreateDefaultAvatar(string fullName, int size = 128)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                fullName = "?";

            //string initials = fullName.Trim()[0].ToString().ToUpper();

            string initials = fullName.Split(' ').Last()[0].ToString().ToUpper();
            int colorIndex = Math.Abs(fullName.GetHashCode() + (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()) % AVATAR_COLORS.Length;
            Color backgroundColor = AVATAR_COLORS[colorIndex];

            Bitmap bitmap = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(backgroundColor);

                Font font = new Font("Arial", size / 2, FontStyle.Bold);
                StringFormat stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(initials, font, Brushes.White,
                    new RectangleF(0, 0, size, size), stringFormat);

                font.Dispose();
            }

            return bitmap;
        }
    }
}