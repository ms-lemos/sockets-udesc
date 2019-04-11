using System;
using System.IO;
using Newtonsoft.Json;

namespace SocketsRede.Infra.Configs
{
	public class Configuracoes
	{
		private static readonly string DiretorioBase = AppDomain.CurrentDomain.BaseDirectory;
		private static readonly string Arquivo = DiretorioBase + "cfg.json";
		private static Configuracoes _instance;

		public int Porta { get; set; }
		public string Endereco { get; set; }
		public int LimiteConexoes { get; set; }
		
		public static Configuracoes Instance
		{
			get => _instance ?? (_instance = LerDoArquivo());
			set { _instance = value; GravarArquivo(value); }
		}
		

		public void Save()
		{
			GravarArquivo(this);
		}

		private static Configuracoes LerDoArquivo()
		{
			if (!File.Exists(Arquivo))
				File.Create(Arquivo).Dispose();

			var readAllText = File.ReadAllText(Arquivo);

			return JsonConvert.DeserializeObject<Configuracoes>(readAllText) ?? new Configuracoes();
		}

		private static void GravarArquivo(Configuracoes cfg)
		{
			File.WriteAllText(Arquivo, JsonConvert.SerializeObject(cfg));
		}
	}
}
