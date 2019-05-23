using System;
using SocketsRede.Infra.Configs;
using SocketsRede.Infra.Interfaces;

namespace SocketsRede.Servidor.Acoes
{
	public class Configurar : IMenuItem
	{
		/// <inheritdoc />
		public void Execute()
		{
			var s = Configuracoes.Instance.Porta.ToString();
			Console.Clear();
			Console.WriteLine("*********************************************");
			Console.WriteLine("*                                           *");
			Console.WriteLine("*  Configurações                            *");
			Console.WriteLine("*                                           *");
			Console.WriteLine("*  Digite a porta que o servidor executará  *");
			Console.WriteLine($"*  Porta atual: {s.PadRight(28)}*");
			Console.Write("*  Nova porta: ");
			
			var novaPorta = Console.ReadLine();

			Console.SetCursorPosition(15 + novaPorta.Length, Console.CursorTop - 1);

			Console.WriteLine($"{new string(' ', 29 - novaPorta.Length)}*");
			Console.WriteLine("*                                           *");
			Console.WriteLine("*********************************************");
			Console.WriteLine("*                                           *");
			Console.WriteLine("*  Pressione qualquer tecla para continuar  *");
			Console.WriteLine("*                                           *");
			Console.WriteLine("*********************************************");
			Console.ReadKey();

			Configuracoes.Instance.Porta = int.Parse(novaPorta);
			Configuracoes.Instance.Save();
		}
	}
}