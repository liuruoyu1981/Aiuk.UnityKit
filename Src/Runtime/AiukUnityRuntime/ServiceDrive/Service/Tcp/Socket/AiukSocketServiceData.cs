using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using AiukUnityRuntime;

public class AiukSocketServiceData
{
    public Socket Socket { get; set; }

    public byte[] ReadBuffer = new byte[1024 * 64];

    public List<byte> CacheBuffer = new List<byte>();

    public bool IsReading;

    public IAiukUnityEnCoder Encoder { get; set; }

    public Thread Thead { get; set; }

    public byte[] SendBytes { get; set; }





}
