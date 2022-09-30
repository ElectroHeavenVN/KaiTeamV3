using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

public class AAClient
{
	public static IPEndPoint Ip;

	public static Socket client;

	public static Thread threadClinent;

	public static void unused_method_0()
	{
		Ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
		client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
		try
		{
			client.Connect(Ip);
		}
		catch
		{
			return;
		}
		threadClinent = new Thread((ThreadStart)delegate
		{
			ConnectQLTK();
		});
		threadClinent.IsBackground = true;
		threadClinent.Start();
	}

	public static void unused_method_1()
	{
		try
		{
			threadClinent.Abort();
			client.Close();
		}
		catch
		{
		}
	}

	public static void ConnectQLTK()
	{
		try
		{
			while (true)
			{
				byte[] array = new byte[5120000];
				client.Receive(array);
				string text = (string)Deserialize(array);
				if (AAAMYs.Id == text.Split('|')[1])
					Service.gI().chat(text.Split('|')[2]);
			}
		}
		catch
		{
		}
	}

	public static void unused_method_2(string string_0)
	{
		client.Send(Serialize(string_0));
	}

	public static byte[] Serialize(object data)
	{
		MemoryStream memoryStream = new MemoryStream();
		new BinaryFormatter().Serialize(memoryStream, data);
		return memoryStream.ToArray();
	}

	public static object Deserialize(byte[] data)
	{
		MemoryStream serializationStream = new MemoryStream(data);
		return new BinaryFormatter().Deserialize(serializationStream);
	}
}
