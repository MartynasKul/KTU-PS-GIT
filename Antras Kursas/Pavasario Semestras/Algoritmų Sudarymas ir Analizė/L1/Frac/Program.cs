using System.Diagnostics;

namespace Frac
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ushort Size = 10000;
            Renderer Render = new Renderer("Result", Size, Size, 0xffffffff);

            RotatingCircles(Render, 5000, 5000, 6000, 90, 5);
            Render.Write();
            sw.Stop();
            Console.WriteLine("Fraktalas sugeneruotas per: " + sw.ElapsedMilliseconds + " ms");
        }

        static void RotatingCircles(Renderer Render, double X, double Y, double Size, double Rotation, uint Iteration)
        {
            if (Iteration == 0)
            {
                Render.DrawFilledCircle(X, Y, Size / 2);
                return;
            }

            double IterationSize = Math.Pow(2, Iteration) + (Math.Pow(2, Iteration) - 1) * 2;
            double NextIteration = (Math.Pow(2, Iteration - 1) + (Math.Pow(2, Iteration - 1) - 1) * 2) / IterationSize * Size;

            double MidSize = (Math.Pow(2, Iteration) / IterationSize) * Size / 2;
            double NextMidSize = (Math.Pow(2, Iteration - 1) / IterationSize) * Size / 2;

            Render.DrawFilledCircle(X, Y, MidSize);

            double Distance = NextMidSize + MidSize;

            double RelX;
            double RelY;

            RelX = Math.Cos(Rotation * (Math.PI / 180)) * Distance;
            RelY = Math.Sin(Rotation * (Math.PI / 180)) * Distance;

            RotatingCircles(Render, X + RelX, Y + RelY, NextIteration, Rotation, Iteration - 1);

            RelX = Math.Cos((Rotation - 90) * (Math.PI / 180)) * Distance;
            RelY = Math.Sin((Rotation - 90) * (Math.PI / 180)) * Distance;

            RotatingCircles(Render, X + RelX, Y + RelY, NextIteration, Rotation - 90, Iteration - 1);

            RelX = Math.Cos((Rotation + 90) * (Math.PI / 180)) * Distance;
            RelY = Math.Sin((Rotation + 90) * (Math.PI / 180)) * Distance;

            RotatingCircles(Render, X + RelX, Y + RelY, NextIteration, Rotation + 90, Iteration - 1);

           // RelX = Math.Cos((Rotation + 180) * (Math.PI / 180)) * Distance;
           // RelY = Math.Sin((Rotation + 180) * (Math.PI / 180)) * Distance;
           //
           // RotatingCircles(Render, X + RelX, Y + RelY, NextIteration, Rotation+180, Iteration - 1);
        }

    }

    internal class Renderer
    {
        public Renderer(string OutputName, ushort Width, ushort Height, uint FillingColor) // Color format is ARGB (to define recomended hex: 0xAARRGGBB), coordinates start from bottom left corner, 1 unit is 1 pixel
        {
            this.Width = Width;
            this.Height = Height;
            Buffer = new uint[Width * Height];

            Array.Fill(Buffer, FillingColor);


            this.OutputName = OutputName;
            if (!OutputName.Contains(".bmp"))
                this.OutputName += ".bmp";
        }

        public void DrawFilledCircle(double X, double Y, double R, double Precision = 0.5, uint Color = 0)
        {
            for (double i = 90; i < 270; i += Precision)
            {
                double Angle = ToRadian(i);

                double CX = (Math.Cos(Angle) * R) + X;
                double CY = (Math.Sin(Angle) * R) + Y;

                double EX = (Math.Cos(Math.PI - Angle) * R) + X;

                for (; CX < EX; CX++)
                    SetPixel(CX, CY, Color);
            }
        }

        public void DrawCircle(double X, double Y, double R, double Precision = 0.5, uint Color = 0)
        {
            for (double i = 0; i < 360; i += Precision)
            {
                double Angle = ToRadian(i);

                double CX = (Math.Cos(Angle) * R) + X;
                double CY = (Math.Sin(Angle) * R) + Y;

                SetPixel(CX, CY, Color);
            }
        }

        private static double ToRadian(double Angle)
        {
            return Angle * (Math.PI / 180);
        }

        private void SetPixel(double X, double Y, uint Color)
        {
            int Pixel = GetPixel(X, Y);
            if (Pixel < 0)
                return;

            Buffer[Pixel] = Color;
        }

        private int GetPixel(double X, double Y)
        {
            int Pixel = ((int)Math.Round(Y) * Width) + (int)Math.Round(X);
            if (Pixel > Buffer.Length)
                return -1;

            if (X < 0)
                return -1;
            else if (X > Width)
                return -1;

            return Pixel;
        }

        public void Write()
        {
            using (FileStream File = new FileStream(OutputName, FileMode.Create, FileAccess.Write))
            {
                File.Write(new byte[] { 0x42, 0x4D }); // BM
                File.Write(BitConverter.GetBytes(Height * Width * sizeof(uint) + 0x1A)); // Size
                File.Write(BitConverter.GetBytes(0)); // Reserved (0s)
                File.Write(BitConverter.GetBytes(0x1A)); // Image Offset (size of the header)

                File.Write(BitConverter.GetBytes(0x0C)); // Header size (size is 12 bytes)
                File.Write(BitConverter.GetBytes(Width)); // Width
                File.Write(BitConverter.GetBytes(Height)); // Height
                File.Write(BitConverter.GetBytes((ushort)1)); // Color plane
                File.Write(BitConverter.GetBytes((ushort)32)); // bits per pixel

                byte[] Converted = new byte[Buffer.Length * sizeof(uint)];

                System.Buffer.BlockCopy(Buffer, 0, Converted, 0, Converted.Length);

                File.Write(Converted);
                File.Close();
            }
        }

        private readonly uint[] Buffer;
        private readonly ushort Width;
        private readonly ushort Height;
        private readonly string OutputName;
    }
}
