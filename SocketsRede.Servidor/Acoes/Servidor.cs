using SocketsRede.Infra.Configs;
using SocketsRede.Infra.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;

namespace SocketsRede.Servidor.Acoes
{
	public class Servidor : IMenuItem
	{
		private readonly TcpListener _server;

		public Servidor()
		{
			_server = new TcpListener(IPAddress.Parse(Configuracoes.Instance.Endereco),
					Configuracoes.Instance.Porta);
		}
		/// <inheritdoc />
		public void Execute()
		{
			ConsoleKey comando;
			do
			{
				Console.WriteLine(" Escutando conexão, tecle Q para sair. ");

				_server.Start();
				AceitaConexao();

				comando = Console.ReadKey().Key;

			} while (comando != ConsoleKey.Q);
		}

		private void AceitaConexao()
		{
			_server.BeginAcceptTcpClient(ProcessaRequisicao, _server);
		}

		private void ProcessaRequisicao(IAsyncResult result)
		{
			AceitaConexao();  

			var client = _server.EndAcceptTcpClient(result);  

			var stream = client.GetStream();
			int i;

			var bytes = new byte[256];

			while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
			{
				var data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

				Console.WriteLine("Received: {0}", data);

				// Process the data sent by the client. TODO AQUI
				data = data.ToUpper();

				var msg = System.Text.Encoding.ASCII.GetBytes(data);

				// Send back a response.
				stream.Write(msg, 0, msg.Length);

				Console.WriteLine("Sent: {0}", data);
			}

			// Shutdown and end connection
			client.Close();
		}
	}
}