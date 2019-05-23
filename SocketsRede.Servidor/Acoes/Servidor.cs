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
			_server = new TcpListener(IPAddress.Any, Configuracoes.Instance.Porta);
		}
		
		public void Execute()
		{
			ConsoleKey comando;

			Console.Clear();
			Console.WriteLine("********************************************");
			Console.WriteLine("*                                          *");
			Console.WriteLine("*  Servidor em execução                    *");
			Console.WriteLine("*                                          *");
			Console.WriteLine("*  Escutando conexão, tecle Q para sair    *");
			Console.WriteLine("*                                          *");

			_server.Start();
			AceitaConexao();

			do
			{
				comando = Console.ReadKey().Key;
			} while (comando != ConsoleKey.Q);

			Console.WriteLine("                                          *");
			Console.WriteLine("*  Pressione qualquer tecla para continuar *");
			Console.WriteLine("*                                          *");
			Console.WriteLine("********************************************");
			Console.ReadKey();
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

				Console.WriteLine("********************************************");
				Console.WriteLine("*                                          *");
				Console.WriteLine("*  Informação recebida                     *");
				Console.WriteLine($"*  {data.PadRight(38)}  *");

				// TODO Aqui é realizado o processamento
				data = data.ToUpper();

				var msg = System.Text.Encoding.ASCII.GetBytes(data);
				
				stream.Write(msg, 0, msg.Length);

				Console.WriteLine("*                                          *");
				Console.WriteLine("*  Informação enviada                      *");
				Console.WriteLine($"*  {data.PadRight(38)}  *");
				Console.WriteLine("*                                          *");
				Console.WriteLine("********************************************");
			}
			
			client.Close();
		}
	}
}