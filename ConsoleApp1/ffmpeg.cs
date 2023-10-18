
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main1()
    {
        // 设置组播地址和端口
        IPAddress multicastAddress = IPAddress.Parse("224.1.1.100");
        int port = 10002;

        // 创建UdpClient
        UdpClient udpClient = new UdpClient();
        udpClient.JoinMulticastGroup(multicastAddress);

        // 执行FFmpeg命令并捕获标准输出
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = "-i video=\"H65 USB CAMERA\" -c:v libx264 -f mpegts -",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();

            using (StreamReader reader = process.StandardOutput)
            {
                byte[] buffer = new byte[1024];
                int bytesRead;

                // 从标准输出读取数据并发送到组播地址
                while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    udpClient.Send(buffer, bytesRead, new IPEndPoint(multicastAddress, port));
                }
            }

            process.WaitForExit();
        }

        Console.WriteLine("MP4视频数据已发送到组播地址：" + multicastAddress + ":" + port);
        udpClient.Close();
    }
}
