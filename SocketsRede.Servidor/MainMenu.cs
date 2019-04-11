using SocketsRede.Infra.Interfaces;
using SocketsRede.Servidor.Acoes;
using System;
using System.Collections.Generic;

namespace SocketsRede.Servidor
{
	public class MainMenu
	{
		private readonly Dictionary<ConsoleKey, IMenuItem> _acoes;

		public MainMenu()
		{
			_acoes = new Dictionary<ConsoleKey, IMenuItem>
			{
				{ConsoleKey.D1, new Configurar()},
				{ConsoleKey.D2, new Acoes.Servidor()}
			};
		}

		public void Start()
		{
			ConsoleKey comando;
			do
			{
				PrintMenu();
				comando = Console.ReadKey().Key;

				if (_acoes.ContainsKey(comando))
					_acoes[comando].Execute();

			} while (comando != ConsoleKey.Q);
		}

		private static void PrintMenu()
		{
			Console.Clear();
			Console.WriteLine("********************************************");
			Console.WriteLine("*                                          *");
			Console.WriteLine("*  Aplicação servidora.                    *");
			Console.WriteLine("*                                          *");
			Console.WriteLine("*  Digite a opção desejada.                *");
			Console.WriteLine("*  1- Configurar                           *");
			Console.WriteLine("*  2- Iniciar                              *");
			Console.WriteLine("*                                          *");
			Console.WriteLine("*  q- Encerrar                             *");
			Console.WriteLine("*                                          *");
			Console.WriteLine("********************************************");
		}
	}
}